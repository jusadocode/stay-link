import { Box, TextField, Typography } from "@mui/material";
import React from "react";

export default function CheckInStep() {
  return (
    <>
      <Box
        sx={{
          mt: 2,
          mb: 1,
          display: "flex",
          flexDirection: "column",
          alignItems: "flex-start",
          gap: "2rem",
        }}
      >
        <Typography sx={{ my: 2 }}>
          Do you have any information to provide?
        </Typography>
      </Box>
    </>
  );
}
