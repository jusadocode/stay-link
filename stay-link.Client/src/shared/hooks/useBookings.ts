import { useAuthentication } from "./useAuthentication";
import { API_BOOKINGS_URL } from "../constants/apiConstants";

const useBookings = () => {
  const { customFetch } = useAuthentication();

  const fetchBookings = async () => {
    const response = await customFetch(API_BOOKINGS_URL, {
      method: "GET",
    });
    return response.json();
  };

  const addBooking = async (booking) => {
    const response = await customFetch(API_BOOKINGS_URL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(booking),
    });

    const data = await response.json();
    return data;
  };

  const deleteBooking = async (bookingId: number) => {
    const response = await customFetch(API_BOOKINGS_URL + `/${bookingId}`, {
      method: "DELETE",
      headers: { "Content-Type": "application/json" },
    });

    return response;
  };

  return {
    addBooking,
    fetchBookings,
    deleteBooking,
  };
};

export default useBookings;
