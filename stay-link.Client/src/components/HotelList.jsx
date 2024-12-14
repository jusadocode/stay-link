/* eslint-disable react/prop-types */
import React, { useContext, useState } from "react";
import {
  Container,
  Table,
  CircularProgress,
  TableBody,
  TableCell,
  TableContainer,
  Button,
  TableHead,
  TableRow,
  Paper,
  Typography,
  Collapse,
  IconButton,
  Box,
  TextField,
} from "@mui/material";
import PersonIcon from "@mui/icons-material/Person";
import { ExpandMore, ExpandLess } from "@mui/icons-material";
import BookingDialog from "./BookingDialog";
import RoomTypes from "../data/roomTypes";
import "../App.css";
import { AuthContext } from "../shared/context/AuthContext";
import { useNavigate } from "react-router-dom";
import { LOGIN_PATH, EDIT_ROOM_PATH } from "../shared/constants/routes";

function HotelList({ hotels }) {
  const [searchInput, setSearchInput] = useState("");
  const [expandedHotel, setExpandedHotel] = useState(null);
  const [selectedRoom, setSelectedRoom] = useState(null);
  const [dialogOpen, setDialogOpen] = useState(false);
  const navigate = useNavigate();

  const { isLoggedIn, userIsAdmin } = useContext(AuthContext);

  const handleExpandClick = (hotelId) => {
    setExpandedHotel(expandedHotel === hotelId ? null : hotelId);
  };

  function filterHotels(hotels, query) {
    query = query.toLowerCase();
    return hotels.filter((hotel) =>
      hotel.address.toLowerCase().includes(query)
    );
  }

  const handleEditClick = (room) => {
    navigate(`rooms/edit/${room.id}`);
  };

  const handleBookClick = (room) => {
    isLoggedIn ? handleOpenDialog(room) : navigate(LOGIN_PATH);
  };

  function handleOpenDialog(room) {
    setSelectedRoom(room);
    setDialogOpen(true);
  }

  const handleCloseDialog = () => {
    setDialogOpen(false);
    setSelectedRoom(null);
  };

  const filteredHotels = filterHotels(hotels, searchInput);

  const hotelList = (
    <TableContainer component={Paper}>
      <Table aria-label="hotel table">
        <TableHead>
          <TableRow>
            <TableCell>Hotels</TableCell>
            <TableCell>Name</TableCell>
            <TableCell>Location</TableCell>
            <TableCell>More</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {filteredHotels.map((hotel) => (
            <React.Fragment key={hotel.id}>
              <TableRow
                className="hotelTableRow"
                onClick={() => handleExpandClick(hotel.id)}
                style={{ cursor: "pointer" }}
              >
                <TableCell>
                  <img
                    src={hotel.imageUrl}
                    alt={hotel.name}
                    style={{
                      width: "80%",
                      height: "80%",
                      objectFit: "cover",
                    }}
                  />
                </TableCell>
                <TableCell>
                  <Typography variant="body1" noWrap>
                    {hotel.name}
                  </Typography>
                </TableCell>
                <TableCell>
                  <Typography variant="body1" noWrap>
                    {hotel.address}
                  </Typography>
                </TableCell>
                <TableCell>
                  <IconButton aria-label="expand row" size="small">
                    {expandedHotel === hotel.id ? (
                      <ExpandLess />
                    ) : (
                      <ExpandMore />
                    )}
                  </IconButton>
                </TableCell>
              </TableRow>
              <TableRow>
                <TableCell
                  colSpan={4}
                  style={{ paddingBottom: 0, paddingTop: 0 }}
                >
                  <Collapse
                    in={expandedHotel === hotel.id}
                    timeout="auto"
                    unmountOnExit
                  >
                    <Box margin={1}>
                      <Typography variant="h6" gutterBottom component="div">
                        Available rooms in {hotel.name}
                      </Typography>
                      <Table size="small" aria-label="rooms">
                        <TableHead>
                          <TableRow>
                            <TableCell>Room Type</TableCell>
                            <TableCell>Number of Guests</TableCell>
                            <TableCell>Price</TableCell>
                            <TableCell>Description</TableCell>
                            <TableCell></TableCell>
                          </TableRow>
                        </TableHead>
                        <TableBody>
                          {hotel.rooms.map((room) => (
                            <TableRow className="roomTableRow" key={room.id}>
                              <TableCell>{RoomTypes[room.roomType]}</TableCell>
                              <TableCell>
                                {Array.from(
                                  { length: room.maxOccupancy },
                                  (_, index) => (
                                    <PersonIcon key={index} />
                                  )
                                )}
                              </TableCell>
                              <TableCell>€{room.price}</TableCell>
                              <TableCell>{room.summary}</TableCell>
                              <TableCell sx={{}}>
                                <Button
                                  variant="contained"
                                  onClick={() => handleBookClick(room)}
                                >
                                  Book
                                </Button>
                                {userIsAdmin() ? (
                                  <Button
                                    variant="contained"
                                    onClick={() => handleEditClick(room)}
                                  >
                                    Edit
                                  </Button>
                                ) : (
                                  ""
                                )}
                              </TableCell>
                            </TableRow>
                          ))}
                        </TableBody>
                      </Table>
                    </Box>
                  </Collapse>
                </TableCell>
              </TableRow>
            </React.Fragment>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );

  return (
    <>
      <Container style={{ marginTop: "20px" }}>
        <TextField
          id="input-with-icon-textfield"
          label="Where are you planning to go?"
          value={searchInput}
          onChange={(event) => setSearchInput(event.target.value)}
          variant="standard"
          fullWidth
          style={{ marginBottom: "20px" }}
        />
        {hotels.length === 0 ? (
          <Box display="flex" alignItems="center">
            <CircularProgress />
            <Typography style={{ marginLeft: "10px" }}>
              Getting hotel data...
            </Typography>
          </Box>
        ) : filteredHotels.length > 0 ? (
          hotelList
        ) : (
          "No hotels found by the location"
        )}
      </Container>
      <BookingDialog
        open={dialogOpen}
        selectedRoom={selectedRoom}
        handleCloseDialog={handleCloseDialog}
        expandedHotel={filteredHotels.find(
          (hotel) => hotel.id === expandedHotel
        )}
      />
    </>
  );
}

export default HotelList;
