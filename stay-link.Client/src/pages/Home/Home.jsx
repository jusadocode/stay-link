import { Box, Button, Container, Link, Typography } from "@mui/material";
import RoomList from "./components/RoomList";
import { useAuthentication } from "../../shared/hooks/useAuthentication";
import useBookings from "../../shared/hooks/useBookings";
import { useContext, useState } from "react";
import { AuthContext } from "../../shared/context/AuthContext";
import { Footer } from "../../components/Footer";
import SearchSection from "./components/SearchSection";

const HomePage = () => {
  const { isLoggedIn, userIsAdmin } = useContext(AuthContext);
  const { fetchRooms } = useBookings();

  const { logout } = useAuthentication();

  const [rooms, setRooms] = useState([
    {
      id: 1,
      title: "Room 101",
      roomType: 0,
      maxOccupancy: 2,
      price: 80,
      summary: "Cozy standard room with a balcony.",
      imageUrl:
        "https://images.unsplash.com/photo-1583847268964-b28dc8f51f92?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
      hotel: {
        name: "Seaside Inn",
        address: "123 Ocean Drive",
      },
    },
    {
      id: 2,
      title: "Room 202",
      roomType: 1,
      maxOccupancy: 3,
      price: 120,
      summary: "Spacious deluxe room with sea view.",
      imageUrl:
        "https://images.unsplash.com/photo-1583847268964-b28dc8f51f92?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
      hotel: {
        name: "Seaside Inn",
        address: "123 Ocean Drive",
      },
    },
    {
      id: 3,
      title: "Room 303",
      roomType: 2,
      maxOccupancy: 4,
      price: 200,
      summary: "Luxurious suite with private terrace.",
      imageUrl:
        "https://images.unsplash.com/photo-1583847268964-b28dc8f51f92?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
      hotel: {
        name: "Mountain Retreat",
        address: "456 Hilltop Avenue",
      },
    },
  ]);

  const handleLogout = async () => {
    await logout();
  };
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

      <SearchSection />

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
