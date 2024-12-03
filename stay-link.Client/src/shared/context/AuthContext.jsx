import React, { createContext, useState, useEffect, useRef } from "react";
import { jwtDecode } from "jwt-decode";
import dayjs from "dayjs";
import { useLocation, useNavigate } from "react-router-dom";
import {
  BEARER_TOKEN_PREFIX,
  REFRESH_TOKEN_URL,
} from "../constants/apiConstants";
import { LOGIN_PATH, PUBLIC_PATHS } from "../constants/routes";

export const AuthContext = createContext({
  isLoggedIn: () => false,
  setAccessToken: () => {},
  accessToken: null,
  setAuthData: () => {},
  userID: null,
  userRoles: null,
  refToken: { current: null },
  validateJwtToken: () => Promise.resolve(false),
  subscribeTokenRefresh: () => {},
});

const validateToken = (token) => {
  try {
    const decoded = jwtDecode < ParsedJWTType > token;
    if (decoded && decoded.exp && dayjs.unix(decoded.exp).isBefore(dayjs())) {
      return false;
    }
    return !!decoded;
  } catch (e) {
    return false;
  }
};

const clearLocalStorage = () => {
  localStorage.removeItem("accessToken");
  localStorage.removeItem("refreshToken");
  localStorage.removeItem("userID");
  localStorage.removeItem("userRoles");
};

const matchPath = (path, patterns, []) => {
  return patterns.some((pattern) => {
    const regex = new RegExp(`^${pattern.replace("*", ".*")}$`);
    return regex.test(path);
  });
};

export const AuthProvider = ({ children }) => {
  const navigate = useNavigate();

  const location = useLocation();

  const [accessToken, setAccessToken] =
    (useState < string) |
    (null >
      (() => {
        return localStorage.getItem("accessToken");
      }));

  const refToken = useRef(localStorage.getItem("accessToken"));

  const [refreshToken, setRefreshToken] =
    (useState < string) |
    (null >
      (() => {
        return localStorage.getItem("refreshToken");
      }));

  const [userID, setUserID] = useState(() => {
    const userIDString = localStorage.getItem("userID");
    return userIDString ? parseInt(userIDString, 10) : null;
  });

  const [userRoles, setUserRoles] = useState(() => {
    const rolesString = localStorage.getItem("userRoles");
    return rolesString ? JSON.parse(rolesString) : null;
  });

  const isLoggedIn = () => accessToken !== null;

  const setAuthData = (token, refresh) => {
    if (validateToken(token)) {
      const userInfo = jwtDecode < ParsedJWTType > token;
      if (userInfo) {
        refToken.current = token;
        setAccessToken(token);
        setRefreshToken(refresh);
        setUserID(userInfo.userId);
        setUserRoles(userInfo.roles);

        localStorage.setItem("accessToken", token);
        localStorage.setItem("refreshToken", refresh);
        localStorage.setItem("userID", userInfo.userId.toString());
        localStorage.setItem("userRoles", JSON.stringify(userInfo.roles));
      } else {
        console.error("Failed to decode token.");
        navigate(LOGIN_PATH);
      }
    } else {
      console.error("Invalid or expired token");
      setAccessToken(null);
      setRefreshToken(null);
      navigate(LOGIN_PATH);
    }
  };

  const refreshAccessToken = async () => {
    try {
      const response = await fetch(REFRESH_TOKEN_URL, {
        method: "POST",
        headers: {
          Authorization: BEARER_TOKEN_PREFIX + `${refreshToken}`,
        },
      });
      if (response.ok) {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const data = await response.json();
        setAuthData(data.access_token, data.refresh_token);
        return data.access_token;
      } else {
        setAccessToken(null);
        setRefreshToken(null);
        navigate(LOGIN_PATH);
      }
    } catch (error) {
      console.error("Failed to refresh access token", error);
      setAccessToken(null);
      setRefreshToken(null);
      navigate(LOGIN_PATH);
    }
  };

  let isRefreshing = false;

  let refreshSubscribers = [];

  const subscribeTokenRefresh = (callback) => {
    refreshSubscribers.push(callback);
  };
  const onRefreshed = (token) => {
    refreshSubscribers.forEach((callback) => callback(token));
    refreshSubscribers = [];
  };

  const validateJwtToken = async () => {
    let token = refToken.current;
    if (token && !validateToken(token)) {
      if (isRefreshing) {
        return false;
      }
      isRefreshing = true;
      token = await refreshAccessToken();
      if (token) {
        onRefreshed(token);
      }
    }
    return true;
  };

  useEffect(() => {
    if (!accessToken) {
      clearLocalStorage();
      refToken.current = null;
      if (!matchPath(location.pathname, PUBLIC_PATHS)) {
        navigate(LOGIN_PATH);
      }
    }
  }, [accessToken]);

  return (
    <AuthContext.Provider
      value={{
        isLoggedIn,
        setAccessToken,
        accessToken,
        setAuthData,
        userID,
        userRoles,
        refToken,
        validateJwtToken,
        subscribeTokenRefresh,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};
