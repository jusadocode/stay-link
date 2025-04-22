namespace stay_link.Server.Data
{
    public static class BookingConstants
    {
        public const double GeneralCleaningThreshold = 0.4; 
        public const double DeepMaintenanceThreshold = 0.8; 
        public const double WearThreshold = 0.8; 
        public const double WearIncreasePerBooking = 0.10; 
        public const double WearReductionAfterMaintenance = 0.5;

        public const int DeepCleaningThreshold = 2; 
        public const double CleaningWearThreshold = 0.6; 

        public const int MaxOccupancyDefault = 2; 
        public const int MaxBookingDurationDays = 30; 
        public const int MinBookingAdvanceDays = 1; 

        public const double BasePricePerNight = 100.0; 
        public const double PriceIncreasePerGuest = 20.0; 
        public const double DiscountForLongStay = 0.1; 

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
