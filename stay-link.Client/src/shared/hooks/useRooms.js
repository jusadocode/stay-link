
import { useAuthentication } from "./useAuthentication";
import { API_BOOKINGS_URL, API_HOTELS_URL, API_ROOMS_FEATURES_URL, API_ROOMS_SEARCH_URL, API_ROOMS_URL } from "../constants/apiConstants";

const useBookings = () => {

const {customFetch} = useAuthentication();


    const fetchBookings = async () => {
        const response = await customFetch(API_BOOKINGS_URL, {
            method: 'GET'
        });
        return response.json();
    };

    // const fetchHotel = async (hotelId) => {
    //     const response = await customFetch(API_HOTELS_URL+`/${hotelId}`, {
    //         method: 'GET'
    //     });
    //     return response.json();
    // };

    const fetchRooms = async () => {
        const response = await customFetch(API_ROOMS_URL, {
            method: 'GET'
        });
        return await response.json();
    };


    const fetchFeatures = async () => {
        const response = await customFetch(API_ROOMS_FEATURES_URL, {
            method: 'GET'
        });
        return await response.json();
    };

    const searchRooms = async ({ checkIn, checkOut, guestCount, preferenceIds }) => {
        const params = new URLSearchParams({
          checkIn: checkIn.format("YYYY-MM-DD"),
          checkOut: checkOut.format("YYYY-MM-DD"),
          guestCount: guestCount.toString(),
        });
      
        preferenceIds.forEach((id) => params.append("preferenceIds", id.toString()));
      
        const response = await customFetch(`${API_ROOMS_URL}/filter?${params.toString()}`, {
          method: 'GET',
        });
      
        if (!response.ok) {
          throw new Error("Failed to fetch rooms");
        }
      
        return await response.json();
      };

    const fetchRoom = async (roomId) => {
        const response = await customFetch(API_ROOMS_URL+ `/${roomId}`, {
            method: 'GET'
        });
        return response.json();
    };

    const addBooking = async (booking) => {
        const response = await customFetch(API_BOOKINGS_URL, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(booking)
        });

        const data = await response.json();
        return data;
    };

    const updateRoom = async (room) => {
        const response = await customFetch(API_ROOMS_URL+`/${room.id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(room)
        });

        return response;
    };

    // async function fetchHotels() {
    //     const response = await fetch(API_HOTELS_URL);
    //     return response.json();
    // }
    
    async function fetchHotelRooms(hotelId) {
        const response = await fetch(API_HOTELS_URL + `/${hotelId}/Rooms`);
        return response.json();
    }

    const deleteBooking = async (bookingId) => {
        const response = await customFetch(API_BOOKINGS_URL+`/${bookingId}`, {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' }
        });

        return response;
    };

    return {
        addBooking,
        fetchBookings,
        fetchRooms,
        fetchRoom,
        searchRooms,
        updateRoom,
        fetchHotelRooms,
        fetchFeatures,
        deleteBooking
    };
};

export default useBookings;
