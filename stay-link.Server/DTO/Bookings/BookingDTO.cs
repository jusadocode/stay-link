using stay_link.Server.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace stay_link.Server.DTO.Bookings
{
    public class BookingDTO
    {
        public int Id { get; set; }

        public string? DisplayName { get; set; }
        public string Status { get; set; }

        public string CheckInDate { get; set; }


        public string CheckOutDate { get; set; }


        public List<int> RoomIds { get; set; }


        public int TotalGuests { get; set; }

        public int BreakfastRequests { get; set; }


    }
}
