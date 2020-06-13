using System.Threading.Tasks;

namespace FitnessTrackerAnalyzer.Model
{
    public interface IExporter
    {
        bool ExportData(string fileName, UserInfo userInfo);
    }
}