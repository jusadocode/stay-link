import { useState } from 'react';

export const useErrorHandler = () => {
  const [error, setError] = useState(null);
  const handleError = (error) => {
    console.error('Error occurred:', error);
    if (error instanceof Error) {
      try {
        const errorDetail = JSON.parse(error.message);
        const errorMessage = `${errorDetail.title}: ${errorDetail.detail}`;
        setError(errorMessage);
      } catch (parseError) {
        console.error('Failed to parse error message:', parseError);
        setError('An unexpected error occurred.');
      }
    } else {
      console.error('Unknown error:', error);
      setError('An unknown error occurred.');
    }
  };
  return { error, handleError, clearError: () => setError(null) };
};
