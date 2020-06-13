using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace FitnessTrackerAnalyzer.Model
{
    public class UserInfoImporter
    {
        private static List<UserDayTraining> Load(string fileName)
        {
            var regex = new Regex(@"day[0-9]+.json");
            if (!regex.IsMatch(Path.GetFileName(fileName)))
            {
                throw new Exception();
            }

            var strNumber = Path.GetFileName(fileName)
                .Replace("day", String.Empty)
                .Replace(".json", String.Empty);
            var dayNumber = Convert.ToInt32(strNumber);
            var content = File.ReadAllText(fileName);
            var userDayTrainings = JsonConvert.DeserializeObject<List<UserDayTraining>>(content);
            userDayTrainings.ForEach(userTrainingDescription=> userTrainingDescription.Number = dayNumber);
            return userDayTrainings;
        }

        public static List<UserTrainingInfo> Load(string[] fileNames)
        {
            var userDayTrainingsList= new List<UserDayTraining>();
            foreach (var fileName in fileNames)
            {
                userDayTrainingsList.AddRange(Load(fileName));
            }

            return userDayTrainingsList
                .GroupBy(userTrainingDescription => userTrainingDescription.User)
                .Select(groupTrainingByUser => new UserTrainingInfo
                {
                    Name = groupTrainingByUser.Key,
                    Trainings = groupTrainingByUser.Select(tr => new DayTraining
                    {
                        Number = tr.Number,
                        Rank = tr.Rank,
                        Status = tr.Status,
                        Steps = tr.Steps
                    }).ToList(),
                    AverageSteps = groupTrainingByUser.Select(tr => tr.Steps).Average(),
                    BestStepResult = groupTrainingByUser.Select(tr => tr.Steps).Max(),
                    WorstStepResult = groupTrainingByUser.Select(tr => tr.Steps).Min()

                }).ToList();
        }
    }
}