import { useContext } from "react";
import {
  BEARER_TOKEN_PREFIX,
  GET_USER_ATTENDANCE_URL,
} from "../constants/apiConstants";
import { AuthContext } from "../context/AuthContext";
import {
  UserAttendanceContext,
} from "../context/UserAttendanceContext";
import { useCustomFetch } from "./useCustomFetch";

export const useAttendance = () => {
  const { accessToken } = useContext(AuthContext);
  const { customFetch } = useCustomFetch();

  const { setUserAttendance, setUserAttendanceByEventId } = useContext(
    UserAttendanceContext
  );

  const getUserEventAttendance = async () => {
    const response = await customFetch(GET_USER_ATTENDANCE_URL, {
      headers: {
        Authorization: BEARER_TOKEN_PREFIX + `${accessToken}`,
      },
    });
    return await response.json();
  };

  const loadUserAttendance = async () => {
    const userAttendanceData =
      await getUserEventAttendance();

    setUserAttendance(userAttendanceData);
  };

  return {
    getUserEventAttendance,
    loadUserAttendance,
  };
};
