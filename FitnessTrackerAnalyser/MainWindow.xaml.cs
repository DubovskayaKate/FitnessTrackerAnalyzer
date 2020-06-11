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
        private UserInfoViewModel DataView;
        public MainWindow()
        {
            InitializeComponent();
            DataView = new UserInfoViewModel();
            DataGrid.ItemsSource = DataView.Users;

        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            DataView.Users.AddRange(UserInfoImporter.Load(new List<string>{"TestData/day1.json", "TestData/day2.json"}));

            DataGrid.Items.Refresh();
        }
    }

}
