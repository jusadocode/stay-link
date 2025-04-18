namespace stay_link.Server.DTO.RoomOperations
{
    public class RoomStatsDTO
    {
        public int RoomId { get; set; }
        public string RoomTitle { get; set; }
        public string RoomType { get; set; }
        public double Occupancy { get; set; }
        public decimal Revenue { get; set; }
        public int Reservations { get; set; }
        public int Nights { get; set; }
        public decimal ADR { get; set; }
        public double LeadTime { get; set; }
        public double LoS { get; set; }
        public decimal RevPar { get; set; }
        public int TotalLoS { get; set; }
        public double GeneralWear { get; set; }
    }
}
