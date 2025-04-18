using stay_link.Server.Models.Rooms;

namespace stay_link.Server.Models.RoomOperations
{
    public class RoomClosure
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public string Reason { get; set; }
    }
}
