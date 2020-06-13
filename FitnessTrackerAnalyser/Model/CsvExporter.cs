using System;
using System.IO;
using System.Linq;

namespace FitnessTrackerAnalyzer.Model
{
    public class CsvExporter : IExporter
    {
        public bool ExportData(string fileName, UserTrainingInfo userTrainingInfo)
        {
            var generalInfo = $"User,{userTrainingInfo.Name}\nAverage Steps,{userTrainingInfo.AverageSteps}\n" +
                              $"Best Step Result,{userTrainingInfo.BestStepResult}\nWorth Step Result,{userTrainingInfo.WorstStepResult}\n";
            var trainingHeader = $"Day,Rang,Status,Steps\n";
            var dayToDayInfo = string
                .Join("\n",
                    userTrainingInfo.Trainings
                        .OrderBy(training => training.Number)
                        .Select(training => $"Day{training.Number},{training.Rank},{training.Status},{training.Steps}"));
            try
            {
                File.WriteAllText(fileName, generalInfo + trainingHeader + dayToDayInfo );
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
    }
}