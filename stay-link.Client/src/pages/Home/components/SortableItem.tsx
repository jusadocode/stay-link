import { useSortable } from "@dnd-kit/sortable";
import { Button, IconButton, ListItem } from "@mui/material";
import React from "react";
import { CSS } from "@dnd-kit/utilities";
import CloseIcon from "@mui/icons-material/Close";

function SortableItem({ id, label, onRemove }) {
  const {
    attributes,
    listeners,
    setNodeRef,
    transform,
    transition,
    isDragging,
  } = useSortable({
    id,
  });

  const style = {
    transform: CSS.Transform.toString(transform),
    transition,
    opacity: isDragging ? 0.5 : 1,
    backgroundColor: "#e0f7fa",
    marginBottom: "8px",
    borderRadius: "8px",
    cursor: "pointer",
  };

  return (
    <ListItem
      ref={setNodeRef}
      style={style}
      disablePadding
      sx={{
        "&:hover": {
          backgroundColor: "#FFD95F",
        },
        "&:hover .remove-btn": {
          opacity: 1,
        },
      }}
      secondaryAction={
        <IconButton
          edge="end"
          aria-label="delete"
          className="remove-btn"
          onClick={(e) => {
            e.stopPropagation();
            e.preventDefault();
            onRemove();
          }}
          sx={{ opacity: 0, transition: "opacity 0.2s" }}
        >
          <CloseIcon fontSize="small" />
        </IconButton>
      }
      {...attributes}
      {...listeners}
    >
      <Button fullWidth variant="contained" disableElevation>
        {label}
      </Button>
    </ListItem>
  );
}

export default SortableItem;
