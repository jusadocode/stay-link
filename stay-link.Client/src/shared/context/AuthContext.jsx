import React, { createContext, useState, useEffect } from "react";
import { parseJWTToken } from "../utils/parseJWTTokenUtil";

export const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
  const [accessToken, setAccessToken] = useState(() => {
    return localStorage.getItem("accessToken");
  });

  const [userID, setUserID] = useState(() => {
    const userIDString = localStorage.getItem("userID");
    return userIDString ? parseInt(userIDString, 10) : null;
  });

  const [userRoles, setUserRoles] = useState(() => {
    const rolesString = localStorage.getItem("userRoles");
    return rolesString ? JSON.parse(rolesString) : null;
  });

  const isLoggedIn = () => accessToken !== null;

  const setAuthData = (token) => {
    const userInfo = parseJWTToken(token);

    if (userInfo) {
      setAccessToken(token);
      setUserID(userInfo.userID);
      setUserRoles(userInfo.userRoles);

      localStorage.setItem("accessToken", token);
      localStorage.setItem("userID", userInfo.userID.toString());
      localStorage.setItem("userRoles", JSON.stringify(userInfo.userRoles));
    }
  };

  const manageLocalStorage = () => {
    if (accessToken) {
      localStorage.setItem("accessToken", accessToken);
    } else {
      localStorage.removeItem("accessToken");
      localStorage.removeItem("userID");
      localStorage.removeItem("userRoles");
    }
  };

  useEffect(() => {
    manageLocalStorage();
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
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};
