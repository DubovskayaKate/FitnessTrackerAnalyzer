namespace FitnessTrackerAnalyzer.Model
{
    public interface IExporter
    {
        bool ExportData(string fileName);
    }
}