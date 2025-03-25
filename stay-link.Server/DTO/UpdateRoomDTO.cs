using stay_link.Server.Models;

namespace stay_link.Server.DTO
{
    public class UpdateRoomDTO
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public RoomType RoomType { get; set; }
        public decimal Price { get; set; }
        public int HotelID { get; set; }
        public int MaxOccupancy { get; set; }
    }
}

