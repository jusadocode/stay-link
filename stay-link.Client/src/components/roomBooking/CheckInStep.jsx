import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { DateRangePicker } from "@mui/x-date-pickers-pro";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { Box, TextField, Typography } from "@mui/material";
import dayjs from "dayjs";
import React from "react";

export default function CheckInStep({
  bookingDates,
  setBookingDates,
  groupName,
  setGroupName,
  isGroupBooking,
}) {
  const tomorrow = dayjs().add(1, "day");
  return (
    <>
      <Box
        sx={{
          mt: 2,
          mb: 1,
          display: "flex",
          flexDirection: "column",
          alignItems: "flex-start",
          gap: "2rem",
        }}
      >
        <Typography sx={{ my: 2 }}>
          When should we prepare your room? Select your dates:
        </Typography>

        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <DateRangePicker
            localeText={{ start: "Check-in", end: "Check-out" }}
            value={bookingDates}
            minDate={tomorrow}
            onChange={(newValue) => setBookingDates(newValue)}
          />
        </LocalizationProvider>

        {isGroupBooking && (
          <TextField
            label="Group name"
            value={groupName}
            onChange={(e) => setGroupName(e.target.value)}
            variant="outlined" // Changed variant for better look
            size="small" // Make it smaller
            style={{ flexGrow: 1, minWidth: "200px" }} // Allow shrinking/growing
          />
        )}
      </Box>
    </>
  );
}
