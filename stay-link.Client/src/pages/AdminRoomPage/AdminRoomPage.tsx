import React, { useState, useEffect } from "react";
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
  IconButton,
  Tooltip,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from "@mui/material";
import { CircularProgress } from "@mui/material";
import PersonIcon from "@mui/icons-material/Person";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import AddIcon from "@mui/icons-material/Add";
import { useNavigate } from "react-router-dom";
import useRooms from "../../shared/hooks/useRooms";

function AdminRoomPage() {
  const [searchInput, setSearchInput] = useState("");
  const [roomToDelete, setRoomToDelete] = useState(null);
  const { fetchRooms, deleteRoom } = useRooms();

  const navigate = useNavigate();

  const [rooms, setRooms] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);

  const [roomUsages, setRoomUsages] = useState<{ [roomId: number]: RoomUsage }>(
    {}
  );

  const { fetchRoomsUsages } = useRooms();

  async function populateUsageData() {
    try {
      const usageData: RoomUsage[] = await fetchRoomsUsages();
      const usageMap = usageData.reduce((acc, usage) => {
        acc[usage.roomId] = usage;
        return acc;
      }, {});
      setRoomUsages(usageMap);
    } catch (error) {
      console.error("Error fetching usage data:", error);
    }
  }

  const getWearColor = (wear: number) => {
    if (wear < 30) return "success";
    if (wear < 70) return "warning";
    return "error";
  };

  useEffect(() => {
    populateUsageData();
  }, []);

  const filteredRooms = rooms.filter((room) =>
    room.title.toLowerCase().includes(searchInput.toLowerCase())
  );

  const handleEditClick = (room: Room) => {
    navigate(`/rooms/edit/${room.id}`); // Navigate to admin edit route
  };

  const handleOpenDeleteDialog = (room: Room) => {
    setRoomToDelete(room);
  };

  const handleCloseDeleteDialog = () => {
    setRoomToDelete(null);
  };

  const handleConfirmDelete = async () => {
    if (!roomToDelete) return;

    try {
      await deleteRoom(roomToDelete.id); // Using the mock/real function

      handleCloseDeleteDialog();
    } catch (err) {
      console.error("Error deleting room:", err);
      handleCloseDeleteDialog(); // Close dialog even on error for now
    }
  };

  const handleAddNewClick = () => {
    navigate("/rooms/new"); // Navigate to admin add route
  };

  if (error) {
    return <Typography color="error">Error loading rooms: {error}</Typography>;
  }

  useEffect(() => {
    const loadRooms = async () => {
      try {
        setIsLoading(true);
        const roomsData = await fetchRooms();
        setRooms(roomsData);
      } catch (err) {
        console.error("Error loading rooms:", err);
        setError("Failed to load rooms.");
      } finally {
        setIsLoading(false);
      }
    };

    loadRooms();
  }, []);

  return (
    <Container style={{ marginTop: "20px" }}>
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          mb: 2,
          flexWrap: "wrap",
          gap: 2,
        }}
      >
        <TextField
          label="Search Room Title"
          value={searchInput}
          onChange={(e) => setSearchInput(e.target.value)}
          variant="outlined" // Changed variant for better look
          size="small" // Make it smaller
          style={{ flexGrow: 1, minWidth: "200px" }} // Allow shrinking/growing
        />
        <Button
          variant="contained"
          color="primary"
          startIcon={<AddIcon />}
          onClick={handleAddNewClick}
        >
          Add New Room
        </Button>
      </Box>

      {rooms.length === 0 ? (
        <Typography style={{ marginLeft: "10px", marginTop: "20px" }}>
          No rooms found. Use the button above to add one.
        </Typography>
      ) : filteredRooms.length > 0 ? (
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell sx={{ width: "100px" }}>Image</TableCell>{" "}
                <TableCell>Title</TableCell>
                <TableCell>Type</TableCell>
                <TableCell>Guests</TableCell>
                <TableCell>Price (€)</TableCell>
                <TableCell>Wear</TableCell>
                <TableCell align="right">Actions</TableCell>{" "}
              </TableRow>
            </TableHead>
            <TableBody>
              {filteredRooms.map((room) => {
                const usage = roomUsages[room.id];
                const usagePercentage = usage
                  ? Math.round(usage.generalWear * 100)
                  : 0;

                return (
                  <TableRow key={room.id} hover>
                    <TableCell>
                      {room.imageUrl ? (
                        <img
                          src={room.imageUrl}
                          alt={room.title}
                          style={{
                            width: 80, // Smaller image
                            height: 50,
                            objectFit: "cover",
                            borderRadius: 4,
                          }}
                        />
                      ) : (
                        <Box
                          sx={{
                            width: 80,
                            height: 50,
                            backgroundColor: "#eee",
                            borderRadius: 1,
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "center",
                            color: "#999",
                          }}
                        >
                          <Typography variant="caption">No Image</Typography>
                        </Box>
                      )}
                    </TableCell>
                    <TableCell>{room.title}</TableCell>
                    {/* Use RoomTypes mapping, provide fallback */}
                    <TableCell>{room.roomType}</TableCell>
                    <TableCell>
                      <Box display="flex" alignItems="center">
                        <PersonIcon fontSize="small" sx={{ mr: 0.5 }} />{" "}
                        {room.maxOccupancy}
                      </Box>
                    </TableCell>
                    <TableCell>€{room.price.toFixed(2)}</TableCell>{" "}
                    <TableCell>
                      {usage ? (
                        <Tooltip
                          title={`General Wear: ${usagePercentage}%, Status: ${usage.cleaningState}`}
                        >
                          <Box
                            sx={{
                              position: "relative",
                              display: "inline-flex",
                              alignItems: "center",
                              justifyContent: "center",
                            }}
                          >
                            <CircularProgress
                              variant="determinate"
                              value={usagePercentage}
                              size={40}
                              thickness={5}
                              color={getWearColor(usagePercentage)}
                            />
                            <Box
                              sx={{
                                position: "absolute",
                                display: "flex",
                                alignItems: "center",
                                justifyContent: "center",
                              }}
                            >
                              <Typography
                                variant="caption"
                                component="div"
                                color="textSecondary"
                              >
                                {`${usagePercentage}%`}
                              </Typography>
                            </Box>
                          </Box>
                        </Tooltip>
                      ) : (
                        <Typography variant="body2" color="textSecondary">
                          —
                        </Typography>
                      )}
                    </TableCell>
                    <TableCell align="right">
                      {" "}
                      {/* Actions Cell */}
                      <Tooltip title="Edit Room">
                        <IconButton
                          onClick={() => handleEditClick(room)}
                          color="primary"
                          size="small"
                        >
                          <EditIcon />
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Delete Room">
                        <IconButton
                          onClick={() => handleOpenDeleteDialog(room)}
                          color="error"
                          size="small"
                        >
                          <DeleteIcon />
                        </IconButton>
                      </Tooltip>
                    </TableCell>
                  </TableRow>
                );
              })}
            </TableBody>
          </Table>
        </TableContainer>
      ) : (
        <Typography sx={{ mt: 2 }}>No rooms match your search.</Typography>
      )}

      {/* Delete Confirmation Dialog */}
      <Dialog
        open={!!roomToDelete} // Open if roomToDelete is not null
        onClose={handleCloseDeleteDialog}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">Confirm Deletion</DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Are you sure you want to permanently delete the room "
            {roomToDelete?.title}"? This action cannot be undone.
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseDeleteDialog}>Cancel</Button>
          <Button onClick={handleConfirmDelete} color="error" autoFocus>
            Delete
          </Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
}

export default AdminRoomPage;
