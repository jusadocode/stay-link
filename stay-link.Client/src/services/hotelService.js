async function fetchHotels() {
    const response = await fetch('/api/Hotels');
    return response.json();
}

async function fetchHotelRooms(hotelId) {
    const response = await fetch(`/api/Hotels/${hotelId}/Rooms`);
    return response.json();
}

export { fetchHotels, fetchHotelRooms};