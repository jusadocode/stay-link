import "./App.css";
import { Routes, Route } from "react-router-dom";
import HomePage from "./pages/Home/Home";
import Header from "./components/Header";
import BookingsPage from "./pages/Bookings/Bookings";
import Registration from "./pages/Registration/Registration";
import Login from "./pages/Login/Login";
import BookingsCalendar from "./pages/BookingsCalendar/BookingCalendar";

function App() {
  return (
    <div style={{ flex: 1 }}>
      <Header />
      <Routes>
        <Route path={"/"} element={<HomePage />} />
        <Route path={"/login"} element={<Login />} />
        <Route path={"/register"} element={<Registration />} />
        <Route path={"/bookings"} element={<BookingsPage />} />
        <Route path={"/bookings/calendar"} element={<BookingsCalendar />} />
      </Routes>
    </div>
  );
}

export default App;
