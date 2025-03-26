import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import useBookings from "../../shared/hooks/useBookings";
import {
  TextField,
  Button,
  Box,
  Typography,
  CircularProgress,
} from "@mui/material";
import RoomTypes from "../../data/roomTypes";
import { Select, MenuItem, InputLabel, FormControl } from "@mui/material";
import { useNavigate } from "react-router-dom";

const RoomEditPage = () => {
  const { id } = useParams();

  const navigate = useNavigate();

  const { fetchRoom, fetchHotel, updateRoom } = useBookings(); // Assuming you have an updateRoom method
  const [room, setRoom] = useState(null);
  const [hotel, setHotel] = useState(null);
  const [loading, setLoading] = useState(true); // Add loading state
  const [updateLoading, setUpdateLoading] = useState(false); // Add update button loading state

  // Load room and hotel information
  const loadRoomInfo = async () => {
    try {
      const roomData = await fetchRoom(id);
      setRoom(roomData);

      const hotelData = await fetchHotel(roomData.hotelID);
      setHotel(hotelData);
    } catch (error) {
      console.error("Failed to load room or hotel data:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadRoomInfo();
  }, [id]);

  // Handle changes in room data
  const handleRoomChange = (field, value) => {
    setRoom((prev) => ({
      ...prev,
      [field]: value,
    }));
  };

  // Handle changes in hotel data
  const handleHotelChange = (field, value) => {
    setHotel((prev) => ({
      ...prev,
      [field]: value,
    }));
  };

  // Handle form submission to update room info
  const handleUpdate = async () => {
    try {
      setUpdateLoading(true);
      const response = await updateRoom(room);
      if (response.ok) {
        navigate("/");
      }
      // Assuming the updateRoom function takes id and updated room object
    } catch (error) {
      console.error("Failed to update room:", error);
    } finally {
      setUpdateLoading(false);
    }
  };

  if (loading) {
    return (
      <Box sx={{ display: "flex", justifyContent: "center", mt: 5 }}>
        <CircularProgress />
      </Box>
    );
  }

  if (!room || !hotel) {
    return (
      <Box sx={{ mt: 5, textAlign: "center" }}>
        <Typography variant="h6">Room or Hotel not found.</Typography>
      </Box>
    );
  }

  return (
    <Box sx={{ maxWidth: 600, margin: "0 auto", mt: 5 }}>
      <Typography variant="h4" gutterBottom>
        Edit Room
      </Typography>

      <Box
        sx={{
          mb: 10,
          display: "flex",
          flexDirection: "column",
          gap: "2rem",
          alignItems: "center",
        }}
      >
        <Typography variant="h6" gutterBottom>
          Located in {hotel.name}, {hotel.address}
        </Typography>

        <Box
          component="img"
          src={hotel.imageUrl}
          alt={hotel.name}
          sx={{
            borderRadius: "0.5rem",
            width: "100%",
            height: "50%",
            objectFit: "cover",
            transition: "transform 1s ease-in-out", // Smooth transition
            "&:hover": {
              transform: "scale(1.2)", // Slight zoom-in effect
            },
          }}
        />
      </Box>

      <Box sx={{ mb: 3 }}>
        <Typography variant="h6" gutterBottom>
          Room Description
        </Typography>
        <TextField
          label="Room Summary"
          fullWidth
          sx={{ mb: 2 }}
          value={room.summary || ""}
          onChange={(e) => handleRoomChange("summary", e.target.value)}
        />
        <FormControl fullWidth sx={{ mb: 2 }}>
          <InputLabel id="room-type-label">Room Type</InputLabel>
          <Select
            labelId="room-type-label"
            id="room-type-select"
            value={room.roomType || 0}
            onChange={(e) => handleRoomChange("roomType", e.target.value)}
            label="Room Type"
          >
            {RoomTypes.map((type, index) => (
              <MenuItem key={index} value={index}>
                {type}
              </MenuItem>
            ))}
          </Select>
        </FormControl>

        <TextField
          label="Max Occupancy"
          fullWidth
          type="number"
          sx={{ mb: 2 }}
          value={room.maxOccupancy || ""}
          onChange={(e) => handleRoomChange("maxOccupancy", e.target.value)}
        />
        <TextField
          label="Price per Night"
          fullWidth
          type="number"
          sx={{ mb: 2 }}
          value={room.price || 0}
          onChange={(e) =>
            handleRoomChange("price", parseFloat(e.target.value))
          }
        />
      </Box>

      <Button
        variant="contained"
        color="primary"
        fullWidth
        onClick={handleUpdate}
        disabled={updateLoading}
      >
        {updateLoading ? "Updating Room..." : "Update"}
      </Button>
    </Box>
  );
};

export default RoomEditPage;
