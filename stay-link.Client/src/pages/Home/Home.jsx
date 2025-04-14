import { Box, Button, Container, Link, Typography } from "@mui/material";
import RoomList from "./components/RoomList";
import { useAuthentication } from "../../shared/hooks/useAuthentication";
import { useContext, useEffect, useState } from "react";
import { AuthContext } from "../../shared/context/AuthContext";
import { Footer } from "../../components/Footer";
import SearchSection from "./components/SearchSection";
import useRooms from "../../shared/hooks/useRooms";
import LoadingIndicator from "../../shared/components/LoadingIndicator";
import { useNavigate } from "react-router-dom";
import React from "react";
import dayjs from "dayjs";

const HomePage = () => {
  const { isLoggedIn, userIsAdmin } = useContext(AuthContext);
  const { searchRooms } = useRooms();
  const navigate = useNavigate();
  const { logout } = useAuthentication();

  const [roomOffers, setRoomOffers] = useState([]);
  const [isLoading, setIsLoading] = useState(false);

  const [bookingDates, setBookingDates] = useState([
    dayjs().add(1, "day"),
    dayjs().add(3, "day"),
  ]);

  const defaultRoomSearch = {
    checkIn: dayjs().startOf("week").add(1, "day"), // Monday
    checkOut: dayjs().endOf("week"), // Sunday
    guestCount: 0, // 'Auto' search
    preferenceIds: [],
  };

  const getInitialRooms = async () => {
    try {
      setIsLoading(true);
      const initialRooms = await searchRooms(defaultRoomSearch);
      setRoomOffers(initialRooms);
    } catch (err) {
      console.error("Failed to fetch initial room offers", err);
    } finally {
      setIsLoading(false);
    }
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

      <Box display="flex" gap={"2rem"} my={2}>
        {isLoggedIn && (
          <Button
            variant="outlined"
            onClick={() =>
              userIsAdmin()
                ? navigate("/bookings/calendar")
                : navigate("/bookings")
            }
          >
            {userIsAdmin() ? "All Bookings" : "My Bookings"}
          </Button>
        )}
        {userIsAdmin() ? (
          <Button
            variant="outlined"
            component="a"
            onClick={() => navigate("/rooms")}
            target="_blank"
            rel="noopener noreferrer"
          >
            Rooms
          </Button>
        ) : (
          ""
        )}
      </Box>

      <Typography variant="h3" gutterBottom>
        Search for a place to stay
      </Typography>
      <Typography variant="body1" gutterBottom>
        We provide a variety of options for all kinds of customer needs.
      </Typography>

      <Container>
        <SearchSection
          setRoomOffers={setRoomOffers}
          setIsLoading={setIsLoading}
          setBookingDates={setBookingDates}
          bookingDates={bookingDates}
        />
      </Container>

      {!isLoading ? (
        <Container sx={{ minWidth: "50vw", minHeight: "80vh" }}>
          <RoomList roomOffers={roomOffers} bookingDates={bookingDates} />
        </Container>
      ) : (
        <Box display="flex" alignItems="center" mt={4}>
          <LoadingIndicator />
          <Typography sx={{ ml: 2 }}>Getting rooms...</Typography>
        </Box>
      )}

      <Footer
        copyright="Copyright © jusadocode"
        privacyPolicy="Privacy policy"
      />
    </Container>
  );
};

export default HomePage;
