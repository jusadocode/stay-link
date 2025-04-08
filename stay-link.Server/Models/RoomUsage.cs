using System.ComponentModel.DataAnnotations;

namespace stay_link.Server.Models
{
    public class RoomUsage
    {
        public int Id { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public double GeneralWear { get; set; } = 0.0;
        [Required]
        public CleaningState CleaningState { get; set; } = CleaningState.Clean;
        [Required]

        public int TimesBookedThisYear { get; set; } = 0;
        [Required]
        public int TimesBookedSinceMaintenance { get; set; } = 0;
        public virtual Room Room { get; set; }

    }
}

