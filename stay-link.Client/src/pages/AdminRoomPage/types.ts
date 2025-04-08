interface Room {
  id: number | string;
  title: string;
  roomType: string; // Adjust based on RoomTypes keys
  maxOccupancy: number;
  price: number;
  imageUrl?: string;
  // Add other relevant fields displayed or needed for delete/edit
}

interface AdminRoomListProps {}
