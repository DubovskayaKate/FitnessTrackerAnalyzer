namespace FitnessTrackerAnalyzer.Model
{
    public class DayTraining
    {
        public int Rank { get; set; }
        public string Status { get; set; }
        public int Steps { get; set; }

        // Should be set using source file name 
        public int Number { get; set; } 
    }
}