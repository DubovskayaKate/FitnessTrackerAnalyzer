using System;
using System.IO;
using System.Xml.Serialization;

namespace FitnessTrackerAnalyzer.Model
{
    public class XmlExporter : IExporter
    {
        public bool ExportData(string fileName, UserTrainingInfo userTrainingInfo)
        {
            try
            {
                var formatter = new XmlSerializer(typeof(UserTrainingInfo));

                using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, userTrainingInfo);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}