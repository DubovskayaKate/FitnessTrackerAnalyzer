using System.Collections.Generic;
using System.Windows;
using FitnessTrackerAnalyzer.Model;
using FitnessTrackerAnalyzer.ViewModel;

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
            //DataGrid.ItemsSource = DataViewModel.Users;
            //DataGrid.SelectedItem = DataViewModel.SelectedUser;

        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            DataViewModel.Users = UserInfoImporter.Load(new List<string>{"TestData/day1.json", "TestData/day2.json"});

            //DataGrid.Items.Refresh();
        }

        private void RowCopyCommand(object sender, RoutedEventArgs e)
        {
            new CsvExporter().ExportData("1.csv",
                new UserInfo
                {
                    AverageSteps = 1234, Name = "Katya",
                    BestStepResult = 4000, WorstStepResult = 123,
                    Trainings = new List<DayTraining>
                    {
                        new DayTraining {Number = 1, Rank = 2, Status = "Finished", Steps = 1234},
                        new DayTraining {Number = 2, Rank = 5, Status = "Finished", Steps = 1904},
                    }

                });
        }
    }

}
