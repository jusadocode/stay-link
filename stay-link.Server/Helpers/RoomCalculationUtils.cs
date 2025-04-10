using stay_link.Server.Models;

namespace stay_link.Server.Helpers
{
    public static class RoomCalculationUtils
    {
        public static double CalculateCombinationScore(List<Room> combo, List<RoomFeature> preferences, int totalGuestCount, Dictionary<Room, double> individualScores)
        {
            if (combo == null || !combo.Any()) return 0;

            // Option 1: Average individual scores (simple)
            double avgIndividualScore = combo.Average(r => individualScores.ContainsKey(r) ? individualScores[r] : 0.0);

            // Option 2: More complex - check if *all* high-priority preferences are met *somewhere* in the combo

            // Option 3: Factor in occupancy fit strongly
            double totalCapacity = combo.Sum(r => r.MaxOccupancy);
            double occupancyFitScore = (totalCapacity == 0) ? 0 : Math.Max(0.0, 1.0 - ((totalCapacity - totalGuestCount) / (double)totalGuestCount) * 0.5); // Penalize overcapacity, score ~1 if close fit

            // Example weighted score
            return (0.6 * avgIndividualScore) + (0.4 * occupancyFitScore); // Adjust weights
        }


    }
}
