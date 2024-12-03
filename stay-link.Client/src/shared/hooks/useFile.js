import { useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
import {
  BEARER_TOKEN_PREFIX,
} from '../constants/apiConstants';
import { useCustomFetch } from './useCustomFetch';

export const useFile = () => {
  const { accessToken } = useContext(AuthContext);
  const { customFetch } = useCustomFetch();

  const uploadFile = async (file) => {
    const formData = new FormData();
    formData.append('file', file);

    const response = await customFetch(FILE_UPLOAD_API_URL, {
      method: 'POST',
      headers: { 'Authorization': `${BEARER_TOKEN_PREFIX}${accessToken}` },
      body: formData
    });

    if (!response.ok) {
      const errorResponse = await response.json();
      throw new Error(JSON.stringify(errorResponse));
    }

    return await response.json();
  };

  const uploadProfileImage = async (file) => {
    const FormData = new FormData();
    formData.append('file', file);

    const response = await fetch(PROFILE_IMG_UPLOAD_API_URL, {
      method: 'POST',
      body: formData
    });

    if (!response.ok) {
      const errorResponse = await response.json();
      throw new Error(JSON.stringify(errorResponse));
    }

    return await response.json();
  };

  return { uploadFile, uploadProfileImage };
};
