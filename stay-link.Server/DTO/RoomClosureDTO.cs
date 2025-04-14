namespace stay_link.Server.DTO
{
    public class RoomClosureDTO
    {
        public int RoomId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Reason { get; set; }
    }
}
