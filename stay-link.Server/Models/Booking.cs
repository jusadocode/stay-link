using System;
using System.ComponentModel.DataAnnotations;

namespace stay_link.Server.Models
{
    public class Booking : IValidatableObject
    {
        public int ID { get; set; }

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

        public Booking() { }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)  
        {  
            if(CheckInDate.CompareTo(CheckOutDate) < 0)  
            {  
                yield return new ValidationResult("End date must be greater than the start date.", new[] { "EndDate" });  
            }  
        }  
    }
}
