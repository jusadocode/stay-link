import { Box, Button, Link, Typography } from "@mui/material";
import { useAuthentication } from "../shared/hooks/useAuthentication";
import useBookings from "../shared/hooks/useBookings";
import { useContext } from "react";
import { AuthContext } from "../shared/context/AuthContext";
import { useNavigate } from "react-router-dom";
import { LOGIN_PATH, REGISTER_PATH } from "../shared/constants/routes";

const Header = () => {
  const { isLoggedIn, userIsAdmin } = useContext(AuthContext);
  const { fetchRooms } = useBookings();

  const navigate = useNavigate();

  const { logout } = useAuthentication();

  const handleLogout = async () => {
    await logout();
  };
  return (
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
            onClick={() => navigate(LOGIN_PATH)}
          >
            Sign in
          </Button>
          <Button variant="outlined" onClick={() => navigate(REGISTER_PATH)}>
            Register
          </Button>
        </Box>
      )}
    </Box>
  );
};

export default Header;
