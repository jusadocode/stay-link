import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import App from "./App.jsx";
import BookingList from "./components/BookingList.jsx";
import ErrorPage from "./components/ErrorPage.jsx";
import "./index.css";
import Login from "./pages/Login/Login.jsx";
import Registration from "./pages/Registration/Registration.jsx";

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
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
