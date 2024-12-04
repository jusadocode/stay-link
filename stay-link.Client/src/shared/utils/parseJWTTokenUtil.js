import {jwtDecode} from 'jwt-decode';

export const parseJWTToken = (token) => {
  try {
    const decodedPayload = jwtDecode(token);

    console.log(decodedPayload);

    const userID = decodedPayload.sub; 
    const roles = decodedPayload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    return {
      userID: userID,
      userRoles: Array.isArray(roles) ? roles : [roles]
    };
  } catch (error) {
    console.error('Error during JWT Token parsing: ', error);
    return null; 
  }
};