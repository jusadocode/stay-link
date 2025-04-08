import { RoomTypes } from "../constants/roomTypes";

export const roomTypeOptions = Object.entries(RoomTypes).map(
  ([key, value]) => ({
    label: key,
    value: value,
  })
);
