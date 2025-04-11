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
import { useNavigate } from "react-router-dom";

function BookingGrid({ rooms, bookings, checkInDate, numDays }) {
  const parseBookingDate = (dateStr) => startOfDay(parseISO(dateStr));

  const [roomUsages, setRoomUsages] = useState([]);

  const navigate = useNavigate();

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
        <Box
          sx={{
            p: 1,
            border: "1px solid #eee",
            borderTop: "none",
            borderLeft: "none",
            backgroundColor: "#f9f9f9",
          }}
        ></Box>

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

              {roomsOfType.map((room) => {
                const usage = roomUsages.find((u) => u.roomId === room.id);
                const wearPercentage = usage ? usage.generalWear * 100 : 0;

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
                      <Stack>
                        {" "}
                        <Typography
                          variant="body2"
                          fontWeight="medium"
                          minWidth={"100%"}
                        >
                          {room.title}
                        </Typography>
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

                    {dateArray.map((date, dateIndex) => {
                      const coveringBooking = bookings.find(
                        (b) =>
                          b.roomIds.includes(room.id) &&
                          isWithinInterval(date, {
                            start: parseBookingDate(b.checkInDate),
                            end: parseBookingDate(b.checkOutDate),
                          })
                      );

                      let renderCellContent = null;

                      if (coveringBooking) {
                        const bookingStartDate = parseBookingDate(
                          coveringBooking.checkInDate
                        );

                        const bookingLastNight: DateOrStringOrNumber =
                          coveringBooking.checkOutDate;

                        const isActualStartDate = isSameDay(
                          date,
                          bookingStartDate
                        );
                        const startedBeforeView =
                          bookingStartDate < dateArray[0];

                        const isFirstVisibleDayOfBooking =
                          startedBeforeView && dateIndex === 0;

                        const shouldRenderBlockInThisCell =
                          isActualStartDate || isFirstVisibleDayOfBooking;

                        if (shouldRenderBlockInThisCell) {
                          const visibleStartDate = startedBeforeView
                            ? date
                            : bookingStartDate; // Start from view start or actual start

                          const visibleEndDate = min([
                            bookingLastNight,
                            dateArray[dateArray.length - 1],
                          ]);

                          // Calculate the span in days based on visible start/end dates
                          const spanDuration =
                            differenceInDays(visibleEndDate, visibleStartDate) +
                            1;

                          const remainingDaysInView = numDays - dateIndex;
                          const bookingSpan = Math.min(
                            spanDuration,
                            remainingDaysInView
                          );

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
                              onClick={() =>
                                navigate(`/bookings/${coveringBooking.id}/edit`)
                              }
                            >
                              {/* Uses your guest name field */}
                              {coveringBooking.displayName}
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
