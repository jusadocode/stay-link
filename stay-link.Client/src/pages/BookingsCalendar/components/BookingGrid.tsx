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
  const [roomClosures, setRoomClosures] = useState([]);

  const navigate = useNavigate();

  const { fetchRoomsUsages, getRoomClosures } = useRooms();

  async function populateUsageData() {
    try {
      const usageData = await fetchRoomsUsages();
      const closureData = await getRoomClosures();
      setRoomUsages(usageData);
      setRoomClosures(closureData);
    } catch (error) {
      console.error("Error fetching usage or closure data:", error);
    }
  }

  const getBookingsForCell = (roomId, date) => {
    return bookings.find(
      (b) =>
        b.roomIds.includes(roomId) &&
        isWithinInterval(date, {
          start: parseBookingDate(b.checkInDate),
          end: parseBookingDate(b.checkOutDate),
        })
    );
  };

  const getClosureForCell = (roomId, date) => {
    return roomClosures.find(
      (closure) =>
        roomId === closure.roomId &&
        isWithinInterval(date, {
          start: parseBookingDate(closure.startDate),
          end: parseBookingDate(closure.endDate),
        })
    );
  };

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
              position: "sticky",
              top: 0,
              zIndex: 2,
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
                      const coveringBooking = getBookingsForCell(room.id, date);

                      const coveringClosure = getClosureForCell(room.id, date);

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
                      } else if (coveringClosure) {
                        // Only render once per closure block
                        const isActualStartDate = isSameDay(
                          date,
                          parseBookingDate(coveringClosure.startDate)
                        );
                        const startedBeforeView =
                          parseBookingDate(coveringClosure.startDate) <
                          dateArray[0];
                        const isFirstVisibleDayOfClosure =
                          startedBeforeView && dateIndex === 0;
                        const shouldRenderClosureBlock =
                          isActualStartDate || isFirstVisibleDayOfClosure;

                        if (shouldRenderClosureBlock) {
                          const visibleStartDate = startedBeforeView
                            ? date
                            : parseBookingDate(coveringClosure.startDate);
                          const visibleEndDate = min([
                            parseBookingDate(coveringClosure.endDate),
                            dateArray[dateArray.length - 1],
                          ]);

                          const spanDuration =
                            differenceInDays(visibleEndDate, visibleStartDate) +
                            1;
                          const remainingDaysInView = numDays - dateIndex;
                          const closureSpan = Math.min(
                            spanDuration,
                            remainingDaysInView
                          );

                          return (
                            <Box
                              key={`closure-${room.id}-${format(
                                date,
                                "yyyy-MM-dd"
                              )}`}
                              sx={{
                                position: "relative",
                                gridColumn: `${
                                  dateIndex + 2
                                } / span ${closureSpan}`,
                                margin: 0.5,
                                backgroundColor: "grey.400",
                                color: "white",
                                borderRadius: 1,
                                border: "1px dashed grey",
                                fontSize: "0.75rem",
                                minHeight: "40px",
                                display: "flex",
                                alignItems: "center",
                                justifyContent: "center",
                                textAlign: "center",
                              }}
                            >
                              {coveringClosure.reason || "Room Closed"}
                            </Box>
                          );
                        } else {
                          return null;
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
