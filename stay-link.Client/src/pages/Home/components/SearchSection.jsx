import React, { useState } from "react";
import {
  DndContext,
  closestCenter,
  KeyboardSensor,
  PointerSensor,
  useSensor,
  useSensors,
} from "@dnd-kit/core";
import {
  arrayMove,
  SortableContext,
  sortableKeyboardCoordinates,
  useSortable,
  verticalListSortingStrategy,
} from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";
import {
  Box,
  Typography,
  Paper,
  TextField,
  Grid,
  List,
  ListItem,
  ListItemText,
  ListItemButton,
} from "@mui/material";
import { format } from "date-fns";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { DateRangePicker } from "@mui/x-date-pickers-pro";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import dayjs from "dayjs";

const initialPreferences = [
  { id: "1", label: "Balcony" },
  { id: "2", label: "Sea View" },
  { id: "3", label: "Air Conditioning" },
  { id: "4", label: "Minibar" },
];

function SearchSection() {
  const [preferences, setPreferences] = useState(initialPreferences);
  const [checkIn, setCheckIn] = useState("");
  const [checkOut, setCheckOut] = useState("");
  const [guests, setGuests] = useState(1);

  const [bookingDates, setBookingDates] = useState([dayjs(), dayjs()]);

  const sensors = useSensors(
    useSensor(PointerSensor),
    useSensor(KeyboardSensor, {
      coordinateGetter: sortableKeyboardCoordinates,
    })
  );

  const handleDragEnd = (event) => {
    const { active, over } = event;

    if (active.id !== over?.id) {
      const oldIndex = preferences.findIndex((item) => item.id === active.id);
      const newIndex = preferences.findIndex((item) => item.id === over?.id);

      setPreferences((items) => arrayMove(items, oldIndex, newIndex));
    }
  };

  return (
    <Box p={3} component={Paper}>
      <Typography variant="h6" gutterBottom>
        Search Preferences
      </Typography>

      <Box sx={{ mt: 2, mb: 1 }}>
        <Typography sx={{ my: 2 }}>
          When should we prepare your room? Select your dates:
        </Typography>

        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <DateRangePicker
            localeText={{ start: "Check-in", end: "Check-out" }}
            value={bookingDates}
            onChange={(newValue) => setBookingDates(newValue)}
          />
        </LocalizationProvider>
      </Box>

      <Typography variant="subtitle1" gutterBottom>
        Drag to prioritize features:
      </Typography>

      <DndContext
        sensors={sensors}
        collisionDetection={closestCenter}
        onDragEnd={handleDragEnd}
      >
        <SortableContext
          items={preferences}
          strategy={verticalListSortingStrategy}
        >
          <List component="div">
            {preferences.map((pref) => (
              <SortableItem key={pref.id} id={pref.id} label={pref.label} />
            ))}
          </List>
        </SortableContext>
      </DndContext>
    </Box>
  );
}

function SortableItem({ id, label }) {
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
    backgroundColor: "#f5f5f5",
    marginBottom: "8px",
  };

  return (
    <ListItem
      ref={setNodeRef}
      style={style}
      {...attributes}
      {...listeners}
      disablePadding
    >
      <ListItemButton>
        <ListItemText primary={label} />
      </ListItemButton>
    </ListItem>
  );
}

export default SearchSection;
