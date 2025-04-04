using System.ComponentModel.DataAnnotations;

namespace stay_link.Server.Models
{
    public class Booking : IValidatableObject
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public string? GroupName { get; set; }

        [Required(ErrorMessage = "Check-in date is required.")]
        public DateOnly CheckInDate { get; set; }

        [Required(ErrorMessage = "Check-out date is required.")]
        public DateOnly CheckOutDate { get; set; }

        [Required(ErrorMessage = "Room ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Room ID must be a positive number.")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Hotel ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Hotel ID must be a positive number.")]
        public int HotelId { get; set; }
        public decimal CleaningFee = 20m;
        public decimal BreakfastFee = 15m;

        [Range(0, 10, ErrorMessage = "Breakfast requests must be a non-negative integer and up to 10")]
        public int BreakfastRequests { get; set; }

        public int TotalGuests { get; set; }

        public decimal TotalPrice  { get; set; }

        public BookingStatus Status { get; set; }
        public virtual BookingUser User { get; set; }
        public virtual List<RoomFeature> FeaturePreferences { get; set; }
        public virtual List<BookingFeature> BookingPreferences { get; set; }
        [Required(ErrorMessage = "")]
        [Range(1, int.MaxValue, ErrorMessage = "Room ID must be a positive number.")]



        public virtual List<Room> Rooms { get; set; }

        public Booking() { }

        

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)  
        {  
            if(CheckInDate.CompareTo(CheckOutDate) < 0)  
            {  
                yield return new ValidationResult("End date must be greater than the start date.", new[] { "EndDate" });  
            }  

            if(Rooms.Count <= 0)
            {
                yield return new ValidationResult("Booking must have atleast 1 room.");
            }
        }  
    }
}
