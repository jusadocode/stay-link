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
import { useNavigate } from "react-router-dom";

export default function BookingAdditionPage() {
  const navigate = useNavigate();
  const { addBooking } = useBookings();
  const { fetchRooms, getRoomAvailability } = useRooms();

  const [rooms, setRooms] = useState([]);
  const [availableRoomIds, setAvailableRoomIds] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  const [booking, setBooking] = useState({
    displayName: "",
    breakfastRequests: 0,
    checkInDate: dayjs().add(1, "day").format("YYYY-MM-DD"),
    checkOutDate: dayjs().add(2, "day").format("YYYY-MM-DD"),
    roomIds: [],
  });

  useEffect(() => {
    const load = async () => {
      const r = await fetchRooms();
      setRooms(r);

      await getAvailability(booking.checkInDate, booking.checkOutDate);

      setIsLoading(false);
    };

    load();
  }, []);

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
      await addBooking(booking);
      navigate("/bookings/calendar");
    } catch (error) {
      console.error("Failed to create booking", error);
    }
  };

  const filteredRooms = rooms.filter((r) => availableRoomIds.includes(r.id));

  if (isLoading) return <CircularProgress />;

  return (
    <Container maxWidth="sm" sx={{ mt: 4 }}>
      <Typography variant="h5" gutterBottom>
        Create Booking
      </Typography>

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
        sx={{ mb: 3 }}
      >
        {filteredRooms.map((room) => (
          <MenuItem key={room.id} value={room.id}>
            {room.title} – {room.roomType}
          </MenuItem>
        ))}
      </TextField>

      <Button variant="contained" onClick={handleSubmit}>
        Create Booking
      </Button>
    </Container>
  );
}
