using System;
using System.ComponentModel.DataAnnotations;

namespace stay_link.Server.Models
{
    public class BookingDTO : IValidatableObject
    {
        [Required(ErrorMessage = "Check-in date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Check-in date must be a valid date.")]
        public string CheckInDate { get; set; }

        [Required(ErrorMessage = "Check-out date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Check-out date must be a valid date.")]
        public string CheckOutDate { get; set; }

        [Required(ErrorMessage = "Room ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Room ID must be a positive number.")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Hotel ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Hotel ID must be a positive number.")]
        public int HotelId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Breakfast requests must be a non-negative integer.")]
        public int BreakfastRequests { get; set; } = 0;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Attempt to parse CheckInDate
            if (!DateOnly.TryParse(CheckInDate, out var checkInDate))
            {
                yield return new ValidationResult(
                    "Check-in date is not a valid date.",
                    new[] { nameof(CheckInDate) }
                );
            }

            // Attempt to parse CheckOutDate
            if (!DateOnly.TryParse(CheckOutDate, out var checkOutDate))
            {
                yield return new ValidationResult(
                    "Check-out date is not a valid date.",
                    new[] { nameof(CheckOutDate) }
                );
            }

            // Additional check: CheckOutDate should be after CheckInDate
            if (checkInDate != default && checkOutDate != default)
            {
                if (checkOutDate <= checkInDate)
                {
                    yield return new ValidationResult(
                        "Check-out date must be after the check-in date.",
                        new[] { nameof(CheckOutDate) }
                    );
                }
            }
        }
    }
}
