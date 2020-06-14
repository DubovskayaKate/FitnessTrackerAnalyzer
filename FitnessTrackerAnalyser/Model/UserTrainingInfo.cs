using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace FitnessTrackerAnalyzer.Model
{
    public class UserTrainingInfo
    {
        public string Name { get; set; }
        public double AverageSteps { get; set; }
        public int BestStepResult { get; set; }
        public int WorstStepResult { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public bool ShouldHighlight
        {
            get
            {
                return AverageSteps * 1.2 < BestStepResult 
                       || AverageSteps * 0.8 > WorstStepResult;
            }
        }

        public List<DayTraining> Trainings { get; set; } = new List<DayTraining>();

    }
}