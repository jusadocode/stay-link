import React, { useEffect, useState } from "react";
import {
  Container,
  Typography,
  TextField,
  MenuItem,
  Button,
  CircularProgress,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import { LocalizationProvider, DatePicker } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import dayjs from "dayjs";
import useRooms from "../../../../shared/hooks/useRooms";

export default function RoomClosureCreatePage() {
  const navigate = useNavigate();
  const { fetchRooms, getRoomAvailability, createRoomClosure } = useRooms();

  const [rooms, setRooms] = useState([]);
  const [availableRoomIds, setAvailableRoomIds] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  const [closure, setClosure] = useState({
    reason: "",
    startDate: dayjs().add(1, "day").format("YYYY-MM-DD"),
    endDate: dayjs().add(2, "day").format("YYYY-MM-DD"),
    roomId: "",
  });

  useEffect(() => {
    const load = async () => {
      const r = await fetchRooms();
      setRooms(r);
      await checkAvailability(closure.startDate, closure.endDate);
      setIsLoading(false);
    };
    load();
  }, []);

  const checkAvailability = async (start, end) => {
    if (!start || !end) return;
    try {
      const ids = await getRoomAvailability(start, end);
      setAvailableRoomIds(ids);
    } catch (err) {
      console.error("Failed to check room availability", err);
    }
  };

  const handleChange = (field) => (e) => {
    const value = e.target.value;
    const updated = { ...closure, [field]: value };
    setClosure(updated);

    if (field === "startDate" || field === "endDate") {
      checkAvailability(
        field === "startDate" ? value : updated.startDate,
        field === "endDate" ? value : updated.endDate
      );
    }
  };

  const handleSubmit = async () => {
    try {
      await createRoomClosure(closure);
      navigate("/bookings/calendar");
    } catch (error) {
      console.error("Error submitting closure:", error);
    }
  };

  const availableRooms = rooms.filter((room) =>
    availableRoomIds.includes(room.id)
  );

  if (isLoading) return <CircularProgress />;

  return (
    <Container maxWidth="sm" sx={{ mt: 4 }}>
      <Typography variant="h5" gutterBottom>
        Create Room Closure
      </Typography>

      <TextField
        fullWidth
        label="Reason for Closure"
        value={closure.reason}
        onChange={handleChange("reason")}
        sx={{ mb: 2 }}
      />

      <LocalizationProvider dateAdapter={AdapterDayjs}>
        <DatePicker
          label="Start Date"
          value={dayjs(closure.startDate)}
          onChange={(newValue) => {
            if (newValue) {
              handleChange("startDate")({
                target: { value: newValue.format("YYYY-MM-DD") },
              });
            }
          }}
          sx={{ mb: 2, width: "100%" }}
        />
        <DatePicker
          label="End Date"
          value={dayjs(closure.endDate)}
          onChange={(newValue) => {
            if (newValue) {
              handleChange("endDate")({
                target: { value: newValue.format("YYYY-MM-DD") },
              });
            }
          }}
          sx={{ mb: 2, width: "100%" }}
        />
      </LocalizationProvider>

      <TextField
        select
        fullWidth
        label="Select Room"
        value={closure.roomId}
        onChange={handleChange("roomId")}
        sx={{ mb: 3 }}
      >
        {availableRooms.map((room) => (
          <MenuItem key={room.id} value={room.id}>
            {room.title} – {room.roomType}
          </MenuItem>
        ))}
      </TextField>

      <Button variant="contained" color="primary" onClick={handleSubmit}>
        Submit Closure
      </Button>
    </Container>
  );
}
