import { Box } from "@mui/material";
import React from "react";

export const Footer = ({ copyright, privacyPolicy }) => {
  return (
    <Box
      sx={{
        width: "100%",
        display: "flex",
        justifyContent: "space-between",
        marginBlock: "3rem",
        alignItems: "start",
      }}
    >
      <Box>{copyright}</Box>
      <Box>{privacyPolicy}</Box>
    </Box>
  );
};
