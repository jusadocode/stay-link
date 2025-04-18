import { Today, ChevronLeft, ChevronRight } from "@mui/icons-material";
import {
  Box,
  Button,
  IconButton,
  MenuItem,
  Select,
  Typography,
} from "@mui/material";
import { addDays, addWeeks, format, subDays, subWeeks } from "date-fns";
import React, { useState } from "react";
import BookingAdditionDialog from "./BookingAddition/BookingAddition";
import RoomClosureDialog from "./RoomClosureAddition/RoomClosureAdditionPage";

function Toolbar({
  currentDate,
  setCurrentDate,
  numDays,
  setNumDays,
  populateBookingData,
  populateUsageData,
  setShowBookings,
  setShowHousekeeping,
  showBookings,
  showHouseKeeping,
}) {
  const handlePrevWeek = () => setCurrentDate((prev) => subWeeks(prev, 1));
  const handlePrevDay = () => setCurrentDate((prev) => subDays(prev, 1));
  const handleNextDay = () => setCurrentDate((prev) => addDays(prev, 1));
  const handleNextWeek = () => setCurrentDate((prev) => addWeeks(prev, 1));
  const handleToday = () => setCurrentDate(new Date());

  const [openBookingDialog, setOpenBookingDialog] = useState(false);
  const [openClosureDialog, setOpenClosureDialog] = useState(false);

  return (
    <Box
      display="flex"
      justifyContent="space-between"
      alignItems="center"
      mb={2}
      p={1}
      flexWrap="wrap"
    >
      <Box>
        <Select
          value={numDays}
          onChange={(e) => setNumDays(Number(e.target.value))}
          size="small"
          sx={{ mr: 2 }}
        >
          <MenuItem value={7}>7 days</MenuItem>
          <MenuItem value={14}>14 days</MenuItem>
          <MenuItem value={30}>30 days</MenuItem>
        </Select>
        <Button
          variant="outlined"
          startIcon={<Today />}
          onClick={handleToday}
          size="small"
        >
          View Today
        </Button>

        <Box sx={{ display: "flex", gap: 1 }}>
          <Button
            variant={showBookings ? "contained" : "outlined"}
            color="primary"
            size="small"
            onClick={() => setShowBookings((prev) => !prev)}
          >
            Bookings
          </Button>
          <Button
            variant={showHouseKeeping ? "contained" : "outlined"}
            color="secondary"
            size="small"
            onClick={() => setShowHousekeeping((prev) => !prev)}
          >
            Housekeeping
          </Button>
        </Box>
      </Box>
      <Box display="flex" alignItems="center">
        {/* Add << and >> buttons if needed */}
        <IconButton onClick={handlePrevWeek} size="small">
          <ChevronLeft />
        </IconButton>
        <IconButton onClick={handlePrevDay} size="small">
          <ChevronLeft />
        </IconButton>
        <Typography variant="subtitle1" mx={1}>
          {format(currentDate, "dd MMM yyyy")}
        </Typography>
        <IconButton onClick={handleNextDay} size="small">
          <ChevronRight />
        </IconButton>
        <IconButton onClick={handleNextWeek} size="small">
          <ChevronRight />
        </IconButton>
      </Box>

      <Box sx={{ display: "flex", gap: "1rem" }}>
        <Button
          variant="contained"
          color="primary"
          size="small"
          onClick={() => setOpenBookingDialog(true)}
        >
          + Add booking
        </Button>
        <Button
          variant="contained"
          color="primary"
          size="small"
          onClick={() => setOpenClosureDialog(true)}
        >
          + Room Closure
        </Button>
      </Box>

      <BookingAdditionDialog
        open={openBookingDialog}
        onClose={() => setOpenBookingDialog(false)}
        onSuccess={() => {
          populateBookingData();
          populateUsageData();
          setOpenBookingDialog(false);
        }}
      />

      <RoomClosureDialog
        open={openClosureDialog}
        onClose={() => setOpenClosureDialog(false)}
        onSuccess={() => {
          populateBookingData();
          populateUsageData();
          setOpenClosureDialog(false);
        }}
      />
    </Box>
  );
}

export default Toolbar;
