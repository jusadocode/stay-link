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
import GroupBookingDialog from "../../../components/groupBooking/GroupBookingDialog";

function RoomGroupList({ roomGroups }) {
  const [searchInput, setSearchInput] = useState("");
  const [selectedGroup, setSelectedGroup] = useState(null);
  const [expandedRoomId, setExpandedRoomId] = useState(null);
  const [dialogOpen, setDialogOpen] = useState(false);
  const navigate = useNavigate();
  const { isLoggedIn } = useContext(AuthContext);

  const filteredGroups = roomGroups.filter((group) =>
    group.rooms.some((room) =>
      room.title.toLowerCase().includes(searchInput.toLowerCase())
    )
  );

  const handleBookClick = (group) => {
    isLoggedIn ? handleOpenDialog(group) : navigate(LOGIN_PATH);
  };

  const handleOpenDialog = (group) => {
    setSelectedGroup(group);
    setDialogOpen(true);
  };

  const handleCloseDialog = () => {
    setDialogOpen(false);
    setSelectedGroup(null);
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

      {roomGroups.length === 0 ? (
        <Box display="flex" alignItems="center">
          <Typography style={{ marginLeft: "10px" }}>
            No rooms found.
          </Typography>
        </Box>
      ) : filteredGroups.length > 0 ? (
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell></TableCell>
                <TableCell>Room Group</TableCell>
                <TableCell>Description</TableCell>
                <TableCell>Guests</TableCell>
                <TableCell>Total Price</TableCell>
                <TableCell></TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {filteredGroups.map((group, index) => (
                <React.Fragment key={index}>
                  {/* Group Label Row */}
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
                          Room Group #{index + 1}
                        </Typography>
                        <Typography variant="body1">
                          Price per night
                          <strong>€{group.totalPrice.toFixed(2)}</strong>
                        </Typography>
                      </Box>
                    </TableCell>
                  </TableRow>

                  {/* Summary Row */}
                  <TableRow
                    hover
                    onClick={() =>
                      setExpandedRoomId(expandedRoomId === index ? null : index)
                    }
                    style={{ cursor: "pointer" }}
                  >
                    <TableCell>
                      <img
                        src={group.rooms[0].imageUrl}
                        alt={group.rooms[0].title}
                        style={{
                          width: 100,
                          height: 70,
                          objectFit: "cover",
                          borderRadius: 4,
                        }}
                      />
                    </TableCell>
                    <TableCell>
                      {group.rooms.map((r) => r.title).join(" + ")}
                    </TableCell>
                    <TableCell>
                      {group.rooms.map((r) => r.roomType).join(", ")}
                    </TableCell>
                    <TableCell>
                      {group.rooms.reduce((acc, r) => acc + r.maxOccupancy, 0)}{" "}
                      <PersonIcon fontSize="small" />
                    </TableCell>
                    <TableCell>€{group.totalPrice.toFixed(2)}</TableCell>
                    <TableCell>
                      <Button
                        variant="contained"
                        color="primary"
                        onClick={(e) => {
                          e.stopPropagation();
                          handleBookClick(group);
                        }}
                      >
                        Book Group
                      </Button>
                    </TableCell>
                  </TableRow>

                  {/* Collapsible Detail Row */}
                  <TableRow>
                    <TableCell
                      colSpan={6}
                      sx={{ paddingBottom: 0, paddingTop: 0 }}
                    >
                      <Collapse
                        in={expandedRoomId === index}
                        timeout="auto"
                        unmountOnExit
                      >
                        <Box
                          sx={{
                            margin: 1,
                            display: "flex",
                            flexDirection: "column",
                            gap: 2,
                          }}
                        >
                          {group.rooms.map((room, i) => (
                            <Box
                              key={i}
                              sx={{
                                display: "flex",
                                justifyContent: "space-between",
                                alignItems: "flex-start",
                                borderBottom: "1px solid #ccc",
                                paddingBottom: 1,
                              }}
                            >
                              <Box>
                                <Typography variant="subtitle1">
                                  {room.title}
                                </Typography>
                                <Typography variant="body2">
                                  <strong>Summary:</strong> {room.summary}
                                </Typography>
                                <Typography variant="body2">
                                  <strong>Features:</strong>{" "}
                                  {room.features.map((f, fi) => (
                                    <Typography
                                      key={fi}
                                      component="span"
                                      sx={{ color: "#3ddff3", mr: 1 }}
                                    >
                                      {f.name}
                                    </Typography>
                                  ))}
                                </Typography>
                              </Box>
                            </Box>
                          ))}
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

      <GroupBookingDialog
        open={dialogOpen}
        selectedRooms={selectedGroup?.rooms || []}
        handleCloseDialog={handleCloseDialog}
      />
    </Container>
  );
}

export default RoomGroupList;
