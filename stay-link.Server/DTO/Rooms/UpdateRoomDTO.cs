using stay_link.Server.Models;

namespace stay_link.Server.DTO.Rooms
{
    public class UpdateRoomDTO
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
        public int HotelId { get; set; }
        public int MaxOccupancy { get; set; }
        public double GeneralWear { get; set; }
    }
}

