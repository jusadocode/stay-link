import { useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
import {
  LOGIN_API_URL,
  LOGOUT_API_URL,
  REGISTER_API_URL,
  REFRESH_TOKEN_URL
} from '../constants/apiConstants';
import { LOGIN_PATH } from '../constants/routes';
import { useNavigate} from 'react-router-dom';

export const useAuthentication = () => {
  const { isLoggedIn, setAuthData, clearUserData } = useContext(
    AuthContext
  ) ;

  const navigate = useNavigate();

  const login = async (data) => {
    const response= await fetch(LOGIN_API_URL, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data),
      credentials: 'include'
    });

    if (!response.ok) {
      const errorMessage = await response.text();
      throw new Error(errorMessage);
    }

    const userData = await response.json();

    console.log(userData);

    setAuthData(userData)

    return true;
  };

  const logout = async () => {
    try {
      const response = await fetch(LOGOUT_API_URL, {
        method: 'POST',
        credentials: 'include'
      });

      if (!response.ok) {
        throw new Error(`Logout failed with status ${response.status}`);
      }

      clearUserData();

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
        body: JSON.stringify(data),
        credentials: 'include'
      });

      if (!response.ok) {
        const errorResponse = await response.json();
        throw new Error(`Registration failed. ${errorResponse}`);
      }

      return true;
    } catch (error) {
      throw new Error(error instanceof Error ? error.message : 'Unknown error');
    }
  };

  const customFetch = async (
    url,
    options = {}
  ) => {
    
    try {
      const response = await fetch(url, {
        ...options,
        credentials: 'include', 
      });
  
      if (response.ok)
        return response; 
      
      if (response.status === 401) {
        console.log('Access token expired. Attempting to refresh...');
        const refreshResponse = await fetch(REFRESH_TOKEN_URL, {
          method: 'POST',
          credentials: 'include', 
        });
  
        if (refreshResponse.ok) {
          console.log('Token refreshed. Retrying original request...');
          return fetch(url, {
            ...options,
            credentials: 'include',
          });
        }
  
        console.error('Failed to refresh token. Logging out...');
        clearUserData();
        navigate(LOGIN_PATH);

        throw new Error('Unauthorized');
      }
  
      return response;
    } catch (error) {
      console.error('Error occurred during fetchWithRefresh:', error);
      throw error;
    }
};

  return { login, logout, registerUser, customFetch, isLoggedIn};
};
