namespace stay_link.Server.Models.Rooms
{
    public class RoomOffer
    {
        public List<Room> Rooms { get; set; }
        public double Score { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
