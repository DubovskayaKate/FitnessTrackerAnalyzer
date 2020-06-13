using System.Collections.Generic;
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
        private UserInfoViewModel DataViewModel { get; set; }

        private UserInfo SelectedRow { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataViewModel = new UserInfoViewModel();
            DataContext = DataViewModel;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "JSON file (*.json)|day*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                DataViewModel.Users = UserInfoImporter
                    .Load(openFileDialog.FileNames);
            }
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON file (*.json)|*.json|XML (*.xml)|*.xml|CSV (*.csv)|*.csv";
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

                if (exporter.ExportData(saveFileDialog.FileName, DataViewModel.SelectedUser))
                {
                    MessageBox.Show("User info have been saved");
                }
                else
                {
                    MessageBox.Show("Can't save info");
                }
            }
        }
    }

}
