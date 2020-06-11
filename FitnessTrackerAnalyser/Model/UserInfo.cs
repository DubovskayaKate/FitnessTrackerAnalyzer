using System.Collections.Generic;
using System.Windows.Documents;

namespace FitnessTrackerAnalyzer.Model
{
    public class UserInfo
    {
        public string Name { get; set; }
        public double AverageSteps { get; set; }
        public int BestStepResult { get; set; }
        public int WorstStepResult { get; set; }

        public List<DayTraining> Trainings { get; set; }

        public UserInfo()
        {
            Trainings = new List<DayTraining>();
        }
    }
}