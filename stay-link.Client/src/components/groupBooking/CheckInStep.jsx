import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";

import { DateRangePicker } from "@mui/x-date-pickers-pro";

import { LocalizationProvider } from "@mui/x-date-pickers";
import { Box, Typography } from "@mui/material";

export default function CheckInStep({ bookingDates, setBookingDates }) {
  return (
    <>
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
    </>
  );
}
