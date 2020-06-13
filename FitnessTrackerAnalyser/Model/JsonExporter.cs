using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Newtonsoft.Json;

namespace FitnessTrackerAnalyzer.Model
{
    public class JsonExporter : IExporter
    {
        public bool ExportData(string filePath, UserInfo userInfo)
        {
            var json = JsonConvert.SerializeObject(userInfo);
            try
            {
                File.WriteAllText(filePath, json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}