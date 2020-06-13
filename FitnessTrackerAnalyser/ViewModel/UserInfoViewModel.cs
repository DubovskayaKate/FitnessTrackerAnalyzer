using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FitnessTrackerAnalyzer.Model;

namespace FitnessTrackerAnalyzer.ViewModel
{
    public class UserInfoViewModel : INotifyPropertyChanged
    {
        private List<UserInfo> _users;

        public List<UserInfo> Users
        {
            get => _users;
            set
            {
                _users = value;
                NotifyPropertyChanged();
            }
        }
        private List<Point> _trainingResultPoint = new List<Point>{new Point(0,0), new Point(1,1), new Point(2, 4), new Point(3, 8) };
        public IEnumerable<Point> TrainingResultPoint => _trainingResultPoint;


        private List<Point> _averageSteps = new List<Point> { new Point(0, 3), new Point(3, 3), };
        public IEnumerable<Point> AverageSteps => _averageSteps;

        private List<Point> _maxStepPoints = new List<Point> { new Point(2, 4), new Point(3, 4), };
        public IEnumerable<Point> MaxStepsPoints => _maxStepPoints;

        private List<Point> _minStepsPoints = new List<Point> { new Point(0, 1), new Point(4, 1), };
        public IEnumerable<Point> MinStepsPoints => _minStepsPoints;
        public UserInfo SelectedUser { get; set; }

        public UserInfoViewModel()
        {
            Users = new List<UserInfo>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}