import React, { useState } from "react";
import {
  SortableContext,
  useSortable,
  verticalListSortingStrategy,
  arrayMove,
  sortableKeyboardCoordinates,
} from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";
import {
  Box,
  Typography,
  Paper,
  List,
  ListItem,
  ListItemText,
  ListItemButton,
  Card,
  Button,
  IconButton,
  TextField,
} from "@mui/material";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { DateRangePicker } from "@mui/x-date-pickers-pro";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import dayjs from "dayjs";
import {
  closestCenter,
  DndContext,
  KeyboardSensor,
  PointerSensor,
  useSensor,
  useSensors,
} from "@dnd-kit/core";
import CloseIcon from "@mui/icons-material/Close";
import SearchIcon from "@mui/icons-material/Search";
import { Bed } from "@mui/icons-material";

const initialPreferences = [
  { id: "1", label: "Balcony" },
  { id: "2", label: "Sea View" },
  { id: "3", label: "Air Conditioning" },
  { id: "4", label: "Minibar" },
  { id: "5", label: "Yakuzi" },
  { id: "6", label: "City View" },
];

function SearchSection() {
  const [preferences] = useState(initialPreferences);
  const [selectedPreferences, setSelectedPreferences] = useState([]);
  const [bookingDates, setBookingDates] = useState([dayjs(), dayjs()]);
  const [guests, setGuests] = useState(2);
  const [rooms, setRooms] = useState(1);

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
              onChange={(newValue) => setBookingDates(newValue)}
            />

            <Box sx={{ display: "flex", gap: 2 }}>
              <TextField
                label="Guests"
                type="number"
                size="small"
                value={guests}
                onChange={(e) => setGuests(Math.max(1, Number(e.target.value)))}
                InputProps={{ inputProps: { min: 1 } }}
              />

              <TextField
                label="Rooms"
                type="number"
                size="small"
                value={rooms}
                onChange={(e) => setRooms(Math.max(1, Number(e.target.value)))}
                InputProps={{ inputProps: { min: 1 } }}
              />
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
              {preferences
                .filter((p) => !selectedPreferences.find((s) => s.id === p.id))
                .map((pref) => (
                  <Button
                    key={pref.id}
                    onClick={() => handlePreferenceToggle(pref)}
                    size="small"
                    variant="outlined"
                  >
                    {pref.label}
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
                      label={pref.label}
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
        <Button size="small">
          <SearchIcon></SearchIcon>
          Find your place
        </Button>
      </Box>
    </Box>
  );
}

function SortableItem({ id, label, onRemove }) {
  const {
    attributes,
    listeners,
    setNodeRef,
    transform,
    transition,
    isDragging,
  } = useSortable({
    id,
  });

  const style = {
    transform: CSS.Transform.toString(transform),
    transition,
    opacity: isDragging ? 0.5 : 1,
    backgroundColor: "#e0f7fa",
    marginBottom: "8px",
    borderRadius: "8px",
    cursor: "pointer",
  };

  return (
    <ListItem
      ref={setNodeRef}
      style={style}
      disablePadding
      sx={{
        "&:hover": {
          backgroundColor: "#FFD95F",
        },
        "&:hover .remove-btn": {
          opacity: 1,
        },
      }}
      secondaryAction={
        <IconButton
          edge="end"
          aria-label="delete"
          className="remove-btn"
          onClick={(e) => {
            e.stopPropagation();
            e.preventDefault();
            onRemove();
          }}
          sx={{ opacity: 0, transition: "opacity 0.2s" }}
        >
          <CloseIcon fontSize="small" />
        </IconButton>
      }
      {...attributes}
      {...listeners}
    >
      <Button fullWidth variant="contained" disableElevation>
        {label}
      </Button>
    </ListItem>
  );
}

export default SearchSection;
