using stay_link.Server.Models;

namespace stay_link.Server.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Summary { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
        public int HotelId { get; set; }
        public int MaxOccupancy { get; set; }
        public List<RoomFeatureDTO> Features { get; set; }
    }
}
