using System.IO;
using System.Linq;

namespace FitnessTrackerAnalyzer.Model
{
    public class CsvExporter : IExporter
    {
        public bool ExportData(string fileName, UserTrainingInfo userTrainingInfo)
        {
            var generalInfo = $"User,{userTrainingInfo.Name}\n" +
                              $"Average Steps,{userTrainingInfo.AverageSteps}\n" +
                              $"Best Step Result,{userTrainingInfo.BestStepResult}\n" +
                              $"Worth Step Result,{userTrainingInfo.WorstStepResult}\n";

            var trainingHeader = $"Day,Rang,Status,Steps\n";

            var dayToDayInfo = string.Join("\n", userTrainingInfo.Trainings
                        .OrderBy(training => training.Number)
                        .Select(training => $"Day{training.Number},{training.Rank},{training.Status},{training.Steps}"));
            try
            {
                File.WriteAllText(fileName, generalInfo + trainingHeader + dayToDayInfo );
                return true;
            }
            catch
            {
                // Ignore
                return false;
            }


        }
    }
}