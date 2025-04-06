import React, { useState, useMemo, useEffect } from "react";
import {
  Box,
  Paper,
  Typography,
  IconButton,
  Select,
  MenuItem,
  Button,
  Container,
} from "@mui/material";
import { ChevronLeft, ChevronRight, Today } from "@mui/icons-material";
import {
  format,
  addDays,
  subDays,
  eachDayOfInterval,
  differenceInDays,
  isWithinInterval,
  parseISO, // If your dates are strings
  isSameDay,
  startOfDay,
  min,
  subWeeks,
  addWeeks, // Important for comparisons
} from "date-fns";
import useBookings from "../../shared/hooks/useBookings";
import dayjs from "dayjs";

// --- Sample Data (Replace with API data) ---
// const sampleRooms = [
//   { id: "fr1", title: "Room 1", roomType: "Family Room" },
//   { id: "fr2", title: "Room 2", roomType: "Family Room" },
//   { id: "fr3", title: "Room 3", roomType: "Family Room" },
//   { id: "qr1", title: "Room 1", roomType: "Queen Room" },
//   { id: "qr2", title: "Room 2", roomType: "Queen Room" },
//   { id: "qr3", title: "Room 3", roomType: "Queen Room" },
// ];

// const sampleBookings = [
//   {
//     id: "b1",
//     roomIds: ["fr1"],
//     guestFullName: "Clark, Oliver",
//     checkInDate: "2025-04-05",
//     checkOutDate: "2025-04-07",
//   }, // Dates matching current context
//   {
//     id: "b2",
//     roomIds: ["fr2"],
//     guestFullName: "Davis, Isla",
//     checkInDate: "2025-04-06",
//     checkOutDate: "2025-04-08",
//   },
//   {
//     id: "b3",
//     roomIds: ["qr1"],
//     guestFullName: "Smith, Charlotte",
//     checkInDate: "2025-04-05",
//     checkOutDate: "2025-04-05",
//   },
//   {
//     id: "b4",
//     roomIds: ["qr2"],
//     guestFullName: "Ferraro, Luend",
//     checkInDate: "2025-04-05",
//     checkOutDate: "2025-04-06",
//   },
//   {
//     id: "b5",
//     roomIds: ["fr1"],
//     guestFullName: "Smith, Clina",
//     checkInDate: "2025-04-09",
//     checkOutDate: "2025-04-11",
//   },
//   {
//     id: "b6",
//     roomIds: ["qr1"],
//     guestFullName: "Jones, Thomas",
//     checkInDate: "2025-04-08",
//     checkOutDate: "2025-04-10",
//   },
//   {
//     id: "b7",
//     roomIds: ["qr2"],
//     guestFullName: "Clark, Jack",
//     checkInDate: "2025-04-08",
//     checkOutDate: "2025-04-09",
//   },
//   {
//     id: "b8",
//     roomIds: ["qr3"],
//     guestFullName: "Clark, Oliver",
//     checkInDate: "2025-04-07",
//     checkOutDate: "2025-04-09",
//   },
// ];

// Helper to parse dates consistently
const parseBookingDate = (dateStr) => startOfDay(parseISO(dateStr));

// --- Components ---

function Toolbar({ currentDate, setCurrentDate, numDays, setNumDays }) {
  const handlePrevWeek = () => setCurrentDate((prev) => subWeeks(prev, 1));
  const handlePrevDay = () => setCurrentDate((prev) => subDays(prev, 1));
  const handleNextDay = () => setCurrentDate((prev) => addDays(prev, 1));
  const handleNextWeek = () => setCurrentDate((prev) => addWeeks(prev, 1));
  const handleToday = () => setCurrentDate(new Date());
  // Add more complex navigation (prev/next week/month) if needed

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
        <Button variant="contained" color="primary" size="small">
          + Reservation
        </Button>

        <Button variant="contained" color="primary" size="small">
          + Room Closure
        </Button>
      </Box>
    </Box>
  );
}

