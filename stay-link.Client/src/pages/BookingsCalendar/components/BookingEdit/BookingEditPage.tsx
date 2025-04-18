import { useEffect, useState } from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Typography,
  TextField,
  MenuItem,
  Button,
  CircularProgress,
  Box,
} from "@mui/material";
import { LocalizationProvider, DatePicker } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import dayjs from "dayjs";
import DeleteIcon from "@mui/icons-material/Delete";
import SaveIcon from "@mui/icons-material/Save";
import useBookings from "../../../../shared/hooks/useBookings";
import useRooms from "../../../../shared/hooks/useRooms";
import React from "react";

export default function BookingEditDialog({
  open,
  onClose,
  bookingId,
  onSuccess,
}) {
  const { fetchBooking, updateBooking, deleteBooking } = useBookings();
  const { fetchRooms, getRoomAvailability } = useRooms();

  const [booking, setBooking] = useState(null);
  const [rooms, setRooms] = useState([]);
  const [availableRoomIds, setAvailableRoomIds] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    if (!open || !bookingId) return;

    const load = async () => {
      const b = await fetchBooking(bookingId);
      const r = await fetchRooms();
      setBooking(b);
      setRooms(r);

      await getAvailability(b.checkInDate, b.checkOutDate);

      setIsLoading(false);
    };

    load();
  }, [open, bookingId]);

  const getAvailability = async (checkIn, checkOut) => {
    if (!checkIn || !checkOut) return;
    try {
      const ids = await getRoomAvailability(checkIn, checkOut);
      setAvailableRoomIds(ids);
    } catch (err) {
      console.error("Failed to fetch availability", err);
    }
  };

  const handleChange = (field) => (e) => {
    const value = e.target.value;
    const updated = { ...booking, [field]: value };

    setBooking(updated);

    if (field === "checkInDate" || field === "checkOutDate") {
      getAvailability(
        field === "checkInDate" ? value : updated.checkInDate,
        field === "checkOutDate" ? value : updated.checkOutDate
      );
    }
  };

  const handleSubmit = async () => {
    try {
      await updateBooking(booking);
      onSuccess?.();
      onClose();
    } catch (error) {
      console.error("Failed to update booking", error);
    }
  };

  const handleDelete = async () => {
    if (window.confirm("Are you sure you want to delete this booking?")) {
      try {
        await deleteBooking(bookingId);
        onSuccess?.();
        onClose();
      } catch (error) {
        console.error("Failed to delete booking", error);
      }
    }
  };

  const filteredRooms = rooms.filter(
    (r) => availableRoomIds.includes(r.id) || booking?.roomIds?.includes(r.id)
  );

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogTitle>Edit Booking #{bookingId}</DialogTitle>
      {isLoading || !booking ? (
        <DialogContent>
          <CircularProgress />
        </DialogContent>
      ) : (
        <>
          <DialogContent dividers>
            <TextField
              fullWidth
              label="Display Name"
              value={booking.displayName}
              onChange={handleChange("displayName")}
              sx={{ mb: 2 }}
            />

            <TextField
              fullWidth
              type="number"
              label="Breakfast Requests"
              value={booking.breakfastRequests}
              onChange={handleChange("breakfastRequests")}
              sx={{ mb: 2 }}
            />

            <LocalizationProvider dateAdapter={AdapterDayjs}>
              <DatePicker
                label="Check-in"
                value={dayjs(booking.checkInDate)}
                onChange={(newValue) => {
                  if (newValue) {
                    handleChange("checkInDate")({
                      target: { value: newValue.format("YYYY-MM-DD") },
                    });
                  }
                }}
                sx={{ mb: 2, width: "100%" }}
              />
              <DatePicker
                label="Check-out"
                value={dayjs(booking.checkOutDate)}
                onChange={(newValue) => {
                  if (newValue) {
                    handleChange("checkOutDate")({
                      target: { value: newValue.format("YYYY-MM-DD") },
                    });
                  }
                }}
                sx={{ mb: 2, width: "100%" }}
              />
            </LocalizationProvider>

            <TextField
              fullWidth
              select
              label="Rooms"
              SelectProps={{ multiple: true }}
              value={booking.roomIds}
              onChange={handleChange("roomIds")}
              sx={{ mb: 2 }}
            >
              {filteredRooms.map((room) => (
                <MenuItem key={room.id} value={room.id}>
                  {room.title} – {room.roomType}
                </MenuItem>
              ))}
            </TextField>
          </DialogContent>

          <DialogActions>
            <Box
              display="flex"
              justifyContent="space-between"
              width="100%"
              px={2}
            >
              <Button
                variant="outlined"
                color="error"
                onClick={handleDelete}
                startIcon={<DeleteIcon />}
              >
                Delete
              </Button>

              <Button
                variant="contained"
                onClick={handleSubmit}
                startIcon={<SaveIcon />}
              >
                Save
              </Button>
            </Box>
          </DialogActions>
        </>
      )}
    </Dialog>
  );
}
