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

  const fetchBooking = async (bookingId) => {
    const response = await customFetch(`${API_BOOKINGS_URL}/${bookingId}`, {
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

  const updateBooking = async (booking) => {
    const response = await customFetch(`${API_BOOKINGS_URL}/${booking.id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(booking),
    });

    if (response.status === 204) return;
  };

  const deleteBooking = async (bookingId) => {
    const response = await customFetch(API_BOOKINGS_URL + `/${bookingId}`, {
      method: "DELETE",
      headers: { "Content-Type": "application/json" },
    });

    return response;
  };

  const cancelBooking = async (id) => {
    try {
      await customFetch(`/api/bookings/${id}/cancel`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
      });
    } catch (error) {
      console.error("Error cancelling booking:", error);
    }
  };

  const checkInBooking = async (id) => {
    try {
      await customFetch(`/api/bookings/${id}/check-in`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
      });
    } catch (error) {
      console.error("Error during check-in:", error);
    }
  };

  const checkOutBooking = async (id) => {
    try {
      await customFetch(`/api/bookings/${id}/check-out`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
      });
    } catch (error) {
      console.error("Error during check-out:", error);
    }
  };

  return {
    addBooking,
    fetchBookings,
    fetchBooking,
    deleteBooking,
    updateBooking,
    cancelBooking,
    checkInBooking,
    checkOutBooking,
  };
};

export default useBookings;
