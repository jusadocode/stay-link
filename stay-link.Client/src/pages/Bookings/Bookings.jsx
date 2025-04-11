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
import useBookings from "../../shared/hooks/useBookings";
import dayjs from "dayjs";
import { AuthContext } from "../../shared/context/AuthContext";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import LoadingIndicator from "../../shared/components/LoadingIndicator";
import React from "react";
import useRooms from "../../shared/hooks/useRooms";

function BookingsPage() {
  const [bookings, setBookings] = useState([]);
  const [selectedBooking, setSelectedBooking] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const { userIsAdmin } = useContext(AuthContext);

  const { fetchBookings, deleteBooking } = useBookings();
  const { fetchRoom } = useRooms();
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

  async function populateBookingData() {
    try {
      const bookingsData = await fetchBookings();

      const bookingsWithDetails = await Promise.all(
        bookingsData.map(async (booking) => {
          const rooms = await Promise.all(
            booking.roomIds.map((id) => fetchRoom(id))
          );

          const checkIn = dayjs(booking.checkInDate);
          const checkOut = dayjs(booking.checkOutDate);
          const nights = checkOut.diff(checkIn, "day");

          const roomCost = rooms.reduce(
            (sum, room) => sum + room.price * nights,
            0
          );
          const breakfastCost = booking.breakfastRequests * 15 * nights;
          const cleaningFee = 20;
          const total = roomCost + breakfastCost + cleaningFee;

          return {
            ...booking,
            rooms,
            totalPrice: total,
            checkIn,
            checkOut,
          };
        })
      );

      setBookings(bookingsWithDetails);
    } catch (error) {
      console.error("Error fetching booking data:", error);
    }
  }

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
            {bookings.map((booking, index) => {
              const { checkIn, checkOut, totalPrice, rooms } = booking;
              const now = dayjs();
              const canCancel = checkIn.diff(now, "hour") > 24;
              const canCheckOut = !canCancel && checkOut.diff(now, "hour") > 0;
              const totalGuests = rooms.reduce(
                (acc, r) => acc + r.maxOccupancy,
                0
              );
              const nights = checkOut.diff(checkIn, "day");
              const previewImage = rooms[0]?.imageUrl;
              return (
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
                      src={previewImage}
                      alt={rooms[0].title}
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
                        <strong>Nights:</strong> {nights}
                      </Typography>

                      <Box sx={{ mt: 1 }}>
                        <Typography variant="subtitle1" sx={{ mb: 1 }}>
                          <strong>Booked Rooms:</strong>
                        </Typography>

                        {rooms.map((room, i) => (
                          <Box
                            key={room.id || i}
                            sx={{
                              borderBottom: "1px solid #e0e0e0",
                              pb: 1,
                              mb: 1,
                            }}
                          >
                            <Typography
                              variant="body1"
                              sx={{ fontWeight: "bold" }}
                            >
                              {room.title}
                            </Typography>
                            <Typography variant="body2">
                              Type: {room.roomType}
                            </Typography>
                            <Typography variant="body2">
                              Max Occupancy: {room.maxOccupancy}
                            </Typography>
                            <Typography variant="body2">
                              Price per night: €{room.price}
                            </Typography>
                          </Box>
                        ))}
                      </Box>
                      <Typography>
                        <strong>Total guests expected:</strong> {totalGuests}
                      </Typography>
                      <Typography>
                        <strong>Total price:</strong> €{totalPrice}
                      </Typography>
                    </Grid>
                    {userIsAdmin() && (
                      <Button
                        variant="outlined"
                        color="error"
                        onClick={() => handleOpenModal(booking)}
                        sx={{ mt: 2 }}
                        disabled={!canCancel}
                      >
                        <DeleteForeverIcon sx={{ mr: 1 }} />
                        {canCancel
                          ? "Cancel Booking"
                          : "Cancellation Unavailable"}
                      </Button>
                    )}

                    <Typography
                      variant="body2"
                      sx={{ mt: 1 }}
                      color="text.secondary"
                    >
                      {canCancel
                        ? `You can cancel until ${checkIn
                            .subtract(24, "hour")
                            .format("DD MMM HH:mm")}`
                        : ""}
                    </Typography>

                    {canCancel ? (
                      <Button variant="outlined" sx={{ mt: 1 }}>
                        Cancel
                      </Button>
                    ) : (
                      ""
                    )}

                    <Button variant="outlined" sx={{ mt: 1 }}>
                      {canCheckOut ? "Check out" : "Check In"}
                    </Button>
                  </Box>
                </Paper>
              );
            })}
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
}

export default BookingsPage;
