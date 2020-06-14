using System.IO;
using Newtonsoft.Json;

namespace FitnessTrackerAnalyzer.Model
{
    public class JsonExporter : IExporter
    {
        public bool ExportData(string filePath, UserTrainingInfo userTrainingInfo)
        {
            var json = JsonConvert.SerializeObject(userTrainingInfo);
            try
            {
                File.WriteAllText(filePath, json);
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