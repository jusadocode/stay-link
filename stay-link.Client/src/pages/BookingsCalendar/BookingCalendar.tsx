import { useState, useEffect } from "react";
import { Typography, Container } from "@mui/material";
import { startOfDay } from "date-fns";
import useBookings from "../../shared/hooks/useBookings";
import Toolbar from "./components/Toolbar";
import BookingGrid from "./components/BookingGrid";
import React from "react";
import useRooms from "../../shared/hooks/useRooms";

function BookingsCalendar() {
  const [currentDate, setCurrentDate] = useState(startOfDay(new Date()));
  const [numDays, setNumDays] = useState(14);
  const [rooms, setRooms] = useState([]);
  const [bookings, setBookings] = useState([]);
  const [roomUsages, setRoomUsages] = useState([]);
  const [roomClosures, setRoomClosures] = useState([]);

  const [showBookings, setShowBookings] = useState(true);
  const [showHousekeeping, setShowHousekeeping] = useState(true);

  const { fetchBookings } = useBookings();
  const { fetchRoom, fetchRoomsUsages, getRoomClosures } = useRooms();

  async function populateBookingData() {
    try {
      const bookingsData = await fetchBookings();

      const roomIdSet = new Set();
      bookingsData.forEach((booking) =>
        booking.roomIds.forEach((roomId) => roomIdSet.add(roomId))
      );

      const roomFetchPromises = Array.from(roomIdSet).map((id: string) =>
        fetchRoom(id)
      );
      const roomsData = await Promise.all(roomFetchPromises);

      setBookings(bookingsData);
      setRooms(roomsData);
    } catch (error) {
      console.error("Error fetching booking data:", error);
    }
  }

  async function populateUsageData() {
    try {
      const usageData = await fetchRoomsUsages();
      const closureData = await getRoomClosures();
      setRoomUsages(usageData);
      setRoomClosures(closureData);
    } catch (error) {
      console.error("Error fetching usage or closure data:", error);
    }
  }

  useEffect(() => {
    populateBookingData();
    populateUsageData();
  }, []);

  return (
    <Container maxWidth="xl" sx={{ mt: 4 }}>
      <Typography variant="h4" gutterBottom>
        Booking Calendar
      </Typography>
      <Toolbar
        currentDate={currentDate}
        setCurrentDate={setCurrentDate}
        numDays={numDays}
        setNumDays={setNumDays}
        populateBookingData={populateBookingData}
        populateUsageData={populateUsageData}
        setShowBookings={setShowBookings}
        showBookings={showBookings}
        setShowHousekeeping={setShowHousekeeping}
        showHouseKeeping={showHousekeeping}
      />
      <BookingGrid
        rooms={rooms}
        roomUsages={roomUsages}
        roomClosures={roomClosures}
        bookings={bookings}
        checkInDate={currentDate}
        numDays={numDays}
        populateBookingData={populateBookingData}
        populateUsageData={populateUsageData}
        showBookings={showBookings}
        showHousekeeping={showHousekeeping}
      />
    </Container>
  );
}

export default BookingsCalendar;