function BookingGrid({ rooms, bookings, checkInDate, numDays }) {
  console.log("Rooms received in BookingGrid:", rooms);
  const dateArray = useMemo(
    () =>
      eachDayOfInterval({
        start: checkInDate,
        end: addDays(checkInDate, numDays - 1),
      }),
    [checkInDate, numDays]
  );

  const groupedRooms = useMemo(() => {
    return rooms.reduce((acc, room) => {
      if (!acc[room.roomType]) {
        acc[room.roomType] = [];
      }
      acc[room.roomType].push(room);
      return acc;
    }, {});
  }, [rooms]);

  // Calculate grid column template: 1 for room names + 1 for each day
  const gridTemplateColumns = `150px repeat(${numDays}, 80px)`;

  // Find booking for a specific room and date cell
  const getBookingForCell = (roomId, date) => {
    // Find bookings that *start* on this specific date for this room
    return bookings.find(
      (b) =>
        b.roomIds.includes(roomId) &&
        isSameDay(parseBookingDate(b.checkInDate), date)
    );
  };

  // Check if a date cell is part of an *ongoing* booking (but not the start)
  const isCellBooked = (roomId, date) => {
    return bookings.some(
      (b) =>
        b.roomIds.includes(roomId) &&
        !isSameDay(parseBookingDate(b.checkInDate), date) && // Exclude the start date itself
        isWithinInterval(date, {
          start: parseBookingDate(b.checkInDate),
          end: parseBookingDate(b.checkOutDate),
        })
    );
  };

  return (
    <Paper elevation={1} sx={{ overflowX: "auto" }}>
      <Box
        display="grid"
        gridTemplateColumns={gridTemplateColumns}
        sx={{ minWidth: `${150 + numDays * 80}px` }} // Ensure minimum width
      >
        {/* Header Row: Corner */}
        <Box
          sx={{
            p: 1,
            border: "1px solid #eee",
            borderTop: "none",
            borderLeft: "none",
            backgroundColor: "#f9f9f9",
          }}
        ></Box>

        {/* Header Row: Dates */}
        {dateArray.map((date, index) => (
          <Box
            key={index}
            sx={{
              textAlign: "center",
              p: 1,
              border: "1px solid #eee",
              borderTop: "none",
              borderLeft: index === 0 ? "none" : undefined, // Remove left border for first date cell
              backgroundColor: "#f9f9f9",
              fontWeight: "bold",
            }}
          >
            <Typography variant="caption" display="block">
              {format(date, "EEE")}
            </Typography>
            <Typography variant="body2">{format(date, "dd")}</Typography>
            <Typography variant="caption" display="block">
              {format(date, "MMM")}
            </Typography>
          </Box>
        ))}

        {/* Room Rows */}
        {Object.entries(groupedRooms).map(
          ([roomType, roomsOfType], typeIndex) => (
            <React.Fragment key={roomType}>
              {/* Room Type Header Row */}
              <Box
                gridColumn={`1 / span ${numDays + 1}`}
                sx={{
                  p: 1,
                  backgroundColor: "#fafafa",
                  fontWeight: "bold",
                  borderBottom: "1px solid #ddd",
                  borderTop: typeIndex > 0 ? "1px solid #ddd" : "none",
                  textAlign: "left",
                }}
              >
                <Typography variant="subtitle2">{roomType}</Typography>
              </Box>

              {/* Individual Room Rows */}
              {roomsOfType.map((room) => (
                <React.Fragment key={room.id}>
                  {/* Room Name Cell */}
                  <Box
                    sx={{
                      p: 1,
                      border: "1px solid #eee",
                      borderLeft: "none",
                      display: "flex",
                      alignItems: "center",
                      gridColumn: 1,
                    }}
                  >
                    <Typography variant="body2">{room.title}</Typography>
                  </Box>

                  {/* Date Cells for this Room */}
                  {dateArray.map((date, dateIndex) => {
                    const coveringBooking = bookings.find(
                      (b) =>
                        b.roomIds.includes(room.id) &&
                        isWithinInterval(date, {
                          start: parseBookingDate(b.checkInDate),
                          // *** IMPORTANT: isWithinInterval is usually inclusive.
                          // If your checkOutDate means "the day *of* checkout", you might need to subtract a day
                          // from b.checkOutDate for accurate interval checking, depending on how you store/define it.
                          // Assuming checkOutDate is the day *after* the last night stayed for now:
                          end: parseBookingDate(b.checkOutDate), // This usually means up to the START of the checkout day.
                          // If checkOutDate *is* the last day of stay, use end: addDays(parseBookingDate(b.checkOutDate), 1)
                          // Or adjust the differenceInDays calculation later. Let's assume checkout is the day AFTER last night for now.
                        })
                    );

                    let renderCellContent = null; // What to render in this cell

                    if (coveringBooking) {
                      const bookingStartDate = parseBookingDate(
                        coveringBooking.checkInDate
                      );
                      // Assuming checkOutDate is the day AFTER the last night stayed.
                      // The last night is checkOutDate - 1 day.
                      // const bookingLastNight = subDays(
                      //   parseBookingDate(coveringBooking.checkOutDate),
                      //   1
                      // );

                      const bookingLastNight = coveringBooking.checkOutDate;

                      // Determine if this 'date' cell is where the *visible* part of the booking should START rendering
                      const isActualStartDate = isSameDay(
                        date,
                        bookingStartDate
                      );
                      const startedBeforeView = bookingStartDate < dateArray[0];
                      // Check if it's the first day *of the view* AND the booking started before
                      const isFirstVisibleDayOfBooking =
                        startedBeforeView && dateIndex === 0;

                      // Render the block if it's the actual start date OR if it's the first visible day of a booking that started earlier
                      const shouldRenderBlockInThisCell =
                        isActualStartDate || isFirstVisibleDayOfBooking;

                      if (shouldRenderBlockInThisCell) {
                        // Calculate the STARTING date for the visible block span
                        const visibleStartDate = startedBeforeView
                          ? date
                          : bookingStartDate; // Start from view start or actual start

                        // Calculate the ENDING date for the visible block span (last night of stay, clamped by view end)
                        const visibleEndDate = min([
                          bookingLastNight,
                          dateArray[dateArray.length - 1],
                        ]);

                        // Calculate the span in days based on visible start/end dates
                        const spanDuration =
                          differenceInDays(visibleEndDate, visibleStartDate) +
                          1;

                        // Calculate the grid column span based on duration
                        // It cannot exceed remaining days in view starting from current cell
                        const remainingDaysInView = numDays - dateIndex;
                        const bookingSpan = Math.min(
                          spanDuration,
                          remainingDaysInView
                        );

                        // Style adjustments for partial bookings (optional)
                        const blockStyle = {
                          position: "relative",
                          gridColumn: `${dateIndex + 2} / span ${
                            bookingSpan > 0 ? bookingSpan : 1
                          }`,
                          margin: 0.5,
                          backgroundColor: "primary.light",
                          color: "primary.contrastText",
                          borderRadius: 1,
                          overflow: "hidden",
                          whiteSpace: "nowrap",
                          textOverflow: "ellipsis",
                          fontSize: "0.75rem",
                          border: "1px solid",
                          borderColor: "primary.dark",
                          zIndex: 1,
                          cursor: "pointer",
                          minHeight: "40px",
                          display: "flex",
                          alignItems: "center",
                          justifyContent: "center",
                          // Add visual cue if booking started before the view
                          borderTopLeftRadius: startedBeforeView
                            ? 0
                            : undefined,
                          borderBottomLeftRadius: startedBeforeView
                            ? 0
                            : undefined,
                        };

                        renderCellContent = (
                          <Box
                            key={`${coveringBooking.id}-${format(
                              date,
                              "yyyy-MM-dd"
                            )}`}
                            sx={blockStyle}
                          >
                            {/* Uses your guest name field */}
                            {coveringBooking.guestFullName}
                          </Box>
                        );
                      } else {
                        // This cell is covered by a booking, but the visual block
                        // starts in an earlier cell within the view. Render nothing (null).
                        renderCellContent = null;
                      }
                    } else {
                      // This cell is completely empty
                      renderCellContent = (
                        <Box
                          key={`${room.id}-${format(date, "yyyy-MM-dd")}-empty`}
                          sx={{
                            border: "1px solid #eee",
                            borderTop: "none",
                            borderLeft: "none",
                            minHeight: "40px",
                            p: 0.5,
                          }}
                        ></Box>
                      );
                    }

                    // Make sure this return is the LAST thing in the dateArray.map callback
                    return renderCellContent;
                  })}
                </React.Fragment>
              ))}
            </React.Fragment>
          )
        )}
      </Box>
    </Paper>
  );
}

