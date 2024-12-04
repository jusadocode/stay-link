import { useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
import { BEARER_TOKEN_PREFIX } from '../constants/apiConstants';

const useBookings = () => {
    const { accessToken } = useContext(AuthContext); 

    const fetchBookings = async () => {
        const response = await fetch('/api/Bookings', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `${BEARER_TOKEN_PREFIX}${accessToken}`
            }
        });
        return response.json();
    };

    const fetchHotel = async (hotelId) => {
        const response = await fetch(`/api/Hotels/${hotelId}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `${BEARER_TOKEN_PREFIX}${accessToken}`
            }
        });
        return response.json();
    };

    const fetchRoom = async (roomId) => {
        const response = await fetch(`/api/Rooms/${roomId}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `${BEARER_TOKEN_PREFIX}${accessToken}`
            }
        });
        return response.json();
    };

    const addBooking = async (booking) => {
        const response = await fetch('/api/Bookings', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `${BEARER_TOKEN_PREFIX}${accessToken}`
            },
            body: JSON.stringify(booking)
        });

        const data = await response.json();
        return {
            status: response.status,
            data: data
        };
    };

    return {
        addBooking,
        fetchBookings,
        fetchHotel,
        fetchRoom
    };
};

export default useBookings;
