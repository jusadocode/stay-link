import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import App from "./App.jsx";
import BookingList from "./components/BookingList.jsx";
import ErrorPage from "./components/ErrorPage.jsx";
import "./index.css";
import Login from "./pages/Login/Login.jsx";
import Registration from "./pages/Registration/Registration.jsx";
import { AuthProvider } from "./shared/context/AuthContext.jsx";
import RoomEditPage from "./pages/RoomEditPage/RoomEditPage.jsx";

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    errorElement: <ErrorPage />,
  },
  {
    path: "login",
    element: <Login />,
  },
  {
    path: "register",
    element: <Registration />,
  },
  {
    path: "bookings",
    element: <BookingList />,
  },
  {
    path: "rooms/edit/:id",
    element: <RoomEditPage />,
  },
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <AuthProvider>
      <RouterProvider router={router} />
    </AuthProvider>
  </React.StrictMode>
);
