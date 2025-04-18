import React, { useEffect, useState } from "react";
import {
  Container,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Typography,
  TextField,
  CircularProgress,
  TableSortLabel,
  Box,
  Button,
  IconButton,
  Tooltip,
  MenuItem,
  Select,
  FormControl,
  InputLabel,
} from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import useRooms from "../../shared/hooks/useRooms";
import dayjs from "dayjs";
import { DatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { CircularProgress as ProgressCircle } from "@mui/material";
import { useNavigate } from "react-router-dom";

const AdminRoomMetricsPage = () => {
  const { getRoomStats, deleteRoom } = useRooms();
  const [roomStats, setRoomStats] = useState([]);
  const [filteredStats, setFilteredStats] = useState([]);
  const [searchInput, setSearchInput] = useState("");
  const [roomTypeFilter, setRoomTypeFilter] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const [sortConfig, setSortConfig] = useState({
    key: "Revenue",
    direction: "desc",
  });
  const [startDate, setStartDate] = useState(dayjs().startOf("month"));
  const [endDate, setEndDate] = useState(dayjs().endOf("month"));

  const navigate = useNavigate();

  const fetchStats = async (start, end) => {
    setIsLoading(true);
    try {
      const stats = await getRoomStats(
        start.format("YYYY-MM-DD"),
        end.format("YYYY-MM-DD")
      );
      setRoomStats(stats);
      setFilteredStats(stats);
    } catch (error) {
      console.error("Failed to fetch room stats:", error);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    fetchStats(startDate, endDate);
  }, []);

  useEffect(() => {
    const filtered = roomStats.filter((room) => {
      return (
        room.roomTitle.toLowerCase().includes(searchInput.toLowerCase()) &&
        (roomTypeFilter === "" || room.roomType === roomTypeFilter)
      );
    });
    setFilteredStats(filtered);
  }, [searchInput, roomTypeFilter, roomStats]);

  const handleSort = (key) => {
    let direction = "asc";
    if (sortConfig.key === key && sortConfig.direction === "asc") {
      direction = "desc";
    }
    setSortConfig({ key, direction });

    const sorted = [...filteredStats].sort((a, b) => {
      if (direction === "asc") return a[key] > b[key] ? 1 : -1;
      return a[key] < b[key] ? 1 : -1;
    });
    setFilteredStats(sorted);
  };

  const handleApplyFilter = () => {
    fetchStats(startDate, endDate);
  };

  const handleEdit = (roomId) => {
    navigate(`/rooms/edit/${roomId}`);
  };

  const handleDelete = async (roomId) => {
    try {
      await deleteRoom(roomId);
      fetchStats(startDate, endDate); // refresh after deletion
    } catch (error) {
      console.error("Failed to delete room:", error);
    }
  };

  if (isLoading) return <CircularProgress />;

  const columnHeaders = [
    { key: "roomTitle", label: "Room Title" },
    { key: "revenue", label: "Revenue" },
    { key: "reservations", label: "Reservations" },
    { key: "nights", label: "Nights" },
    { key: "occupancy", label: "Occupancy" },
    { key: "adr", label: "ADR" },
    // { key: "leadTime", label: "Lead Time" },
    { key: "loS", label: "Length of Stay" },
    // { key: "revPar", label: "RevPAR" },
    { key: "generalWear", label: "General Wear" },
    { key: "actions", label: "Actions" },
  ];

  const uniqueRoomTypes = [...new Set(roomStats.map((r) => r.roomType))];

  return (
    <Container sx={{ mt: 4 }}>
      <Typography variant="h5" gutterBottom>
        Room Metrics
      </Typography>

      <Box
        display="grid"
        gap={2}
        mb={3}
        gridTemplateColumns={{
          xs: "1fr",
          sm: "repeat(2, 1fr)",
          md: "repeat(3, 1fr)",
        }}
      >
        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <DatePicker
            label="Start Date"
            value={startDate}
            onChange={(newValue) => setStartDate(newValue)}
          />
          <DatePicker
            label="End Date"
            value={endDate}
            onChange={(newValue) => setEndDate(newValue)}
          />
        </LocalizationProvider>

        <TextField
          label="Search Room Title"
          value={searchInput}
          onChange={(e) => setSearchInput(e.target.value)}
        />

        <FormControl>
          <InputLabel>Room Type</InputLabel>
          <Select
            label="Room Type"
            value={roomTypeFilter}
            onChange={(e) => setRoomTypeFilter(e.target.value)}
          >
            <MenuItem value="">All</MenuItem>
            {uniqueRoomTypes.map((type) => (
              <MenuItem key={type} value={type}>
                {type}
              </MenuItem>
            ))}
          </Select>
        </FormControl>

        <Button
          variant="contained"
          onClick={handleApplyFilter}
          sx={{ alignSelf: "end" }}
        >
          Apply Filter
        </Button>

        <Button
          variant="contained"
          onClick={() => navigate("new")}
          sx={{ alignSelf: "end" }}
        >
          + Add room
        </Button>
      </Box>

      <TableContainer>
        <Table>
          <TableHead>
            <TableRow>
              {columnHeaders.map(({ key, label }) => (
                <TableCell key={key} sx={{ whiteSpace: "nowrap" }}>
                  {key !== "actions" ? (
                    <TableSortLabel
                      active={sortConfig.key === key}
                      direction={sortConfig.direction}
                      onClick={() => handleSort(key)}
                    >
                      {label}
                    </TableSortLabel>
                  ) : (
                    label
                  )}
                </TableCell>
              ))}
            </TableRow>
          </TableHead>
          <TableBody>
            {filteredStats.map((room) => (
              <TableRow key={room.roomId}>
                <TableCell>{room.roomTitle}</TableCell>
                <TableCell>€{room.revenue.toFixed(2)}</TableCell>
                <TableCell>{room.reservations}</TableCell>
                <TableCell>{room.nights}</TableCell>
                <TableCell>{(room.occupancy * 100).toFixed(0)}%</TableCell>
                <TableCell>€{room.adr.toFixed(2)}</TableCell>
                {/* <TableCell>{room.leadTime.toFixed(1)}</TableCell> */}
                <TableCell>{room.loS.toFixed(1)}</TableCell>
                {/* <TableCell>€{room.revPar.toFixed(2)}</TableCell> */}
                <TableCell>
                  <Box position="relative" display="inline-flex">
                    <ProgressCircle
                      variant="determinate"
                      value={room.generalWear * 100}
                      size={36}
                      thickness={4}
                      color={
                        room.generalWear > 0.7
                          ? "error"
                          : room.generalWear > 0.4
                          ? "warning"
                          : "success"
                      }
                    />
                    <Box
                      top={0}
                      left={0}
                      bottom={0}
                      right={0}
                      position="absolute"
                      display="flex"
                      alignItems="center"
                      justifyContent="center"
                    >
                      <Typography
                        variant="caption"
                        component="div"
                        color="textSecondary"
                      >
                        {`${Math.round(room.generalWear * 100)}%`}
                      </Typography>
                    </Box>
                  </Box>
                </TableCell>
                <TableCell>
                  <Tooltip title="Edit Room">
                    <IconButton
                      size="small"
                      color="primary"
                      onClick={() => handleEdit(room.roomId)}
                    >
                      <EditIcon />
                    </IconButton>
                  </Tooltip>
                  <Tooltip title="Delete Room">
                    <IconButton
                      size="small"
                      color="error"
                      onClick={() => handleDelete(room.roomId)}
                    >
                      <DeleteIcon />
                    </IconButton>
                  </Tooltip>
                </TableCell>
              </TableRow>
            ))}
            <TableRow key="summary">
              <TableCell>Total:</TableCell>
              <TableCell>
                €
                {filteredStats
                  .reduce((acc, stat) => acc + stat.revenue, 0)
                  .toFixed(2)}
              </TableCell>
              <TableCell>
                {filteredStats.reduce(
                  (acc, stat) => acc + stat.reservations,
                  0
                )}
              </TableCell>
              <TableCell>
                {filteredStats.reduce((acc, stat) => acc + stat.nights, 0)}
              </TableCell>
              <TableCell colSpan={11}></TableCell>
            </TableRow>
          </TableBody>
        </Table>
      </TableContainer>
    </Container>
  );
};

export default AdminRoomMetricsPage;
