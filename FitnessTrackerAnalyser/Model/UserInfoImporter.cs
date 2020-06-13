using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FitnessTrackerAnalyzer.Model
{
    public class UserInfoImporter
    {
        private static List<UserTrainingDescription> Load(string fileName)
        {
            var regex = new Regex(@"day[0-9]+.json");
            if (!regex.IsMatch(Path.GetFileName(fileName)))
            {
                throw new Exception();
            }

            var strNumber = Path.GetFileName(fileName)
                .Replace("day", String.Empty)
                .Replace(".json", String.Empty);
            int dayNumber = Convert.ToInt32(strNumber);
            var content = File.ReadAllText(fileName);
            var list = JsonConvert.DeserializeObject<List<UserTrainingDescription>>(content);
            list.ForEach(userTrainingDescription=> userTrainingDescription.Number = dayNumber);
            return list;
        }

        public static List<UserInfo> Load(List<string> fileNames)
        {
            var listOfTrainingDescription= new List<UserTrainingDescription>();
            foreach (var fileName in fileNames)
            {
                listOfTrainingDescription.AddRange(Load(fileName));
            }

            return listOfTrainingDescription
                .GroupBy(item => item.User)
                .Select(group => new UserInfo
                {
                    Name = group.Key,
                    Trainings = group.Select(tr => new DayTraining
                    {
                        Number = tr.Number,
                        Rank = tr.Rank,
                        Status = tr.Status,
                        Steps = tr.Steps
                    }).ToList(),
                    AverageSteps = group.Select(tr => tr.Steps).Average(),
                    BestStepResult = group.Select(tr => tr.Steps).Max(),
                    WorstStepResult = group.Select(tr => tr.Steps).Min()

                }).ToList();
        }
    }
}