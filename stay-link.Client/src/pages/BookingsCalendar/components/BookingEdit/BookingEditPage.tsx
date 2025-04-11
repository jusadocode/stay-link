import { useParams, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import {
  Container,
  Typography,
  TextField,
  Button,
  MenuItem,
  CircularProgress,
} from "@mui/material";
import React from "react";
import useBookings from "../../../../shared/hooks/useBookings";
import useRooms from "../../../../shared/hooks/useRooms";
import { LocalizationProvider, DatePicker } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import dayjs from "dayjs";

export default function BookingEditPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const { fetchBooking, updateBooking } = useBookings();
  const { fetchRooms, getRoomAvailability } = useRooms();

  const [booking, setBooking] = useState(null);
  const [rooms, setRooms] = useState([]);
  const [availableRoomIds, setAvailableRoomIds] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const load = async () => {
      const b = await fetchBooking(id);
      const r = await fetchRooms();
      setBooking(b);
      setRooms(r);

      await getAvailability(b.checkInDate, b.checkOutDate);

      setIsLoading(false);
    };

    load();
  }, [id]);

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
      navigate("/bookings/calendar");
    } catch (error) {
      console.error("Failed to update booking", error);
    }
  };

  const filteredRooms = rooms.filter(
    (r) => availableRoomIds.includes(r.id) || booking.roomIds.includes(r.id)
  );

  if (isLoading) return <CircularProgress />;

  return (
    <Container maxWidth="sm" sx={{ mt: 4 }}>
      <Typography variant="h5" gutterBottom>
        Edit Booking #{id}
      </Typography>

      <TextField
        fullWidth
        label="Group Name"
        value={booking.groupName}
        defaultValue={""}
        onChange={handleChange("groupName")}
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
        SelectProps={{
          multiple: true,
        }}
        value={booking.roomIds}
        onChange={handleChange("roomIds")}
        sx={{ mb: 3 }}
      >
        {filteredRooms.map((room) => (
          <MenuItem key={room.id} value={room.id}>
            {room.title} – {room.roomType}
          </MenuItem>
        ))}
      </TextField>

      <Button variant="contained" onClick={handleSubmit}>
        Save Changes
      </Button>
    </Container>
  );
}
