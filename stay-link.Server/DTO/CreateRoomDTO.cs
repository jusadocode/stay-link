using stay_link.Server.Models;

namespace stay_link.Server.DTO
{
    public class CreateRoomDTO
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public RoomType RoomType { get; set; }
        public decimal Price { get; set; }
        public int HotelId { get; set; }
        public int MaxOccupancy { get; set; }
        public List<RoomFeature> Features { get; set; }
    }
}
