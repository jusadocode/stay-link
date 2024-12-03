import { useEffect, useState } from "react";
import "../App.css";
import {
  Box,
  Button,
  Typography,
  Container,
  Paper,
  CircularProgress,
  Grid,
} from "@mui/material";
import { Link } from "react-router-dom";
import RoomTypes from "../data/roomTypes";
import { fetchBookings } from "../services/bookingService";

function BookingList() {
  const [bookings, setBookings] = useState();

  useEffect(() => {
    populateBookingData();
  }, []);

  return (
    <Container maxWidth="md" sx={{ mt: 4 }}>
      <Typography variant="h4" gutterBottom>
        My Bookings
      </Typography>
      {bookings ? (
        bookings.length > 0 ? (
          bookings.map((booking, index) => (
            <Paper key={index} elevation={3} sx={{ p: 2, mb: 2 }}>
              <Grid container spacing={5}>
                <Grid item xs={12} sm={5}>
                  <img
                    src={booking.hotel.imageUrl}
                    alt={booking.hotel.name}
                    style={{ width: "100%", height: "auto" }}
                  />
                </Grid>
                <Grid
                  item
                  xs={12}
                  sm={7}
                  display="flex"
                  flexDirection="column"
                  alignItems="flex-start"
                >
                  <Typography variant="h6" gutterBottom>
                    {booking.hotel.name}
                  </Typography>
                  <Typography>
                    <strong>Check-in Date:</strong>{" "}
                    {new Date(booking.checkInDate).toLocaleDateString()}
                  </Typography>
                  <Typography>
                    <strong>Check-out Date:</strong>{" "}
                    {new Date(booking.checkOutDate).toLocaleDateString()}
                  </Typography>
                  <Typography>
                    <strong>Description:</strong> {booking.room.summary}
                  </Typography>
                  <Typography>
                    <strong>Room type:</strong>{" "}
                    {RoomTypes[booking.room.roomType]}
                  </Typography>
                  <Typography>
                    <strong>Space:</strong>{" "}
                    {booking.room.numOfGuests === 1
                      ? "For 1 person"
                      : `For up to ${booking.room.numOfGuests} people`}
                  </Typography>
                  <Typography>
                    <strong>Total:</strong> €{booking.totalPrice}
                  </Typography>
                </Grid>
              </Grid>
            </Paper>
          ))
        ) : (
          <Typography variant="body1" sx={{ mt: 2 }}>
            You have not booked anything yet.
          </Typography>
        )
      ) : (
        <Box sx={{ mt: 4, textAlign: "center" }}>
          <Typography>Getting booking data...</Typography>
          <CircularProgress sx={{ mt: 2 }} />
        </Box>
      )}

      <Button variant="outlined" component={Link} to="/" sx={{ mt: 4 }}>
        Go Back to Hotel List
      </Button>
    </Container>
  );

  async function populateBookingData() {
    const data = await fetchBookings();
    setBookings(data);
  }
}

export default BookingList;
