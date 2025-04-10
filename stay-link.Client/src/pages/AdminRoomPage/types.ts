interface Room {
  id: number | string;
  title: string;
  roomType: string;
  maxOccupancy: number;
  price: number;
  imageUrl?: string;
}

interface RoomGroup {
  rooms: Room[];
  totalPrice: number;
}

interface RoomCreationRequest {
  title: string;
  summary: string;
  roomType: string;
  price: number;
  maxOccupancy: number;
  featureIds: number[];
}

interface RoomUsage {
  id: number;
  roomId: number;
  generalWear: number;
  cleaningState: string;
  timesBookedThisYear: number;
  timesBookedSinceMaintenance: number;
}
