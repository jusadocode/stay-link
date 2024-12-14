import React, { useState } from "react";
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
import { yupResolver } from "@hookform/resolvers/yup";
import { validationSchema } from "./validations";
import { MainBox, ButtonStyle, errorFieldSx } from "./styles";
import { useAuthentication } from "../../shared/hooks/useAuthentication";
import { HOME_PATH } from "../../shared/constants/routes";
import { useErrorHandler } from "../../shared/hooks/useErrorHandler.js";
import { REGISTER_PATH } from "../../shared/constants/routes";

const Login = () => {
  const [loading, setLoading] = useState(false);

  const navigate = useNavigate();

  const { error, handleError, clearError } = useErrorHandler();

  const { login } = useAuthentication();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(validationSchema),
  });

  const handleLogin = async (data) => {
    setLoading(true);
    try {
      const success = await login(data);
      if (success) navigate(HOME_PATH);
    } catch (err) {
      handleError(err);
      setTimeout(() => {
        clearError();
      }, 4000);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Container maxWidth="xs">
      <CssBaseline />
      <Box sx={MainBox}>
        <Typography variant="h4" textAlign="center" fontSize={"40px"}>
          Welcome to Stay-Link
        </Typography>
        <Typography component="p">
          Sign in with username or email address{" "}
        </Typography>
        <Box
          sx={{ mt: 1 }}
          component="form"
          onSubmit={handleSubmit(handleLogin)}
        >
          <TextField
            margin="normal"
            fullWidth
            id="userName"
            label="Username or Email"
            placeholder="e.g., JohnCodes445"
            autoFocus
            {...register("userName")}
            error={Boolean(errors.userName)}
            helperText={errors.userName?.message}
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

          <Box sx={{ display: "flex", alignItems: "center", gap: 1, mt: 2 }}>
            <Typography variant="body2">
              <Link to={REGISTER_PATH} style={{ color: "gray" }}>
                Don&apos;t have an account?
              </Link>
            </Typography>
          </Box>

          {error && <Typography sx={errorFieldSx}>{error}</Typography>}
          <Button
            type="submit"
            disabled={loading}
            fullWidth
            variant="contained"
            sx={ButtonStyle}
          >
            {loading ? <CircularProgress sx={{ mt: 2 }} /> : "Sign In"}
          </Button>
        </Box>
      </Box>
    </Container>
  );
};

export default Login;
