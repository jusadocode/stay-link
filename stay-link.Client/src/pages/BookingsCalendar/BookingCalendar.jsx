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
  startOfDay, // Important for comparisons
} from "date-fns";
import useBookings from "../../shared/hooks/useBookings";
import dayjs from "dayjs";

// --- Sample Data (Replace with API data) ---
const sampleRooms = [
  { id: "fr1", title: "Room 1", roomType: "Family Room" },
  { id: "fr2", title: "Room 2", roomType: "Family Room" },
  { id: "fr3", title: "Room 3", roomType: "Family Room" },
  { id: "qr1", title: "Room 1", roomType: "Queen Room" },
  { id: "qr2", title: "Room 2", roomType: "Queen Room" },
  { id: "qr3", title: "Room 3", roomType: "Queen Room" },
];

const sampleBookings = [
  {
    id: "b1",
    roomIds: ["fr1"],
    guestFullName: "Clark, Oliver",
    checkInDate: "2025-04-05",
    checkOutDate: "2025-04-07",
  }, // Dates matching current context
  {
    id: "b2",
    roomIds: ["fr2"],
    guestFullName: "Davis, Isla",
    checkInDate: "2025-04-06",
    checkOutDate: "2025-04-08",
  },
  {
    id: "b3",
    roomIds: ["qr1"],
    guestFullName: "Smith, Charlotte",
    checkInDate: "2025-04-05",
    checkOutDate: "2025-04-05",
  },
  {
    id: "b4",
    roomIds: ["qr2"],
    guestFullName: "Ferraro, Luend",
    checkInDate: "2025-04-05",
    checkOutDate: "2025-04-06",
  },
  {
    id: "b5",
    roomIds: ["fr1"],
    guestFullName: "Smith, Clina",
    checkInDate: "2025-04-09",
    checkOutDate: "2025-04-11",
  },
  {
    id: "b6",
    roomIds: ["qr1"],
    guestFullName: "Jones, Thomas",
    checkInDate: "2025-04-08",
    checkOutDate: "2025-04-10",
  },
  {
    id: "b7",
    roomIds: ["qr2"],
    guestFullName: "Clark, Jack",
    checkInDate: "2025-04-08",
    checkOutDate: "2025-04-09",
  },
  {
    id: "b8",
    roomIds: ["qr3"],
    guestFullName: "Clark, Oliver",
    checkInDate: "2025-04-07",
    checkOutDate: "2025-04-09",
  },
];

// Helper to parse dates consistently
const parseBookingDate = (dateStr) => startOfDay(parseISO(dateStr));

// --- Components ---

function Toolbar({ currentDate, setCurrentDate, numDays, setNumDays }) {
  const handlePrev = () => setCurrentDate((prev) => subDays(prev, 1));
  const handleNext = () => setCurrentDate((prev) => addDays(prev, 1));
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
        <IconButton onClick={handlePrev} size="small">
          <ChevronLeft />
        </IconButton>
        <Typography variant="subtitle1" mx={1}>
          {format(currentDate, "dd MMM yyyy")}
        </Typography>
        <IconButton onClick={handleNext} size="small">
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
  const gridTemplateColumns = `150px repeat(${numDays}, 1fr)`;

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
                    }}
                  >
                    <Typography variant="body2">{room.title}</Typography>
                  </Box>

                  {/* Date Cells for this Room */}
                  {dateArray.map((date, dateIndex) => {
                    const booking = getBookingForCell(room.id, date);
                    const isBooked = isCellBooked(room.id, date); // Is part of an ongoing booking

                    // Calculate booking span if it starts here
                    let bookingSpan = 1;
                    if (booking) {
                      const checkOutDate = parseBookingDate(
                        booking.checkOutDate
                      );
                      const checkInDate = parseBookingDate(booking.checkInDate);
                      const duration =
                        differenceInDays(checkOutDate, checkInDate) + 1;

                      // Clamp span to the visible grid
                      const remainingDaysInView = numDays - dateIndex;
                      bookingSpan = Math.min(duration, remainingDaysInView);
                    }

                    // Render booking block or empty cell
                    // Important: Only render the *start* of the booking visually.
                    // Subsequent cells covered by the booking should render nothing to allow the first cell to span over them.
                    if (booking) {
                      return (
                        <Box
                          key={`${room.id}-${format(date, "yyyy-MM-dd")}`}
                          gridColumn={`span ${bookingSpan}`} // Span columns
                          sx={{
                            position: "relative", // Needed for potential absolute positioning inside if required
                            p: 0.5,
                            m: "2px", // Add margin for spacing
                            backgroundColor: "primary.light", // Example color
                            color: "primary.contrastText",
                            borderRadius: 1,
                            overflow: "hidden",
                            whiteSpace: "nowrap",
                            textOverflow: "ellipsis",
                            fontSize: "0.75rem",
                            border: "1px solid",
                            borderColor: "primary.dark",
                            zIndex: 1, // Ensure booking is above grid lines
                            cursor: "pointer",
                            minHeight: "40px", // Ensure minimum height
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "center",
                          }}
                        >
                          {booking.guestFullName}
                        </Box>
                      );
                    } else if (!isBooked) {
                      // Only render an empty cell if it's NOT covered by an ongoing booking
                      return (
                        <Box
                          key={`${room.id}-${format(date, "yyyy-MM-dd")}`}
                          sx={{
                            border: "1px solid #eee",
                            borderTop: "none",
                            borderLeft: "none",
                            minHeight: "40px", // Match booking height
                          }}
                        ></Box>
                      );
                    } else {
                      // This cell is covered by a booking starting earlier, render nothing
                      // Or render a placeholder invisible div if grid layout requires it
                      return null; // Or <Box key={...} sx={{ visibility: 'hidden' }}></Box>
                    }
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
  const [rooms, setRooms] = useState(sampleRooms); // Replace with API fetch
  const [bookings, setBookings] = useState(sampleBookings); // Replace with API fetch

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
