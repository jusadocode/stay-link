import { useContext, useEffect, useState } from "react";
import { Container, Button, Typography, Box } from "@mui/material";
import HotelList from "./components/HotelList";
import { Footer } from "./components/Footer";
import { Link } from "react-router-dom";
import "./App.css";
import { AuthContext } from "./shared/context/AuthContext";
import { useAuthentication } from "./shared/hooks/useAuthentication";
import useBookings from "./shared/hooks/useBookings";

function App() {
  const [hotels, setHotels] = useState([]);

  const { isLoggedIn, userIsAdmin } = useContext(AuthContext);
  const { fetchHotelRooms, fetchHotels } = useBookings();

  const { logout } = useAuthentication();

  const handleLogout = async () => {
    await logout();
  };

  useEffect(() => {
    populateHotelData();
  }, []);

  return (
    <Container
      maxWidth={false}
      sx={{
        display: "flex",
        flexDirection: "column",
        alignItems: "flex-start",
        py: 4,
      }}
    >
      <Box
        sx={{
          display: "flex",
          flexDirection: "row",
          justifyContent: "space-between",
          alignItems: "center",
          width: "100%",
        }}
      >
        <Typography variant="h4" gutterBottom>
          <strong>StayLink</strong>
        </Typography>

        {isLoggedIn ? (
          <Button
            variant="outlined"
            sx={{ marginInline: "1rem" }}
            onClick={handleLogout}
          >
            Sign out
          </Button>
        ) : (
          <Box sx={{}}>
            <Button
              variant="outlined"
              sx={{ marginInline: "1rem" }}
              component={Link}
              to="/login"
            >
              Sign in
            </Button>
            <Button variant="outlined" component={Link} to="/register">
              Register
            </Button>
          </Box>
        )}
      </Box>
      <Typography variant="body1" gutterBottom>
        Discover a seamless way to explore, book, and manage accommodations in
        top hotels with just a few clicks.
      </Typography>

      <Box display="flex" justifyContent="space-between" width="100%" my={2}>
        <Button variant="outlined" component={Link} to="/">
          My Account
        </Button>
        {isLoggedIn ? (
          <Button variant="outlined" component={Link} to="/bookings">
            {userIsAdmin() ? "All Bookings" : "My Bookings"}
          </Button>
        ) : (
          ""
        )}

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

      <Footer
        copyright="Copyright © jusadocode"
        privacyPolicy="Privacy policy"
      />
    </Container>
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
