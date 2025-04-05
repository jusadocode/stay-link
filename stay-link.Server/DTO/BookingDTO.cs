using stay_link.Server.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace stay_link.Server.Models
{
    public class BookingDTO
    {
        public int Id { get; set; }

        public string GuestFullName { get; set; }

        public string GroupName { get; set; }


        public string CheckInDate { get; set; }


        public string CheckOutDate { get; set; }


        public List<int> RoomIds { get; set; }


        public int HotelId { get; set; }

        public int BreakfastRequests { get; set; }

        
    }
}
