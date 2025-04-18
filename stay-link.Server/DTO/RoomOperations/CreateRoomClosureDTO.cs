namespace stay_link.Server.DTO.RoomClosure
{
    public class CreateRoomClosureDTO
    {
        public int RoomId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Reason { get; set; }
    }
}
