import { Box, Button, Container, Link, Typography } from "@mui/material";
import RoomList from "./components/RoomList";
import { useAuthentication } from "../../shared/hooks/useAuthentication";
import useBookings from "../../shared/hooks/useBookings";
import { useContext, useEffect, useState } from "react";
import { AuthContext } from "../../shared/context/AuthContext";
import { Footer } from "../../components/Footer";
import SearchSection from "./components/SearchSection";
import useRooms from "../../shared/hooks/useRooms";

const HomePage = () => {
  const { isLoggedIn, userIsAdmin } = useContext(AuthContext);
  const { fetchRooms } = useRooms();

  const { logout } = useAuthentication();

  const [rooms, setRooms] = useState([]);

  const getInitialRooms = async () => {
    const initialRooms = await fetchRooms();
    setRooms(initialRooms);
  };

  const handleLogout = async () => {
    await logout();
  };

  useEffect(() => {
    getInitialRooms();
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
      <Typography variant="body1" gutterBottom>
        Discover a seamless way to explore, book, and manage accommodations in
        top hotels with just a few clicks.
      </Typography>

      <Box display="flex" justifyContent="space-between" width="100%" my={2}>
        <Button variant="outlined" component={Link}>
          My Account
        </Button>
        {isLoggedIn ? (
          <Button variant="outlined" component={Link}>
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

      <Container>
        <SearchSection setRooms={setRooms} />
      </Container>

      <Container sx={{ minWidth: "50vw", minHeight: "80vh" }}>
        <RoomList rooms={rooms} />
      </Container>

      <Footer
        copyright="Copyright © jusadocode"
        privacyPolicy="Privacy policy"
      />
    </Container>
  );
};

export default HomePage;
