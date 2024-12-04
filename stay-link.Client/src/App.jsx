import { useEffect, useState } from "react";
import { Container, Button, Typography, Box } from "@mui/material";
import HotelList from "./components/HotelList";
import { fetchHotelRooms, fetchHotels } from "./services/hotelService";
import { BrowserRouter as Router, Link } from "react-router-dom";
import "./App.css";
import { AuthContext, AuthProvider } from "./shared/context/AuthContext";

function App() {
  const [hotels, setHotels] = useState([]);

  useEffect(() => {
    populateHotelData();
  }, []);

  return (
    <AuthProvider>
      <Container
        maxWidth="md"
        sx={{
          display: "flex",
          flexDirection: "column",
          alignItems: "flex-start",
          py: 4,
        }}
      >
        <Typography variant="h4" gutterBottom>
          <strong>StayLink</strong>
        </Typography>

        <Typography variant="body1" gutterBottom>
          Discover a seamless way to explore, book, and manage accommodations in
          top hotels with just a few clicks.
        </Typography>

        <Box display="flex" justifyContent="space-between" width="100%" my={2}>
          <Button variant="outlined" component={Link} to="/">
            My Account
          </Button>
          <Button variant="outlined" component={Link} to="/bookings">
            My Bookings
          </Button>
          <Button
            variant="outlined"
            component="a"
            href="https://github.com/jusadocode"
            target="_blank"
            rel="noopener noreferrer"
          >
            Github
          </Button>
          <Button
            variant="outlined"
            component="a"
            href="https://google.com"
            target="_blank"
            rel="noopener noreferrer"
          >
            Check out more
          </Button>
        </Box>

        <Typography variant="h3" gutterBottom>
          Search for a place to stay
        </Typography>
        <Typography variant="body1" gutterBottom>
          We provide a variety of options for all kinds of customer needs.
        </Typography>

        <Container sx={{ minWidth: "50vw", minHeight: "80vh" }}>
          <HotelList hotels={hotels} />
        </Container>
      </Container>
    </AuthProvider>
  );

  async function populateHotelData() {
    const data = await fetchHotels();

    const updatedData = await Promise.all(
      data.map(async (hotel) => {
        const hotelRooms = await fetchHotelRooms(hotel.id);
        return { ...hotel, rooms: hotelRooms };
      })
    );
    setHotels(updatedData);
  }
}

export default App;
