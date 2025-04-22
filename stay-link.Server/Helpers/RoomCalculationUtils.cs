using stay_link.Server.Models.Rooms;

namespace stay_link.Server.Helpers
{
    public static class RoomCalculationUtils
    {
        public static double CalculateCombinationScore(List<Room> combo, List<RoomFeature> preferences, int totalGuestCount, Dictionary<Room, double> individualScores)
        {
            if (combo == null || !combo.Any()) return 0;

            double avgIndividualScore = combo.Average(r => individualScores.ContainsKey(r) ? individualScores[r] : 0.0);

            double totalCapacity = combo.Sum(r => r.MaxOccupancy);
            double occupancyFitScore = totalCapacity == 0 ? 0 : Math.Max(0.0, 1.0 - (totalCapacity - totalGuestCount) / totalGuestCount * 0.5); 

            return 0.6 * avgIndividualScore + 0.4 * occupancyFitScore; 
        }


    }
}
