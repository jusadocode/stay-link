import { Select, MenuItem, Typography, Box } from "@mui/material";

export default function ExtraStepGroup({
  selectedRooms,
  breakfastRequests,
  setBreakfastRequests,
}) {
  if (!selectedRooms) {
    return (
      <Box sx={{ mt: 2 }}>
        <Typography variant="h6">No room selected</Typography>
      </Box>
    );
  }

  const handleSelectChange = (event) => {
    setBreakfastRequests(event.target.value);
  };

  return (
    <Box sx={{ mt: 2, mb: 1 }}>
      <Typography variant="h6">
        Select the number of people opting for breakfast:
        <Select
          value={breakfastRequests}
          onChange={handleSelectChange}
          displayEmpty
          sx={{ minWidth: 100, marginInline: 2 }}
        >
          {Array.from(
            { length: selectedRooms.maxOccupancy + 1 },
            (_, index) => (
              <MenuItem key={index} value={index}>
                {index === 0 ? "None" : index}
              </MenuItem>
            )
          )}
        </Select>
      </Typography>

      <Typography variant="body2" sx={{ mb: 1 }}>
        (+€15 per person per day)
      </Typography>
    </Box>
  );
}
