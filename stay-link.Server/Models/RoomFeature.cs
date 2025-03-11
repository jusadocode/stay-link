using stay_link.Server.Data;
using System.Globalization;

namespace stay_link.Server.Models
{
    public class RoomFeature
    {
        public int ID { get; set; }
        public string RoomId { get; set; }
        public string FeatureId { get; set; }
        public FeatureCondition Condition { get; set; }
    }
}
