import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";

import { DateRangePicker } from "@mui/x-date-pickers-pro";

import { LocalizationProvider } from "@mui/x-date-pickers";
import { Box, Typography } from "@mui/material";
import dayjs, { Dayjs } from "dayjs";

export default function CheckInStep({ bookingDates, setBookingDates }) {
  const lastSunday = dayjs().startOf("week").subtract(1, "day");
  const nextSunday = dayjs().endOf("week").startOf("day");

  const tomorrow = dayjs().add(1, "day");

  const isValidBookingDate = (date) => {
    if (date < dayjs(Date.now())) return false;
    const day = date.day();

    return day === 0 || day === 6;
  };

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
            minDate={tomorrow}
            onChange={(newValue) => setBookingDates(newValue)}
          />
        </LocalizationProvider>
      </Box>
    </>
  );
}
