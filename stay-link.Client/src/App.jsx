import { useContext, useEffect, useState } from "react";
import { Container, Button, Typography, Box } from "@mui/material";
import { Footer } from "./components/Footer";
import { Link } from "react-router-dom";
import "./App.css";
import { AuthContext } from "./shared/context/AuthContext";
import { Routes, Route } from "react-router-dom";
import { useAuthentication } from "./shared/hooks/useAuthentication";
import useBookings from "./shared/hooks/useBookings";
import HomePage from "./pages/Home/Home";
import Header from "./components/Header";
import BookingsPage from "./pages/Bookings/Bookings";
import { Login } from "@mui/icons-material";
import Registration from "./pages/Registration/Registration";

function App() {
  return (
    <div style={{ flex: 1 }}>
      <Header />
      <Routes>
        <Route path={"/"} element={<HomePage />} />
        <Route path={"/login"} element={<Login />} />
        <Route path={"/register"} element={<Registration />} />
        <Route path={"/bookings"} element={<BookingsPage />} />
      </Routes>
    </div>
  );
}

export default App;
