using stay_link.Server.Models;

namespace stay_link.Server.DTO.Rooms
{
    public class RoomOfferDTO
    {
        public List<RoomDTO> Rooms { get; set; }
        public decimal TotalPrice { get; set; }
        public string Description { get; set; }
    }
}
