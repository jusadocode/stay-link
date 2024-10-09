namespace stay_link.Server.Models
{
    public class BookingDTO
    {
        public int ID { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public decimal CleaningFee = 20m;
        public decimal BreakfastFee = 15m;
        public int BreakfastRequests { get; set; } = 0; // Amount of people choosing to take the breakfast option
    }
}
