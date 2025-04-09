import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import {
  TextField,
  Button,
  Box,
  Typography,
  CircularProgress,
} from "@mui/material";
import { Select, MenuItem, InputLabel, FormControl } from "@mui/material";
import { useNavigate } from "react-router-dom";
import React from "react";
import useRooms from "../../shared/hooks/useRooms";
import { roomTypeOptions } from "../../shared/utils/roomTypeUtils";
const RoomEditPage = () => {
  const { id } = useParams();

  const navigate = useNavigate();

  const { fetchRoom, updateRoom } = useRooms();
  const [room, setRoom] = useState(null);
  const [loading, setLoading] = useState(true);
  const [updateLoading, setUpdateLoading] = useState(false);

  const [roomFeatures, setRoomFeatures] = useState([]);
  const { fetchFeatures } = useRooms();

  const loadRoomInfo = async () => {
    try {
      const roomData = await fetchRoom(id);
      setRoom(roomData);
    } catch (error) {
      console.error("Failed to load room or hotel data:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    const loadFeatures = async () => {
      try {
        const features = await fetchFeatures();
        setRoomFeatures(features);
      } catch (err) {
        console.error("Failed to load room features", err);
      }
    };

    loadFeatures();
    loadRoomInfo();
  }, [id]);

  const handleRoomChange = (field, value) => {
    setRoom((prev) => ({
      ...prev,
      [field]: value,
    }));
  };

  const handleUpdate = async () => {
    try {
      setUpdateLoading(true);
      const response = await updateRoom(room);
      if (response.ok) {
        navigate("/");
      }
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

  if (!room) {
    return (
      <Box sx={{ mt: 5, textAlign: "center" }}>
        <Typography variant="h6">Room not found.</Typography>
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
      ></Box>

      <Box sx={{ mb: 3 }}>
        <Typography variant="h6" gutterBottom>
          Room Description
        </Typography>
        <TextField
          label="Room Title"
          fullWidth
          sx={{ mb: 2 }}
          value={room.title || ""}
          onChange={(e) => handleRoomChange("title", e.target.value)}
        />
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
            value={room.roomType}
            onChange={(e) => handleRoomChange("roomType", e.target.value)}
            label="Room Type"
          >
            {roomTypeOptions.map((type) => (
              <MenuItem key={type.value} value={type.value}>
                {type.label}
              </MenuItem>
            ))}
          </Select>
        </FormControl>

        <FormControl fullWidth sx={{ mb: 2 }}>
          <InputLabel id="features-label">Room Features</InputLabel>
          <Select
            labelId="features-label"
            id="features-select"
            multiple
            value={room.featureIds || []}
            onChange={(e) =>
              handleRoomChange(
                "featureIds",
                typeof e.target.value === "string"
                  ? e.target.value.split(",")
                  : e.target.value
              )
            }
            renderValue={(selected) => {
              const selectedNames = roomFeatures
                ?.filter((f) => selected.includes(f.id))
                .map((f) => f.name)
                .join(", ");
              return selectedNames || "None";
            }}
          >
            {roomFeatures.map((feature) => (
              <MenuItem key={feature.id} value={feature.id}>
                <Box sx={{ display: "flex", alignItems: "center" }}>
                  <input
                    type="checkbox"
                    checked={room.featureIds?.includes(feature.id)}
                    readOnly
                    style={{ marginRight: "8px" }}
                  />
                  {feature.name}
                </Box>
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

        <TextField
          label="Image URL"
          fullWidth
          sx={{ mb: 2 }}
          value={room.imageUrl || ""}
          onChange={(e) => handleRoomChange("imageUrl", e.target.value)}
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
