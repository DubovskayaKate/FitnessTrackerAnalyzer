using System;
using System.IO;
using System.Xml.Serialization;

namespace FitnessTrackerAnalyzer.Model
{
    public class XmlExporter : IExporter
    {
        public bool ExportData(string fileName, UserInfo userInfo)
        {
            try
            {
                var formatter = new XmlSerializer(typeof(UserInfo));

                using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, userInfo);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

            //var memoryStream = new MemoryStream();
            //var serializer = new XmlSerializer(typeof(SerializeMe));
            //var content = new SerializeMe("zero", "one");

            //using (var sw = new StreamWriter(memoryStream, new UTF8Encoding(false), 1024, true))
            //{
            //    serializer.Serialize(XmlWriter.Create(sw), content);
            //    await memoryStream.FlushAsync();
            //}
        }
    }
}