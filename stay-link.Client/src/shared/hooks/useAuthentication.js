import { useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
import {
  BEARER_TOKEN_PREFIX,
  LOGIN_API_URL,
  LOGOUT_API_URL,
  REGISTER_API_URL
} from '../constants/apiConstants';

export const useAuthentication = () => {
  const { accessToken, isLoggedIn, setAccessToken, setAuthData } = useContext(
    AuthContext
  ) ;

  const login = async (data) => {
    const response= await fetch(LOGIN_API_URL, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    });

    if (!response.ok) {
      const errorResponse = await response.json();
      throw new Error(JSON.stringify(errorResponse));
    }

    const loginResponse = await response.json();

    console.log(loginResponse);

    setAuthData(loginResponse.accessToken);

    return true;
  };

  const logout = async ()=> {
    try {
      const response= await fetch(LOGOUT_API_URL, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `${BEARER_TOKEN_PREFIX}${accessToken}`
        }
      });

      if (!response.ok) {
        throw new Error(`Logout failed with status ${response.status}`);
      }

      setAccessToken(null);

      return true;
    } catch (error) {
      console.error('Error during logout:', error);
      return false;
    }
  };

  const registerUser = async (data) => {
    try {
      const response = await fetch(REGISTER_API_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
      });

      if (!response.ok) {
        const errorResponse = await response.json();
        throw new Error(`Registration failed. ${errorResponse.detail}`);
      }

      return true;
    } catch (error) {
      throw new Error(error instanceof Error ? error.message : 'Unknown error');
    }
  };

  return { login, logout, registerUser, isLoggedIn, accessToken };
};
