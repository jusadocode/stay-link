/* eslint-disable react/prop-types */

import { Box, Typography } from "@mui/material";
import RoomTypes from "../data/roomTypes";

export default function FinalStep({
  selectedRoom,
  bookingDates,
  breakfastRequests,
  expandedHotel,
}) {
  const numberOfNights = bookingDates[1].diff(bookingDates[0], "day") + 1;
  const cleaningFee = 20;
  const breakfastDailyFee = 15;
  const totalBreakfast = breakfastRequests * breakfastDailyFee * numberOfNights;
  const totalPrice =
    selectedRoom.price * numberOfNights + totalBreakfast + cleaningFee;

  if (!selectedRoom) {
    return (
      <Box>
        <Typography variant="h6">No room selected</Typography>
      </Box>
    );
  }

  return (
    <Box sx={{ mt: 2, mb: 1 }}>
      <Typography variant="h6">Incoming booking information</Typography>
      <Typography variant="body1">
        Make sure you check the details carefully before proceeding with the
        booking
      </Typography>
      <Box sx={{ mt: 2 }}>
        <Typography variant="body2">
          <strong>Hotel name:</strong> {expandedHotel.name}
        </Typography>
        <Typography variant="body2">
          <strong>Room type:</strong> {RoomTypes[selectedRoom.roomType]}
        </Typography>
        <Typography variant="body2">
          <strong>Price per night:</strong> €{selectedRoom.price}
        </Typography>
        <Typography variant="body2">
          <strong>Check-in date:</strong>{" "}
          {bookingDates[0].format("MMMM D, YYYY")}
        </Typography>
        <Typography variant="body2">
          <strong>Check-out date:</strong>{" "}
          {bookingDates[1].format("MMMM D, YYYY")}
        </Typography>
        <Typography variant="body2">
          <strong>Breakfast requests:</strong> {breakfastRequests}
        </Typography>
        <Typography variant="body2">
          <strong>Number of guests:</strong> {selectedRoom.maxOccupancy}
        </Typography>

        <Box sx={{ textAlign: "right", mt: 2 }}>
          <Typography variant="body2">
            Number of nights: {numberOfNights}
          </Typography>
          {breakfastRequests > 0 && (
            <Typography variant="body2">
              Breakfast for: {breakfastRequests}{" "}
              {breakfastRequests === 1 ? "person" : "people"} (15€ per person
              per day)
            </Typography>
          )}
          <Typography variant="body2">Cleaning fee: €{cleaningFee}</Typography>
          <Typography variant="body4">
            <strong>Total:</strong> €{totalPrice}
          </Typography>
        </Box>
      </Box>
    </Box>
  );
}
