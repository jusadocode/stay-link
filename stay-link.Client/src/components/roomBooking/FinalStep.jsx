import { Box, Typography } from "@mui/material";
import React from "react";

export default function FinalStep({
  selectedRooms = [],
  bookingDates,
  breakfastRequests,
  groupName,
  isGroupBooking,
}) {
  const numberOfNights = bookingDates[1].diff(bookingDates[0], "day");
  const cleaningFee = 20;
  const breakfastDailyFee = 15;

  const totalBreakfast = breakfastRequests * breakfastDailyFee * numberOfNights;
  const roomSubtotal = selectedRooms.reduce(
    (acc, room) => acc + room.price * numberOfNights,
    0
  );
  const totalPrice = roomSubtotal + totalBreakfast + cleaningFee;
  const totalGuests = selectedRooms.reduce(
    (acc, room) => acc + room.maxOccupancy,
    0
  );

  if (!selectedRooms || selectedRooms.length === 0) {
    return (
      <Box>
        <Typography variant="h6">No rooms selected</Typography>
      </Box>
    );
  }

  return (
    <Box sx={{ mt: 2, mb: 1 }}>
      <Typography variant="h6">Review Your Booking</Typography>
      <Typography variant="body1" sx={{ mb: 2 }}>
        Please confirm the details before finalizing your booking.
      </Typography>

      {isGroupBooking && (
        <Typography variant="body2">
          <strong>Group:</strong> {groupName}
        </Typography>
      )}

      {selectedRooms.map((room, index) => (
        <Box key={room.id} sx={{ mb: 2, pl: 1, borderLeft: "4px solid #ccc" }}>
          <Typography variant="subtitle1">{room.title}</Typography>
          <Typography variant="body2">Room type: {room.roomType}</Typography>
          <Typography variant="body2">
            Price per night: €{room.price}
          </Typography>
          <Typography variant="body2">
            Max guests: {room.maxOccupancy}
          </Typography>
        </Box>
      ))}

      <Typography variant="body2">
        <strong>Check-in:</strong> {bookingDates[0].format("MMMM D, YYYY")}
      </Typography>
      <Typography variant="body2">
        <strong>Check-out:</strong> {bookingDates[1].format("MMMM D, YYYY")}
      </Typography>
      <Typography variant="body2">
        <strong>Total guests expected:</strong> {totalGuests}
      </Typography>
      <Typography variant="body2">
        <strong>Breakfasts requested:</strong> {breakfastRequests}
      </Typography>

      <Box sx={{ mt: 2, textAlign: "right" }}>
        <Typography variant="body2">Nights: {numberOfNights}</Typography>
        {breakfastRequests > 0 && (
          <Typography variant="body2">
            Breakfast: €{breakfastDailyFee} × {breakfastRequests} ×{" "}
            {numberOfNights} = €{totalBreakfast}
          </Typography>
        )}
        <Typography variant="body2">Cleaning fee: €{cleaningFee}</Typography>
        <Typography variant="body2" fontWeight="bold">
          Total: €{totalPrice}
        </Typography>
      </Box>
    </Box>
  );
}
