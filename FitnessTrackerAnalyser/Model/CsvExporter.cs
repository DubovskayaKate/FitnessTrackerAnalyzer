using System;
using System.IO;
using System.Linq;

namespace FitnessTrackerAnalyzer.Model
{
    public class CsvExporter : IExporter
    {
        public bool ExportData(string fileName, UserInfo userInfo)
        {
            var generalInfo = $"User,{userInfo.Name}\n Average Steps,{userInfo.AverageSteps}\n" +
                              $"Best Step Result,{userInfo.BestStepResult}\nWorth Step Result,{userInfo.WorstStepResult}\n";
            var trainingHeader = $"Day,Rang,Status,Steps\n";
            var dayToDayInfo = string
                .Join("\n",
                    userInfo.Trainings
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