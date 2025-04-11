import { useAuthentication } from "./useAuthentication";
import {
  API_MULTIPLE_ROOMS_URL,
  API_ROOMS_AVAILABILITY_URL,
  API_ROOMS_FEATURES_URL,
  API_ROOMS_SEARCH_URL,
  API_ROOMS_URL,
  API_ROOMS_USAGES_URL,
} from "../constants/apiConstants";

const useRooms = () => {
  const { customFetch } = useAuthentication();

  const fetchRooms = async () => {
    const response = await customFetch(API_ROOMS_URL, {
      method: "GET",
    });
    return await response.json();
  };

  const getRoomAvailability = async (checkIn, checkOut) => {
    const response = await customFetch(
      `${API_ROOMS_AVAILABILITY_URL}?checkIn=${checkIn}&checkOut=${checkOut}`,
      {
        method: "GET",
      }
    );
    return await response.json();
  };

  const fetchRoomsUsages = async () => {
    const response = await customFetch(API_ROOMS_USAGES_URL, {
      method: "GET",
    });
    return await response.json();
  };

  const fetchFeatures = async () => {
    const response = await customFetch(API_ROOMS_FEATURES_URL, {
      method: "GET",
    });
    return await response.json();
  };

  const searchRooms = async ({
    checkIn,
    checkOut,
    guestCount,
    preferenceIds,
  }) => {
    const params = new URLSearchParams({
      checkIn: checkIn.format("YYYY-MM-DD"),
      checkOut: checkOut.format("YYYY-MM-DD"),
      guestCount: guestCount.toString(),
    });

    preferenceIds.forEach((id) =>
      params.append("preferenceIds", id.toString())
    );

    const response = await customFetch(
      `${API_ROOMS_SEARCH_URL}?${params.toString()}`,
      {
        method: "GET",
      }
    );

    if (!response.ok) {
      throw new Error("Failed to fetch rooms");
    }

    return await response.json();
  };

  const searchRoomGroups = async ({
    checkIn,
    checkOut,
    guestCount,
    preferenceIds,
  }) => {
    const params = new URLSearchParams({
      checkIn: checkIn.format("YYYY-MM-DD"),
      checkOut: checkOut.format("YYYY-MM-DD"),
      guestCount: guestCount.toString(),
    });

    preferenceIds.forEach((id) =>
      params.append("preferenceIds", id.toString())
    );

    const response = await customFetch(
      `${API_ROOMS_URL}/filter/group?${params.toString()}`,
      {
        method: "GET",
      }
    );

    if (!response.ok) {
      throw new Error("Failed to fetch room groups");
    }

    return await response.json();
  };

  const fetchRoom = async (roomId) => {
    const response = await customFetch(API_ROOMS_URL + `/${roomId}`, {
      method: "GET",
    });
    return response.json();
  };
  const addRoom = async (roomData) => {
    const response = await customFetch(API_ROOMS_URL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(roomData),
    });

    return await response.json();
  };

  const addRooms = async (
    roomData: RoomCreationRequest,
    roomQuantity: number
  ) => {
    const response = await customFetch(
      `${API_MULTIPLE_ROOMS_URL}/?numOfRooms=${roomQuantity}`,
      {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(roomData),
      }
    );

    return await response.json();
  };

  const updateRoom = async (room) => {
    const response = await customFetch(API_ROOMS_URL + `/${room.id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(room),
    });

    return response;
  };

  const deleteRoom = async (roomId) => {
    const response = await customFetch(API_ROOMS_URL + `/${roomId}`, {
      method: "DELETE",
      headers: { "Content-Type": "application/json" },
    });

    return response;
  };

  return {
    fetchRooms,
    fetchRoomsUsages,
    fetchRoom,
    getRoomAvailability,
    deleteRoom,
    searchRooms,
    searchRoomGroups,
    updateRoom,
    fetchFeatures,
    addRoom,
    addRooms,
  };
};

export default useRooms;
