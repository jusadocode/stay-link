import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";

export const useCustomFetch = () => {
  const { refToken, validateJwtToken, subscribeTokenRefresh } = useContext(
    AuthContext
  );

  const customFetch = async (
    url,
    options = {}
  ) => {
    if (!(await validateJwtToken())) {
      return new Promise<Response>((resolve) => {
        subscribeTokenRefresh(async (newToken) => {
          // eslint-disable-next-line @typescript-eslint/no-explicit-any
          const updatedOptions = {
            ...options,
            headers: {
              ...options.headers,
              Authorization: `Bearer ${newToken}`,
            },
          };
          resolve(fetch(url, updatedOptions));
        });
      });
    }
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const updatedOptions = {
      ...options,
      headers: {
        ...options.headers,
        Authorization: `Bearer ${refToken.current}`,
      },
    };
    return fetch(url, updatedOptions);
  };

  return { customFetch };
};
