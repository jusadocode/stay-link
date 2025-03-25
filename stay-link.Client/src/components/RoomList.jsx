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
  Box,
  TextField,
} from "@mui/material";
import PersonIcon from "@mui/icons-material/Person";
import BookingDialog from "./BookingDialog";
import RoomTypes from "../data/roomTypes";
import { AuthContext } from "../shared/context/AuthContext";
import { useNavigate } from "react-router-dom";
import { LOGIN_PATH } from "../shared/constants/routes";
import { Collapse } from "@mui/material";
import LoadingIndicator from "./LoadingIndicator";

function RoomList({ rooms }) {
  const [searchInput, setSearchInput] = useState("");
  const [selectedRoom, setSelectedRoom] = useState(null);
  const [expandedRoomId, setExpandedRoomId] = useState(null);
  const [dialogOpen, setDialogOpen] = useState(false);
  const navigate = useNavigate();
  const { isLoggedIn, userIsAdmin } = useContext(AuthContext);

  const filteredRooms = rooms.filter((room) =>
    room.hotel?.address?.toLowerCase().includes(searchInput.toLowerCase())
  );

  const handleBookClick = (room) => {
    isLoggedIn ? handleOpenDialog(room) : navigate(LOGIN_PATH);
  };

  const handleOpenDialog = (room) => {
    setSelectedRoom(room);
    setDialogOpen(true);
  };

  const handleCloseDialog = () => {
    setDialogOpen(false);
    setSelectedRoom(null);
  };

  const handleEditClick = (room) => {
    navigate(`rooms/edit/${room.id}`);
  };

  return (
    <Container style={{ marginTop: "20px" }}>
      <TextField
        label="Search by text"
        value={searchInput}
        onChange={(e) => setSearchInput(e.target.value)}
        variant="standard"
        fullWidth
        style={{ marginBottom: "20px" }}
      />

      {rooms.length === 0 ? (
        <Box display="flex" alignItems="center">
          <LoadingIndicator />
          <Typography style={{ marginLeft: "10px" }}>
            Loading rooms...
          </Typography>
        </Box>
      ) : filteredRooms.length > 0 ? (
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>Room Type</TableCell>
                <TableCell>Guests</TableCell>
                <TableCell>Price</TableCell>
                <TableCell>Description</TableCell>
                <TableCell></TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {filteredRooms.map((room) => (
                <React.Fragment key={room.id}>
                  <TableRow
                    hover
                    onClick={() =>
                      setExpandedRoomId(
                        expandedRoomId === room.id ? null : room.id
                      )
                    }
                    style={{ cursor: "pointer" }}
                  >
                    <TableCell>
                      <img
                        src={room.imageUrl}
                        alt={room.title}
                        style={{
                          width: 100,
                          height: 70,
                          objectFit: "cover",
                          borderRadius: 4,
                        }}
                      />
                    </TableCell>
                    <TableCell>{room.title}</TableCell>
                    <TableCell>{RoomTypes[room.roomType]}</TableCell>
                    <TableCell>
                      {Array.from({ length: room.maxOccupancy }).map((_, i) => (
                        <PersonIcon key={i} fontSize="small" />
                      ))}
                    </TableCell>
                    <TableCell>€{room.price}</TableCell>
                    <TableCell></TableCell>
                  </TableRow>
                  <TableRow>
                    <TableCell
                      colSpan={5}
                      sx={{ paddingBottom: 0, paddingTop: 0 }}
                    >
                      <Collapse
                        in={expandedRoomId === room.id}
                        timeout="auto"
                        unmountOnExit
                      >
                        <Box
                          sx={{
                            margin: 1,
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "space-between",
                          }}
                        >
                          <Box>
                            <Typography variant="subtitle1" gutterBottom>
                              Details for {room.title}
                            </Typography>
                            <Typography variant="body2">
                              <strong>Summary:</strong> {room.summary}
                            </Typography>
                            <Typography variant="body2">
                              <strong>Hotel:</strong> {room.hotel?.name}
                            </Typography>
                            <Typography variant="body2">
                              <strong>Address:</strong> {room.hotel?.address}
                            </Typography>
                          </Box>

                          <Button
                            variant="contained"
                            sx={{ marginRight: 1 }}
                            onClick={(e) => {
                              e.stopPropagation();
                              handleBookClick(room);
                            }}
                          >
                            Book
                          </Button>
                          {userIsAdmin() && (
                            <Button
                              variant="outlined"
                              onClick={(e) => {
                                e.stopPropagation();
                                handleEditClick(room);
                              }}
                            >
                              Edit
                            </Button>
                          )}
                          {/* You can add more here (features, usage, etc) */}
                        </Box>
                      </Collapse>
                    </TableCell>
                  </TableRow>
                </React.Fragment>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      ) : (
        <Typography>No rooms match your search.</Typography>
      )}

      <BookingDialog
        open={dialogOpen}
        selectedRoom={selectedRoom}
        handleCloseDialog={handleCloseDialog}
      />
    </Container>
  );
}

export default RoomList;
