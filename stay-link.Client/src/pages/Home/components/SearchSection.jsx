import React, { useEffect, useState } from "react";
import {
  SortableContext,
  verticalListSortingStrategy,
  arrayMove,
  sortableKeyboardCoordinates,
} from "@dnd-kit/sortable";
import { Box, Typography, Paper, List, Button, TextField } from "@mui/material";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { DateRangePicker } from "@mui/x-date-pickers-pro";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import {
  closestCenter,
  DndContext,
  KeyboardSensor,
  PointerSensor,
  useSensor,
  useSensors,
} from "@dnd-kit/core";
import SearchIcon from "@mui/icons-material/Search";
import useRooms from "../../../shared/hooks/useRooms";
import dayjs from "dayjs";
import SortableItem from "./SortableItem";

function SearchSection({
  setRoomOffers,
  setIsLoading,
  bookingDates,
  setBookingDates,
}) {
  const [selectedPreferences, setSelectedPreferences] = useState([]);
  const [features, setFeatures] = useState([]);
  // const [showAllRooms, setShowAllRooms] = useState(false);

  const [guestCount, setGuestCount] = useState(1);
  // const [roomCount, setRoomCount] = useState(1);

  const { searchRooms, fetchFeatures } = useRooms();

  const tomorrow = dayjs().add(1, "day");

  const sensors = useSensors(
    useSensor(PointerSensor, {
      activationConstraint: {
        distance: 1,
      },
    }),
    useSensor(KeyboardSensor, {
      coordinateGetter: sortableKeyboardCoordinates,
    })
  );

  const handlePreferenceToggle = (pref) => {
    const isSelected = selectedPreferences.find((p) => p.id === pref.id);
    if (isSelected) {
      setSelectedPreferences((prev) => prev.filter((p) => p.id !== pref.id));
    } else {
      setSelectedPreferences((prev) => [...prev, pref]);
    }
  };

  const handleDragEnd = (event) => {
    const { active, over } = event;
    if (!over || active.id === over.id) return;

    const oldIndex = selectedPreferences.findIndex((p) => p.id === active.id);
    const newIndex = selectedPreferences.findIndex((p) => p.id === over.id);

    if (oldIndex !== -1 && newIndex !== -1) {
      setSelectedPreferences((items) => arrayMove(items, oldIndex, newIndex));
    }
  };

  const handleSearchClick = async () => {
    setIsLoading(true);
    try {
      const payload = {
        checkIn: bookingDates[0],
        checkOut: bookingDates[1],
        preferenceIds: selectedPreferences.map((p) => parseInt(p.id)),
        guestCount: Number(guestCount),
      };

      const offers = await searchRooms(payload);
      setRoomOffers(offers);
    } catch (err) {
      console.error("Search error:", err);
    } finally {
      setIsLoading(false);
    }
  };

  const getInitialFeatures = async () => {
    const initialFeatures = await fetchFeatures();
    setFeatures(initialFeatures);
  };

  useEffect(() => {
    getInitialFeatures();
  }, []);

  return (
    <Box p={3} component={Paper}>
      <Box sx={{ display: "flex", gap: "2rem" }}>
        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <Box
            sx={{
              display: "flex",
              flexDirection: "column",
              gap: 2,
              justifyContent: "space-evenly",
            }}
          >
            <DateRangePicker
              localeText={{ start: "Check-in", end: "Check-out" }}
              value={bookingDates}
              minDate={tomorrow}
              onChange={(newValue) => setBookingDates(newValue)}
            />

            <Box sx={{ display: "flex", gap: 2 }}>
              <TextField
                label="Guest amount"
                type="number"
                size="small"
                value={guestCount}
                onChange={(e) =>
                  setGuestCount(Math.max(1, Number(e.target.value)))
                }
                InputProps={{ inputProps: { min: 1 } }}
              />

              {/* <TextField
                label="Rooms"
                type="number"
                size="small"
                value={roomCount}
                onChange={(e) =>
                  setRoomCount(Math.max(1, Number(e.target.value)))
                }
                InputProps={{ inputProps: { min: 1 } }}
              /> */}
            </Box>
          </Box>
        </LocalizationProvider>

        <Box
          sx={{ display: "flex", justifyContent: "space-between", gap: "2rem" }}
        >
          <Box>
            <Typography variant="subtitle1">Available Preferences</Typography>
            <List
              sx={{
                display: "grid",
                alignItems: "center",
                gap: "0.2rem",
              }}
            >
              {features
                .filter((p) => !selectedPreferences.find((s) => s.id === p.id))
                .map((pref) => (
                  <Button
                    key={pref.id}
                    onClick={() => handlePreferenceToggle(pref)}
                    size="small"
                    variant="outlined"
                  >
                    {pref.name}
                  </Button>
                ))}
            </List>
          </Box>
          <Box>
            <Typography variant="subtitle1">Selected Preferences</Typography>
            <DndContext
              sensors={sensors}
              onDragEnd={handleDragEnd}
              collisionDetection={closestCenter}
            >
              <SortableContext
                items={selectedPreferences.map((p) => p.id)}
                strategy={verticalListSortingStrategy}
              >
                <List>
                  {selectedPreferences.map((pref) => (
                    <SortableItem
                      key={pref.id}
                      id={pref.id}
                      label={pref.name}
                      onRemove={() => handlePreferenceToggle(pref)}
                    />
                  ))}
                </List>
              </SortableContext>
            </DndContext>
          </Box>
        </Box>
      </Box>

      <Box display={"flex"}>
        <Button size="small" onClick={handleSearchClick}>
          <SearchIcon></SearchIcon>
          Find your place
        </Button>
      </Box>
    </Box>
  );
}

export default SearchSection;
