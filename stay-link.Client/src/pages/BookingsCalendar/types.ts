export interface RoomUsage {
  id: number;
  roomId: string; // Assuming room.id in your rooms array is also a number
  generalWear: number; // e.g., 0.12 (representing 12%)
  cleaningState: number; // e.g., 1 (needs interpretation, maybe 1=clean, 0=dirty?)
  timesBookedThisYear: number;
  timesBookedSinceMaintenance: number;
}

export interface Room {
  id: number; // Assuming number based on usageData
  title: string;
  roomType: string;
  // Add other room properties
}

export interface Booking {
  id: string | number;
  roomIds: number[]; // Assuming number based on usageData
  guestFullName: string;
  checkInDate: string; // ISO date string
  checkOutDate: string; // ISO date string
  // Add other booking properties
}
