async function fetchBookings() {
    const response = await fetch('/api/Bookings');
    return response.json();
}

const addBooking = async (booking) => {
    const response = await fetch('/api/Bookings', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(booking)
    });

    const data = await response.json();
    return {
        status: response.status,
        data: data
    };
};


export { fetchBookings, addBooking };