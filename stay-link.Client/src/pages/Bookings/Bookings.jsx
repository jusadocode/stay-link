import { useContext, useEffect, useState } from "react";
import {
  Box,
  Button,
  Typography,
  Container,
  Paper,
  Grid,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
} from "@mui/material";
import { Link } from "react-router-dom";
import RoomTypes from "../../data/roomTypes";
import useBookings from "../../shared/hooks/useBookings";
import dayjs from "dayjs";
import { AuthContext } from "../../shared/context/AuthContext";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import LoadingIndicator from "../../shared/components/LoadingIndicator";

function BookingsPage() {
  const [bookings, setBookings] = useState([]);
  const [selectedBooking, setSelectedBooking] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const { userIsAdmin } = useContext(AuthContext);

  const { fetchBookings, fetchRoom, deleteBooking } = useBookings();

  useEffect(() => {
    populateBookingData();
  }, []);

  const handleOpenModal = (booking) => {
    setSelectedBooking(booking);
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setSelectedBooking(null);
    setIsModalOpen(false);
  };

  const handleConfirmDelete = async () => {
    if (selectedBooking) {
      try {
        await deleteBooking(selectedBooking.id);
        setBookings((prevBookings) =>
          prevBookings.filter((booking) => booking.id !== selectedBooking.id)
        );
      } catch (error) {
        console.error("Error deleting booking:", error);
      } finally {
        handleCloseModal();
      }
    }
  };

  return (
    <Container maxWidth="md" sx={{ mt: 4 }}>
      <Typography variant="h4" gutterBottom>
        {userIsAdmin() ? "Manage bookings" : "My Bookings"}
      </Typography>

      {bookings ? (
        bookings.length > 0 ? (
          <Box
            sx={{
              display: "grid",
              gridTemplateColumns: { xs: "1fr", md: "repeat(3, 1fr)" },
              gap: 5,
            }}
          >
            {bookings.map((booking, index) => (
              <Paper
                key={index}
                elevation={3}
                sx={{
                  p: 2,
                  mb: 2,
                  maxWidth: "40rem",
                  width: "100%",
                  textAlign: "left",
                }}
              >
                <Box
                  sx={{
                    display: "flex",
                    flexDirection: "column",
                    justifyContent: "space-around",
                    height: "100%",
                  }}
                >
                  {/* Image Section */}
                  <img
                    src="https://images.unsplash.com/photo-1583847268964-b28dc8f51f92?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                    alt="Double room"
                    style={{
                      width: "100%",
                      height: "auto",
                    }}
                  />

                  {/* Information Section */}
                  <Grid
                    item
                    xs={12}
                    sx={{
                      display: "flex",
                      flexDirection: "column",
                      paddingTop: 2,
                    }}
                  >
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
                      {booking.room.maxOccupancy === 1
                        ? "For 1 person"
                        : `For up to ${3} people`}
                    </Typography>
                    <Typography>
                      <strong>Total:</strong> €300
                    </Typography>
                  </Grid>
                  {userIsAdmin() && (
                    <Button
                      variant="outlined"
                      color="error"
                      onClick={() => handleOpenModal(booking)}
                      sx={{ mt: 2 }}
                    >
                      <DeleteForeverIcon />
                      Delete Booking
                    </Button>
                  )}
                </Box>
              </Paper>
            ))}
          </Box>
        ) : (
          <Typography variant="body1" sx={{ mt: 2 }}>
            You have not booked anything yet.
          </Typography>
        )
      ) : (
        <Box sx={{ mt: 4, textAlign: "center" }}>
          <LoadingIndicator />
          <Typography>Getting booking data...</Typography>
        </Box>
      )}

      <Button variant="outlined" component={Link} to="/" sx={{ mt: 4 }}>
        Go Back to Room List
      </Button>

      {/* Delete Confirmation Modal */}
      <Dialog open={isModalOpen} onClose={handleCloseModal}>
        <DialogTitle>Confirm Deletion</DialogTitle>
        <DialogContent>
          <Typography>
            Are you sure you want to delete the booking for{" "}
            <strong>{selectedBooking?.hotel.name}</strong>?
          </Typography>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseModal} color="primary">
            Cancel
          </Button>
          <Button onClick={handleConfirmDelete} color="error">
            Confirm
          </Button>
        </DialogActions>
      </Dialog>
    </Container>
  );

  async function populateBookingData() {
    try {
      const bookingsData = await fetchBookings();

      const bookingsWithDetails = await Promise.all(
        bookingsData.map(async (booking) => {
          const room = await fetchRoom(booking.roomId);

          const checkInDate = dayjs(booking.checkInDate);
          const checkOutDate = dayjs(booking.checkOutDate);

          const numberOfNights = checkOutDate.diff(checkInDate, "day") + 1;
          const cleaningFee = 20;
          const breakfastDailyFee = 15;
          const totalBreakfast =
            booking.breakfastRequests * breakfastDailyFee * numberOfNights;
          const totalPrice =
            room.price * numberOfNights + totalBreakfast + cleaningFee;

          return {
            ...booking,
            totalPrice: totalPrice,
            room: room,
          };
        })
      );

      setBookings(bookingsWithDetails);
    } catch (error) {
      console.error("Error fetching booking data:", error);
    }
  }
}

export default BookingsPage;
