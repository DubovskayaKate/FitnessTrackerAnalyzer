using System;
using System.IO;
using System.Windows;
using FitnessTrackerAnalyzer.Model;
using FitnessTrackerAnalyzer.ViewModel;
using Microsoft.Win32;

namespace FitnessTrackerAnalyzer
{
    public partial class MainWindow : Window
    {
        private const string OpenDialogFilter = "JSON file (*.json)|day*.json";
        private const string SaveDialogFilter = "JSON file (*.json)|*.json|XML (*.xml)|*.xml|CSV (*.csv)|*.csv";
        private UserInfoViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new UserInfoViewModel();
            DataContext = ViewModel;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = true, 
                Filter = OpenDialogFilter
            };
            if (openFileDialog.ShowDialog() == true)
            {
                ViewModel.Users = UserInfoImporter
                    .Load(openFileDialog.FileNames);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = SaveDialogFilter
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                var chosenExtension = Path.GetExtension(saveFileDialog.FileName);
                IExporter exporter = chosenExtension switch
                {
                    ".json" => new JsonExporter(),
                    ".xml" => new XmlExporter(),
                    ".csv" => new CsvExporter(),
                    _ => new JsonExporter()
                };

                var exportResult = exporter.ExportData(saveFileDialog.FileName, ViewModel.SelectedUserTrainingInfo);
                MessageBox.Show(exportResult ? "User info have been saved" : "Couldn't save info");
            }
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((string) Application.Current.FindResource("HelpMessage"), "Fitness Tracker Analyzer");
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((string)Application.Current.FindResource("AboutMessage"), "Fitness Tracker Analyzer");
        }
    }
}
