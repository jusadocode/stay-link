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

  const { fetchBookings } = useBookings();
  const { fetchRoom } = useRooms();

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

  useEffect(() => {
    populateBookingData();
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
      />
      <BookingGrid
        rooms={rooms}
        bookings={bookings}
        checkInDate={currentDate}
        numDays={numDays}
      />
      {/* Add other dashboard elements here */}
    </Container>
  );
}

export default BookingsCalendar;
