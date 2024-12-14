export const HOME_PATH = '/';
export const LOGIN_PATH = '/login';
export const REGISTER_PATH = '/register';
export const FORGOT_PASSWORD_PATH = '/forgot-password';
export const MY_EVENTS_PATH = '/my-events';
export const SETTINGS_PATH = '/settings';
export const ADD_EVENT_PATH = MY_EVENTS_PATH + '/add-event';
export const EDIT_ROOM_PATH = 'rooms/edit/:id';
export const EDIT_EVENT_PATH_WITH_ID = (id) =>
  MY_EVENTS_PATH + `/edit-event/${id}`;
export const EVENT_DETAILS_PATH = '/event-details/:id';
export const PUBLIC_PATHS = [
  '/',
  '/login',
  '/register',
  '/event-details/*'
];
