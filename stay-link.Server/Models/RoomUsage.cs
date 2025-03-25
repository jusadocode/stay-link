using System.ComponentModel.DataAnnotations;

namespace stay_link.Server.Models
{
    public class RoomUsage
    {
        public int ID { get; set; }
        [Required]
        public int RoomID { get; set; }
        [Required]
        public double GeneralWear { get; set; }
        [Required]
        public CleaningState CleaningState { get; set; }
        [Required]

        public int TimesBookedThisYear { get; set; }
        [Required]
        public int TimesBookedSinceMaintenance { get; set; }
        public virtual Room Room { get; set; }

    }
}

