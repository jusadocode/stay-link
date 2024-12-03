export const API_URL= import.meta.env.REACT_APP_API_URL;
export const BEARER_TOKEN_PREFIX = "Bearer ";
export const LOGIN_API_URL= API_URL + "/auth/login";
export const LOGOUT_API_URL= API_URL + "/auth/logout";
export const REGISTER_API_URL= API_URL + "/auth/register";
export const GET_USER_ATTENDANCE_URL  = `${API_URL}/v1/events/attendance`;
export const REFRESH_TOKEN_URL = `${API_URL}/v1/auth/refresh`;
