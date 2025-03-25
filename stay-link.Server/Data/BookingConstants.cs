namespace stay_link.Server.Data
{
    public static class BookingConstants
    {
        public const int MaintenanceThreshold = 1; // Number of bookings before maintenance is required
        public const double WearThreshold = 0.8; // General wear threshold for maintenance
        public const double WearIncreasePerBooking = 0.01; // Wear increase per booking
        public const double WearReductionAfterMaintenance = 0.5; // Wear reduction after maintenance

        public const int CleaningThreshold = 5; // Number of bookings before cleaning is required
        public const double CleaningWearThreshold = 0.6; // Wear threshold for cleaning

        public const int MaxOccupancyDefault = 2; // Default maximum occupancy for rooms
        public const int MaxBookingDurationDays = 30; // Maximum allowed booking duration in days
        public const int MinBookingAdvanceDays = 1; // Minimum days in advance for booking

        public const double BasePricePerNight = 100.0; // Base price per night for a standard room
        public const double PriceIncreasePerGuest = 20.0; // Price increase per additional guest
        public const double DiscountForLongStay = 0.1; // 10% discount for stays longer than 7 days

        public const string FeatureBalcony = "Balcony";
        public const string FeatureAC = "AC";
        public const string FeatureSeaView = "SeaView";

        public const string RoomTypeStandard = "Standard";
        public const string RoomTypeDeluxe = "Deluxe";
        public const string RoomTypeSuite = "Suite";

        public const string DefaultCheckInTime = "15:00"; 
        public const string DefaultCheckOutTime = "11:00"; 

        public const string RoomNotAvailableMessage = "The selected room is not available for the specified dates.";
        public const string MaxOccupancyExceededMessage = "The number of guests exceeds the maximum occupancy for this room.";
        public const string InvalidBookingDurationMessage = "The booking duration exceeds the maximum allowed duration.";
        public const string MaintenanceRequiredMessage = "This room is currently under maintenance and cannot be booked.";

        public const string LoggingSourceName = "StayLinkBookingSystem";
        public const string LoggingFileName = "booking_logs.txt";

        public const int ApiTimeoutSeconds = 30;

        public const int PasswordMinLength = 8;
        public const int PasswordMaxLength = 20;
        public const int SessionTimeoutMinutes = 30;
    }
}
