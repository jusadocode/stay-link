namespace stay_link.Server.Models
{
    public class Booking
    {
        public int ID { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public decimal CleaningFee = 20m;
        public decimal BreakfastFee = 15m;
        public int BreakfastRequests { get; set; } = 0; // Amount of people choosing to take the breakfast option

        public Booking() { }
    }
}
