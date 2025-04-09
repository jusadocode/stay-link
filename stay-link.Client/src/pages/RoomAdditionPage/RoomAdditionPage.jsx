import React, { useEffect, useState } from "react";
import {
  Box,
  Button,
  TextField,
  Typography,
  Select,
  MenuItem,
  InputLabel,
  FormControl,
  CircularProgress,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import { roomTypeOptions } from "../../shared/utils/roomTypeUtils";
import useRooms from "../../shared/hooks/useRooms";

const RoomAdditionPage = () => {
  const navigate = useNavigate();
  const { addRoom, addRooms, fetchFeatures } = useRooms();

  const [roomFeatures, setFeatures] = useState([]);
  const [roomQuantity, setRoomQuantity] = useState(1);

  const [newRoom, setNewRoom] = useState({
    title: "",
    summary: "",
    roomType: "",
    maxOccupancy: "",
    price: "",
    imageUrl: "",
    featureIds: [],
  });

  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleRoomChange = (field, value) => {
    setNewRoom((prev) => ({
      ...prev,
      [field]: value,
    }));
  };

  const handleQuantityChange = (event) => {
    if (event > 0) setRoomQuantity(event);
  };

  const handleFeatureToggle = (event) => {
    const {
      target: { value },
    } = event;

    setNewRoom((prev) => ({
      ...prev,
      featureIds: typeof value === "string" ? value.split(",") : value,
    }));
  };

  const handleCreateRoom = async () => {
    try {
      setIsSubmitting(true);

      const payload = {
        ...newRoom,
        price: parseFloat(newRoom.price),
        maxOccupancy: parseInt(newRoom.maxOccupancy),
      };

      let response;

      if (roomQuantity > 1) {
        response = await addRooms(payload, roomQuantity);
      }

      response = await addRoom(payload);

      if (response.ok || response.id) {
        navigate("/rooms"); // Or wherever your list is
      } else {
        throw new Error("Failed to create room");
      }
    } catch (err) {
      console.error("Error creating room:", err);
    } finally {
      setIsSubmitting(false);
    }
  };

  const loadFeatures = async () => {
    try {
      const featuresData = await fetchFeatures();
      setFeatures(featuresData);
    } catch (error) {
      console.error("Failed to load room features:", error);
    }
  };

  useEffect(() => {
    loadFeatures();
  }, []);

  return (
    <Box sx={{ maxWidth: 600, margin: "0 auto", mt: 5 }}>
      <Typography variant="h4" gutterBottom>
        Add New Room
      </Typography>

      <Box sx={{ display: "flex", flexDirection: "column", gap: 2 }}>
        <TextField
          label="Room Title"
          value={newRoom.title}
          onChange={(e) => handleRoomChange("title", e.target.value)}
          fullWidth
        />

        <TextField
          label="Room Summary"
          value={newRoom.summary}
          onChange={(e) => handleRoomChange("summary", e.target.value)}
          fullWidth
        />

        <FormControl fullWidth>
          <InputLabel id="room-type-label">Room Type</InputLabel>
          <Select
            labelId="room-type-label"
            id="room-type-select"
            value={newRoom.roomType}
            label="Room Type"
            onChange={(e) => handleRoomChange("roomType", e.target.value)}
          >
            {roomTypeOptions.map((type) => (
              <MenuItem key={type.value} value={type.value}>
                {type.label}
              </MenuItem>
            ))}
          </Select>
        </FormControl>

        <FormControl fullWidth>
          <InputLabel id="features-label">Room Features</InputLabel>
          <Select
            labelId="features-label"
            id="features-select"
            multiple
            value={newRoom.featureIds}
            onChange={handleFeatureToggle}
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
                    checked={newRoom.featureIds.includes(feature.id)}
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
          type="number"
          value={newRoom.maxOccupancy}
          onChange={(e) => handleRoomChange("maxOccupancy", e.target.value)}
          fullWidth
        />

        <TextField
          label="Price per Night (€)"
          type="number"
          value={newRoom.price}
          onChange={(e) => handleRoomChange("price", e.target.value)}
          fullWidth
        />

        <TextField
          label="Image URL"
          type="url"
          value={newRoom.imageUrl}
          onChange={(e) => handleRoomChange("imageUrl", e.target.value)}
          fullWidth
        />
        <Typography>Want to insert several identical rooms?</Typography>
        <TextField
          label="Room amount"
          type="number"
          value={roomQuantity}
          onChange={(e) => handleQuantityChange(e.target.value)}
          fullWidth
        />

        <Button
          variant="contained"
          color="primary"
          onClick={handleCreateRoom}
          disabled={isSubmitting}
        >
          {isSubmitting ? <CircularProgress size={24} /> : "Add Room"}
        </Button>
      </Box>
    </Box>
  );
};

export default RoomAdditionPage;
