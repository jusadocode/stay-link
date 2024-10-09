using stay_link.Server.Data;

namespace stay_link.Server.Models
{
    public class Room
    {
        public int ID { get; set; }

        public decimal Price => RoomType switch
        {
            RoomType.Standart => 100m,
            RoomType.Deluxe => 150m,
            RoomType.Suite => 200m,
            _ => throw new ArgumentOutOfRangeException()
        };

        public string? Summary { get; set; }
        public RoomType RoomType { get; set; }
        public int HotelID { get; set; }
        public int NumOfGuests { get; set; }

        public Room(string? summary, RoomType roomType, int hotelID, int numberOfguests)
        {
            Summary = summary;
            RoomType = roomType;
            HotelID = hotelID;
            NumOfGuests = numberOfguests;
        }

        public Room() { }
    }


}
