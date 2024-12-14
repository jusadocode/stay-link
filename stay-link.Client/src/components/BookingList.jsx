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
import useBookings from "../shared/hooks/useBookings";

function BookingList() {
  const [bookings, setBookings] = useState();

  const { fetchBookings, fetchHotel, fetchRoom } = useBookings();
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
                    src="https://149345965.v2.pressablecdn.com/wp-content/uploads/img-hotels-IADGV_006-Dusk-Exterior-home.jpg"
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
                    <strong>Room type:</strong>{" "}
                    {RoomTypes[booking.room.roomType]}
                  </Typography>
                  <Typography>
                    <strong>Space:</strong>{" "}
                    {booking.room.maxOccupancy === 1
                      ? "For 1 person"
                      : `For up to ${booking.room.maxOccupancy} people`}
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
    try {
      const bookingsData = await fetchBookings();

      const bookingsWithDetails = await Promise.all(
        bookingsData.map(async (booking) => {
          const hotel = await fetchHotel(booking.hotelId); // Fetch hotel data
          const room = await fetchRoom(booking.roomId); // Fetch room data
          return {
            ...booking,
            hotel: hotel, // Add hotel data to the booking
            room: room, // Add room data to the booking
          };
        })
      );

      setBookings(bookingsWithDetails);
    } catch (error) {
      console.error("Error fetching booking data:", error);
    }
  }
}

export default BookingList;
