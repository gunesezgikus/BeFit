namespace BeFit.Models
{
    public class ExerciseStats
    {
        public string ExerciseName { get; set; } = "";
        public int TotalSessions { get; set; }
        public int TotalReps { get; set; }
        public float AvgLoad { get; set; }
        public float MaxLoad { get; set; }
    }
}
