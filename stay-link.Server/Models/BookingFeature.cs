using stay_link.Server.Data;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace stay_link.Server.Models
{
    public class BookingFeature
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public decimal? ExtraCost { get; set; }

        public virtual List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
