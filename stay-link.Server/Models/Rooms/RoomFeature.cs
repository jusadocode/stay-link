using stay_link.Server.Data;
using stay_link.Server.Models.Bookings;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace stay_link.Server.Models.Rooms
{
    public class RoomFeature
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
        public decimal? ExtraCost { get; set; }

        public virtual List<Room> Rooms { get; set; } = new List<Room>();
        public virtual List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
