using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Newtonsoft.Json;

namespace FitnessTrackerAnalyzer.Model
{
    public class UserInfoImporter
    {
        private static bool Load(string fileName, out List<UserDayTraining> loadedTrainings)
        {
            var regex = new Regex(@"day[0-9]+.json");
            if (!regex.IsMatch(Path.GetFileName(fileName)))
            {
                loadedTrainings = new List<UserDayTraining>();
                return false;
            }

            var strNumber = Path.GetFileName(fileName)
                .Replace("day", String.Empty)
                .Replace(".json", String.Empty);

            var dayNumber = Convert.ToInt32(strNumber);
            var content = File.ReadAllText(fileName);
            var userDayTrainings = JsonConvert.DeserializeObject<List<UserDayTraining>>(content);

            userDayTrainings.ForEach(userTrainingDescription => userTrainingDescription.Number = dayNumber);

            loadedTrainings = userDayTrainings;
            return true;
        }

        public static List<UserTrainingInfo> Load(string[] fileNames)
        {
            var userDayTrainingsList= new List<UserDayTraining>();
            var incorrectFileNames = new List<string>();
            foreach (var fileName in fileNames)
            {
                if (Load(fileName, out List<UserDayTraining> trainings))
                {
                    userDayTrainingsList.AddRange(trainings);
                }
                else
                {
                    incorrectFileNames.Add(fileName);
                }
            }

            if (incorrectFileNames.Count != 0)
            {
                var message = 
                    "All file loaded except " 
                    + string.Join(", ", incorrectFileNames.Select(filename => Path.GetFileName(filename)))
                    + ". Continue only with loaded files?";
                var header = "Fitness Tracker Analyzer";
                var result = MessageBox.Show(message, header, MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No) return new List<UserTrainingInfo>();
            }

            return userDayTrainingsList
                .GroupBy(userTrainingDescription => userTrainingDescription.User)
                .Select(groupTrainingByUser => new UserTrainingInfo
                {
                    Name = groupTrainingByUser.Key,
                    Trainings = groupTrainingByUser
                        .Select(tr => new DayTraining
                        {
                            Number = tr.Number,
                            Rank = tr.Rank,
                            Status = tr.Status,
                            Steps = tr.Steps
                        })
                        .ToList(),
                    AverageSteps = groupTrainingByUser.Select(tr => tr.Steps).Average(),
                    BestStepResult = groupTrainingByUser.Select(tr => tr.Steps).Max(),
                    WorstStepResult = groupTrainingByUser.Select(tr => tr.Steps).Min()

                }).ToList();
        }
    }
}