using System;
using System.ComponentModel.DataAnnotations;

namespace stay_link.Server.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Summary cannot exceed 500 characters and must be atleast 10 characters long.", MinimumLength = 10)]
        public string? Summary { get; set; }
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Room type is required.")]
        [Range(0, 2, ErrorMessage = "Room type value is between 0 and 2.")]
        public RoomType RoomType { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Price can't be negative.")]
        public decimal Price {get;set;}

        [Required(ErrorMessage = "Hotel ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Hotel ID must be a positive number.")]
        public int HotelID { get; set; }
        public int FloorNumber { get; set; }

        [Required(ErrorMessage = "Max occupancy is required.")]
        [Range(1, 10, ErrorMessage = "Max occupancy must be between 1 and 10.")]
        public int MaxOccupancy { get; set; }
        public virtual List<RoomFeature> Features { get; set; } = new List<RoomFeature>();

        public virtual List<Booking> Bookings { get; set; }

        public virtual RoomUsage RoomUsage { get; set; }


        public Room() { }

        
    }
}
