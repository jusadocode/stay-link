using stay_link.Server.Models;

namespace stay_link.Server.DTO
{
    public class RoomGroupDTO
    {
        public List<RoomDTO> Rooms { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
