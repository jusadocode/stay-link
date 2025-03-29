using stay_link.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace stay_link.Server.DTO
{
    public class RoomFeatureDetailsDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }
        public decimal? ExtraCost { get; set; }

        public int RoomCount { get; set; }
        public int BookingCount { get; set; }

    }
}
