import React, { useRef, useState } from "react";
import {
  Container,
  CssBaseline,
  Box,
  Typography,
  TextField,
  Button,
  CircularProgress,
} from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { RegistrationSchema } from "./validations";
import {
  MainBox,
  ButtonStyle,
  FormBottomSection,
  SuggestionBox,
  RegistrationForm,
} from "./styles";
import { useAuthentication } from "../../shared/hooks/useAuthentication";
import { useFile } from "../../shared/hooks/useFile";
import { LOGIN_PATH } from "../../shared/constants/routes";
import { yupResolver } from "@hookform/resolvers/yup";

const Registration = () => {
  const navigate = useNavigate();

  const { registerUser } = useAuthentication();
  const { uploadProfileImage } = useFile();

  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  const [isLoading, setIsLoading] = useState(false);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(RegistrationSchema),
    mode: "onChange",
  });

  const handleRegister = async (data) => {
    setIsLoading(true);
    setError("");
    try {
      if (image !== null) {
        const response = await uploadProfileImage(image);
        data.profileImageUrl = response.imageLocation;
      }

      const success = await registerUser(data);

      if (success) {
        setSuccess("Success! Moving on to the login page...");
        setTimeout(() => {
          navigate("/login");
        }, 2000);
      }
    } catch (e) {
      setError(e.message);
      setIsLoading(false);
    }
  };

  return (
    <Container maxWidth="xs">
      <CssBaseline />
      <Box sx={MainBox}>
        <Box>
          <Typography variant="h4" textAlign="center" fontSize={"40px"}>
            Welcome to StayLink
          </Typography>
          <Typography component="p" sx={{ textAlign: "center" }}>
            Create an account
          </Typography>
        </Box>

        <Box
          component="form"
          onSubmit={handleSubmit(handleRegister)}
          sx={RegistrationForm}
        >
          <TextField
            margin="normal"
            fullWidth
            id="firstName"
            label="First Name"
            placeholder="e.g., Mike"
            autoFocus
            {...register("firstname")}
            error={Boolean(errors.firstname)}
            helperText={errors.firstname?.message}
          />

          <TextField
            margin="normal"
            fullWidth
            id="lastName"
            label="Last Name"
            placeholder="e.g., Johnson"
            autoFocus
            {...register("lastname")}
            error={Boolean(errors.lastname)}
            helperText={errors.lastname?.message}
          />

          <TextField
            margin="normal"
            fullWidth
            id="email"
            label="Email Address"
            placeholder="e.g., name@gmail.com"
            autoFocus
            {...register("email")}
            error={Boolean(errors.email)}
            helperText={errors.email?.message}
          />

          <TextField
            margin="normal"
            fullWidth
            id="password"
            label="Password"
            type="password"
            {...register("password")}
            error={Boolean(errors.password)}
            helperText={errors.password?.message}
          />

          <TextField
            margin="normal"
            fullWidth
            id="repeatedPassword"
            label="Confirm Password"
            type="password"
            {...register("repeatedPassword")}
            error={Boolean(errors.repeatedPassword)}
            helperText={errors.repeatedPassword?.message}
          />

          <Box sx={SuggestionBox}>
            <Typography variant="body2">Already have an account?</Typography>
            <Link to={LOGIN_PATH} style={{ color: "gray" }}>
              Sign in
            </Link>
          </Box>

          <Box sx={FormBottomSection}>
            <Typography
              variant="body2"
              sx={{
                color: error ? "red" : success ? "green" : "inherit",
              }}
            >
              {error ? error : success ? success : ""}
            </Typography>

            {isLoading ? (
              <CircularProgress sx={{ mt: 2 }} />
            ) : (
              <Button
                type="submit"
                fullWidth
                variant="contained"
                sx={ButtonStyle}
              >
                Register
              </Button>
            )}
          </Box>
        </Box>
      </Box>
    </Container>
  );
};

export default Registration;
