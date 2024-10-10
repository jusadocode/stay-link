namespace stay_link.Server.Models
{
    public class HotelDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public List<Room> Rooms { get; set; }
    }
}