// --- Main Dashboard Component ---
function BookingsCalendar() {
  const [currentDate, setCurrentDate] = useState(startOfDay(new Date())); // Use startOfDay for consistency
  const [numDays, setNumDays] = useState(14); // Default view: 14 days
  const [rooms, setRooms] = useState([]); // Replace with API fetch
  const [bookings, setBookings] = useState([]); // Replace with API fetch

  const { fetchBookings, fetchRoom } = useBookings();

  async function populateBookingData() {
    try {
      const bookingsData = await fetchBookings();

      const roomIdSet = new Set();
      bookingsData.forEach((booking) =>
        booking.roomIds.forEach((roomId) => roomIdSet.add(roomId))
      );

      const roomFetchPromises = Array.from(roomIdSet).map((id) =>
        fetchRoom(id)
      );
      const roomsData = await Promise.all(roomFetchPromises);

      setBookings(bookingsData);
      setRooms(roomsData);
    } catch (error) {
      console.error("Error fetching booking data:", error);
    }
  }

  useEffect(() => {
    populateBookingData();
  }, []);

  return (
    <Container maxWidth="xl" sx={{ mt: 4 }}>
      <Typography variant="h4" gutterBottom>
        Booking Calendar
      </Typography>
      <Toolbar
        currentDate={currentDate}
        setCurrentDate={setCurrentDate}
        numDays={numDays}
        setNumDays={setNumDays}
      />
      <BookingGrid
        rooms={rooms}
        bookings={bookings}
        checkInDate={currentDate}
        numDays={numDays}
      />
      {/* Add other dashboard elements here */}
    </Container>
  );
}

export default BookingsCalendar;
