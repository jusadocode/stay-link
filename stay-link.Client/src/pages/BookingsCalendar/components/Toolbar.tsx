import {
  Today,
  ChevronLeft,
  ChevronRight,
  CleaningServices,
} from "@mui/icons-material";
import {
  Box,
  Button,
  IconButton,
  MenuItem,
  Select,
  Typography,
} from "@mui/material";
import { addDays, addWeeks, format, subDays, subWeeks } from "date-fns";
import React from "react";
import { useNavigate } from "react-router-dom";

function Toolbar({ currentDate, setCurrentDate, numDays, setNumDays }) {
  const handlePrevWeek = () => setCurrentDate((prev) => subWeeks(prev, 1));
  const handlePrevDay = () => setCurrentDate((prev) => subDays(prev, 1));
  const handleNextDay = () => setCurrentDate((prev) => addDays(prev, 1));
  const handleNextWeek = () => setCurrentDate((prev) => addWeeks(prev, 1));
  const handleToday = () => setCurrentDate(new Date());

  const navigator = useNavigate();

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

        <Button
          variant="outlined"
          startIcon={<CleaningServices />}
          onClick={handleToday}
          size="small"
        >
          View Housekeeping
        </Button>
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
          onClick={() => navigator("/bookings/new")}
        >
          + Add booking
        </Button>
        <Button
          variant="contained"
          color="primary"
          size="small"
          onClick={() => navigator("/closures/new")}
        >
          + Room Closure
        </Button>
      </Box>
    </Box>
  );
}

export default Toolbar;
