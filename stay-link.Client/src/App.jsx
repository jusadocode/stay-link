import "./App.css";
import { Routes, Route } from "react-router-dom";
import HomePage from "./pages/Home/Home";
import Header from "./components/Header";
import BookingsPage from "./pages/Bookings/Bookings";
import Registration from "./pages/Registration/Registration";
import Login from "./pages/Login/Login";
import BookingsCalendar from "./pages/BookingsCalendar/BookingCalendar";
import React from "react";
import RoomEditPage from "./pages/RoomEditPage/RoomEdit";
import AdminRoomPage from "./pages/AdminRoomPage/AdminRoomPage";
import RoomAdditionPage from "./pages/RoomAdditionPage/RoomAdditionPage";
import BookingEditPage from "./pages/BookingsCalendar/components/BookingEdit/BookingEditPage";
import BookingCreatePage from "./pages/BookingsCalendar/components/BookingAddition/BookingAddition";
import RoomClosureAdditionPage from "./pages/BookingsCalendar/components/RoomClosureAddition/RoomClosureAdditionPage";

function App() {
  return (
    <div className="App">
      <Header />
      <div style={{ flex: 1 }}>
        <Routes>
          <Route path={"/"} element={<HomePage />} />
          <Route path={"/login"} element={<Login />} />
          <Route path={"/register"} element={<Registration />} />
          <Route path={"/bookings"} element={<BookingsPage />} />
          <Route path={"/rooms"} element={<AdminRoomPage />} />
          <Route path={"/bookings/calendar"} element={<BookingsCalendar />} />
          <Route path={"/rooms/edit/:id"} element={<RoomEditPage />} />
          <Route path="/rooms/new" element={<RoomAdditionPage />} />
          <Route path="/bookings/:id/edit" element={<BookingEditPage />} />
          <Route path="/bookings/new" element={<BookingCreatePage />} />
          <Route path="/closures/new" element={<RoomClosureAdditionPage />} />
        </Routes>
      </div>
    </div>
  );
}

export default App;
