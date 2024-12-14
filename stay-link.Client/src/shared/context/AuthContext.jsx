import React, { createContext, useState, useEffect } from "react";
import { parseJWTToken } from "../utils/parseJWTTokenUtil";
import { CommitSharp } from "@mui/icons-material";

export const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
  const [userID, setUserID] = useState(() => {
    const userIDString = localStorage.getItem("userId");
    return userIDString ? userIDString : null;
  });

  const [userRoles, setUserRoles] = useState(() => {
    const rolesString = localStorage.getItem("userRoles");
    return rolesString ? JSON.parse(rolesString) : null;
  });

  const isLoggedIn = userID !== null;

  const setAuthData = (userInfo) => {
    if (userInfo) {
      setUserID(userInfo.userId);
      setUserRoles(userInfo.roles);

      localStorage.setItem("userId", userInfo.userId.toString());
      localStorage.setItem("userRoles", JSON.stringify(userInfo.roles));
    }
  };

  const clearUserData = () => {
    setUserID(null);
    setUserRoles(null);

    localStorage.removeItem("userId");
    localStorage.removeItem("userRoles");
  };

  return (
    <AuthContext.Provider
      value={{
        isLoggedIn,
        setAuthData,
        userID,
        userRoles,
        clearUserData,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};
