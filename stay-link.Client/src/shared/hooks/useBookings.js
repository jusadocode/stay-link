
import { useAuthentication } from "./useAuthentication";


const useBookings = () => {

const {customFetch} = useAuthentication();


    const fetchBookings = async () => {
        const response = await customFetch('/api/Bookings', {
            method: 'GET'
        });
        return response.json();
    };

    const fetchHotel = async (hotelId) => {
        const response = await customFetch(`/api/Hotels/${hotelId}`, {
            method: 'GET'
        });
        return response.json();
    };

    const fetchRoom = async (roomId) => {
        const response = await customFetch(`/api/Rooms/${roomId}`, {
            method: 'GET'
        });
        return response.json();
    };

    const addBooking = async (booking) => {
        const response = await customFetch('/api/Bookings', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(booking)
        });

        const data = await response.json();
        return data;
    };

    return {
        addBooking,
        fetchBookings,
        fetchHotel,
        fetchRoom
    };
};

export default useBookings;
