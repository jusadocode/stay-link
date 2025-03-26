import React from "react";
import { Box } from "@mui/material";
import Apartment from "@mui/icons-material/Apartment";
import { keyframes } from "@emotion/react";

const shake = keyframes`
  0% { transform: translate(0px, 0px); color: #ccc; }
  25% { transform: translate(1px, -1px); color:rgb(9, 116, 238); }
  50% { transform: translate(-1px, 1px); color:rgb(0, 0, 0); }
  75% { transform: translate(1px, 1px); color: #009688; }
  100% { transform: translate(0px, 0px); color: #ccc; }
`;

const LoadingIndicator = () => (
  <Box display="flex" alignItems="center" justifyContent="center">
    <Apartment
      sx={{
        fontSize: 40,
        animation: `${shake} 1.2s infinite ease-in-out`,
      }}
    />
  </Box>
);

export default LoadingIndicator;
