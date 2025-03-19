namespace stay_link.Server.Models
{
    public class RoomUsage
    {
        public int ID { get; set; }
        public int RoomID { get; set; }
        public double GeneralWear { get; set; }

        public CleaningState CleaningState { get; set; }
        public int TimesBookedThisYear { get; set; }
        public int TimesBookedSinceMaintenance { get; set; }

    }
}

