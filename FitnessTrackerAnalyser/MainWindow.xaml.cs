using System;
using System.IO;
using System.Windows;
using FitnessTrackerAnalyzer.Model;
using FitnessTrackerAnalyzer.ViewModel;
using Microsoft.Win32;

namespace FitnessTrackerAnalyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string OpenDialogFilter = "JSON file (*.json)|day*.json";
        private const string SaveDialogFilter = "JSON file (*.json)|*.json|XML (*.xml)|*.xml|CSV (*.csv)|*.csv";
        private UserInfoViewModel DataViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataViewModel = new UserInfoViewModel();
            DataContext = DataViewModel;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = OpenDialogFilter;
            if (openFileDialog.ShowDialog() == true)
            {
                DataViewModel.Users = UserInfoImporter
                    .Load(openFileDialog.FileNames);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = SaveDialogFilter;
            if (saveFileDialog.ShowDialog() == true)
            {
                var chosenExtension = Path.GetExtension(saveFileDialog.FileName);
                IExporter exporter;
                switch (chosenExtension)
                {
                    case ".json": exporter = new JsonExporter();break;
                    case ".xml": exporter = new XmlExporter();break;
                    case ".csv": exporter = new CsvExporter(); break;
                    default: exporter = new JsonExporter(); break;
                }

                if (exporter.ExportData(saveFileDialog.FileName, DataViewModel.SelectedUserTrainingInfo))
                {
                    MessageBox.Show("User info have been saved");
                }
                else
                {
                    MessageBox.Show("Couldn't save info");
                }
            }
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "1) Load Data - File->Load Data. If load data two times, last loaded data will be used\n" +
                "2) File name - Must be in format day[anyNumber].json\n" +
                "3) Show User Training Plot - Select row with the user\n" +
                "4) Users marked with green color have the difference between average steps and best or worst result more than 20%", 
                "Fitness Tracker Analyzer");
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The program collects and process user training info, shows it using plot and grid, exports user day-to-day training  to Json, Xml, Csv\n" +
                            "@Copyright Dubovskaya Kate, 2020", "Fitness Tracker Analyzer");
        }
    }

}
