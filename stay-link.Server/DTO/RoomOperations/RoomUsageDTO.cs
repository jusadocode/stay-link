using System.ComponentModel.DataAnnotations;
using stay_link.Server.Models;

namespace stay_link.Server.DTO.RoomOperations
{
    public class RoomUsageDTO
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public double GeneralWear { get; set; }
        public string CleaningState { get; set; }

        public int TimesBookedThisYear { get; set; }
        public int TimesBookedSinceMaintenance { get; set; }
    }
}
