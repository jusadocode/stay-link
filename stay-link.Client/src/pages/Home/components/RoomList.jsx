import React, { useContext, useState } from "react";
import {
  Container,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  Button,
  TableHead,
  TableRow,
  Paper,
  Typography,
  Box,
  TextField,
  Collapse,
} from "@mui/material";
import PersonIcon from "@mui/icons-material/Person";
import { AuthContext } from "../../../shared/context/AuthContext";
import { useNavigate } from "react-router-dom";
import { LOGIN_PATH } from "../../../shared/constants/routes";
import GroupBookingDialog from "../../../components/roomBooking/BookingDialog";

function RoomList({ roomOffers, bookingDates }) {
  const [searchInput, setSearchInput] = useState("");
  const [selectedOffer, setSelectedoffer] = useState(null);
  const [expandedRoomId, setExpandedRoomId] = useState(null);
  const [dialogOpen, setDialogOpen] = useState(false);
  const navigate = useNavigate();
  const { isLoggedIn } = useContext(AuthContext);

  const filteredOffers = roomOffers.filter((offer) =>
    offer.rooms.some((room) =>
      room.title.toLowerCase().includes(searchInput.toLowerCase())
    )
  );

  const handleBookClick = (offer) => {
    isLoggedIn ? handleOpenDialog(offer) : navigate(LOGIN_PATH);
  };

  const handleOpenDialog = (offer) => {
    setSelectedoffer(offer);
    setDialogOpen(true);
  };

  const handleCloseDialog = () => {
    setDialogOpen(false);
    setSelectedoffer(null);
  };

  const featureIcons = {
    WiFi: "📶",
    TV: "📺",
    "Air Conditioning": "❄️",
    Balcony: "🌅",
    Minibar: "🍸",
    "Breakfast Included": "🍳",
    "Pool Access": "🏊‍♂️",
    Parking: "🚗",
    "Pet Friendly": "🐶",
  };

  return (
    <Container sx={{ mt: 4 }}>
      <TextField
        label="Search by room title"
        value={searchInput}
        onChange={(e) => setSearchInput(e.target.value)}
        variant="standard"
        fullWidth
        sx={{ mb: 3 }}
      />

      {roomOffers.length === 0 ? (
        <Typography>No rooms found.</Typography>
      ) : filteredOffers.length === 0 ? (
        <Typography>No rooms match your search.</Typography>
      ) : (
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell></TableCell>
                <TableCell>Title</TableCell>
                <TableCell>Room Type</TableCell>
                <TableCell>Guests</TableCell>
                <TableCell>Price per night</TableCell>
                <TableCell></TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {filteredOffers.map((offer, index) => {
                const isGroup = offer.rooms.length > 1;
                const totalGuests = offer.rooms.reduce(
                  (acc, r) => acc + r.maxOccupancy,
                  0
                );

                return (
                  <React.Fragment key={offer.rooms.map((r) => r.id).join("-")}>
                    {/* offer Label */}
                    {isGroup && (
                      <TableRow>
                        <TableCell colSpan={6}>
                          <Box
                            sx={{
                              backgroundColor: "#f0f4f8",
                              border: "1px solid #ddd",
                              padding: 1.5,
                              borderRadius: 2,
                              mb: 1,
                              display: "flex",
                              justifyContent: "space-between",
                            }}
                          >
                            <Typography variant="h6">
                              {offer.rooms.length} Room offer
                            </Typography>
                            <Typography variant="body1">
                              Price per night:{" "}
                              <strong>€{offer.totalPrice.toFixed(2)}</strong>
                            </Typography>
                          </Box>
                        </TableCell>
                      </TableRow>
                    )}

                    {/* Summary row */}
                    <TableRow
                      hover
                      onClick={() =>
                        setExpandedRoomId(
                          expandedRoomId === index ? null : index
                        )
                      }
                      sx={{ cursor: "pointer" }}
                    >
                      <TableCell>
                        <img
                          src={offer.rooms[0]?.imageUrl}
                          alt={offer.rooms[0]?.title}
                          style={{
                            width: 100,
                            height: 70,
                            objectFit: "cover",
                            borderRadius: 4,
                          }}
                        />
                      </TableCell>
                      <TableCell>
                        {offer.rooms.map((r) => r.title).join(" + ")}
                      </TableCell>
                      <TableCell>
                        {offer.rooms.map((r) => r.roomType).join(", ")}
                      </TableCell>
                      <TableCell>
                        {totalGuests} <PersonIcon fontSize="small" />
                      </TableCell>
                      <TableCell>€{offer.totalPrice.toFixed(2)}</TableCell>
                      <TableCell>
                        <Button
                          variant="contained"
                          color={isGroup ? "secondary" : "primary"}
                          onClick={(e) => {
                            e.stopPropagation();
                            handleBookClick(offer);
                          }}
                        >
                          {isGroup ? "Book Rooms" : "Book Room"}
                        </Button>
                      </TableCell>
                    </TableRow>

                    {/* Expandable details */}
                    <TableRow>
                      <TableCell colSpan={6} sx={{ p: 0 }}>
                        <Collapse
                          in={expandedRoomId === index}
                          timeout="auto"
                          unmountOnExit
                        >
                          <Box
                            sx={{
                              m: 2,
                              display: "flex",
                              flexDirection: "column",
                              gap: 3,
                            }}
                          >
                            {offer.description && (
                              <Typography
                                variant="body2"
                                color="textSecondary"
                                sx={{ mb: 1 }}
                              >
                                <strong>Offer Summary:</strong>{" "}
                                {offer.description}
                              </Typography>
                            )}

                            {offer.rooms.map((room) => (
                              <Box
                                key={room.id}
                                sx={{
                                  display: "flex",
                                  justifyContent: "space-between",
                                  alignItems: "flex-start",
                                  borderBottom: "1px solid #eee",
                                  pb: 2,
                                }}
                              >
                                <Box sx={{ maxWidth: "70%" }}>
                                  <Typography variant="h6">
                                    {room.title}
                                  </Typography>

                                  <Typography variant="body2" sx={{ mt: 0.5 }}>
                                    <strong>Room Type:</strong> {room.roomType}
                                  </Typography>

                                  <Typography variant="body2">
                                    <strong>Max Occupancy:</strong>{" "}
                                    {room.maxOccupancy}
                                  </Typography>

                                  <Typography variant="body2">
                                    <strong>Price:</strong> €
                                    {room.price.toFixed(2)}
                                  </Typography>

                                  <Typography variant="body2" sx={{ mt: 1 }}>
                                    <strong>Summary:</strong> {room.summary}
                                  </Typography>

                                  <Typography variant="body2" sx={{ mt: 1 }}>
                                    <strong>Features:</strong>{" "}
                                    {room.features.length > 0 ? (
                                      room.features.map((f, i) => (
                                        <Box
                                          key={f.id || i}
                                          component="span"
                                          sx={{
                                            display: "inline-flex",
                                            alignItems: "center",
                                            mr: 1.5,
                                            mb: 0.5,
                                          }}
                                        >
                                          <span
                                            style={{
                                              fontSize: "1.2em",
                                              marginRight: "4px",
                                            }}
                                          >
                                            {featureIcons[f.name] || "🔧"}
                                          </span>
                                          <Typography
                                            component="span"
                                            variant="body2"
                                            color="text.primary"
                                          >
                                            {f.name}
                                          </Typography>
                                        </Box>
                                      ))
                                    ) : (
                                      <Typography
                                        component="span"
                                        color="text.secondary"
                                      >
                                        None
                                      </Typography>
                                    )}
                                  </Typography>
                                </Box>
                              </Box>
                            ))}
                          </Box>
                        </Collapse>
                      </TableCell>
                    </TableRow>
                  </React.Fragment>
                );
              })}
            </TableBody>
          </Table>
        </TableContainer>
      )}

      <GroupBookingDialog
        open={dialogOpen}
        selectedRooms={selectedOffer?.rooms || []}
        handleCloseDialog={handleCloseDialog}
        bookingDates={bookingDates}
      />
    </Container>
  );
}

export default RoomList;
