import {
  Paper,
  Box,
  Typography,
  LinearProgress,
  Stack,
  Tooltip,
} from "@mui/material";
import {
  addDays,
  isSameDay,
  format,
  min,
  differenceInDays,
  startOfDay,
} from "date-fns";
import { eachDayOfInterval } from "date-fns/eachDayOfInterval";
import { isWithinInterval } from "date-fns/isWithinInterval";
import { parseISO } from "date-fns/parseISO";
import React, { useEffect, useMemo, useState } from "react";
import useRooms from "../../../shared/hooks/useRooms";
import {
  CheckCircleOutline,
  CleaningServicesOutlined,
} from "@mui/icons-material";

function BookingGrid({ rooms, bookings, checkInDate, numDays }) {
  const parseBookingDate = (dateStr) => startOfDay(parseISO(dateStr));

  const [roomUsages, setRoomUsages] = useState([]);

  const { fetchRoomsUsages } = useRooms();

  async function populateUsageData() {
    try {
      const usageData = await fetchRoomsUsages();
      setRoomUsages(usageData);
    } catch (error) {
      console.error("Error fetching usage data:", error);
    }
  }

  useEffect(() => {
    populateUsageData();
  }, []);

  const dateArray: DateOrStringOrNumber[] = useMemo(
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
              {roomsOfType.map((room) => {
                const usage = roomUsages.find((u) => u.roomId === room.id);
                const wearPercentage = usage ? usage.generalWear * 100 : 0;

                // Determine progress bar color based on wear (Example thresholds)
                let progressBarColor: "success" | "warning" | "error" =
                  "success";

                if (wearPercentage > 75) {
                  progressBarColor = "error";
                } else if (wearPercentage > 40) {
                  progressBarColor = "warning";
                }

                const isClean = usage?.cleaningState === "Clean";
                return (
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
                      <Stack spacing={0.5}>
                        {" "}
                        {/* Use Stack for vertical layout */}
                        <Typography variant="body2" fontWeight="medium">
                          {room.title}
                        </Typography>
                        {/* Display Usage Info if available */}
                        {usage && (
                          <>
                            <Tooltip
                              title={`General Wear: ${wearPercentage.toFixed(
                                0
                              )}%`}
                            >
                              <Box sx={{ width: "100%", mt: 0.5 }}>
                                <LinearProgress
                                  variant="determinate"
                                  value={wearPercentage}
                                  color={progressBarColor} // Apply conditional color
                                />
                              </Box>
                            </Tooltip>
                            <Box
                              display="flex"
                              justifyContent="space-between"
                              alignItems="center"
                              mt={0.5}
                            >
                              <Tooltip title={usage.cleaningState}>
                                {isClean ? (
                                  <CheckCircleOutline
                                    fontSize="small"
                                    color="success"
                                  />
                                ) : (
                                  <CleaningServicesOutlined
                                    fontSize="small"
                                    color="warning"
                                  />
                                )}
                              </Tooltip>
                              <Tooltip
                                title={`Booked ${usage.timesBookedSinceMaintenance} times since maintenance`}
                              >
                                <Typography
                                  variant="caption"
                                  color="text.secondary"
                                >
                                  Bk: {usage.timesBookedSinceMaintenance}
                                </Typography>
                              </Tooltip>
                            </Box>
                            {/* Optionally display timesBookedThisYear */}
                            <Tooltip
                              title={`Booked ${usage.timesBookedThisYear} times this year`}
                            >
                              <Typography
                                variant="caption"
                                color="text.secondary"
                                alignSelf="flex-end"
                              >
                                Yr: {usage.timesBookedThisYear}
                              </Typography>
                            </Tooltip>
                          </>
                        )}
                      </Stack>
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

                        const bookingLastNight: DateOrStringOrNumber =
                          coveringBooking.checkOutDate;

                        // Determine if this 'date' cell is where the *visible* part of the booking should START rendering
                        const isActualStartDate = isSameDay(
                          date,
                          bookingStartDate
                        );
                        const startedBeforeView =
                          bookingStartDate < dateArray[0];
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
                            key={`${room.id}-${format(
                              date,
                              "yyyy-MM-dd"
                            )}-empty`}
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
                );
              })}
            </React.Fragment>
          )
        )}
      </Box>
    </Paper>
  );
}

export default BookingGrid;
