using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        private UserInfo _selectedUser;
        public UserInfo SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                TrainingResultPoint =
                    _selectedUser.Trainings.Select(training => new Point(training.Number, training.Steps));

                var maxDayNumber = _selectedUser.Trainings.Max(item => item.Number);
                var minDayNumber = _selectedUser.Trainings.Min(item => item.Number);
                AverageSteps = new List<Point>{new Point{X = minDayNumber, Y = _selectedUser.AverageSteps}, 
                    new Point{X=maxDayNumber, Y = _selectedUser.AverageSteps}};

                var daysWithMaxSteps = _selectedUser.Trainings.Where(training => training.Steps == _selectedUser.Trainings.Max(y => y.Steps));
                var daysWithMinSteps = _selectedUser.Trainings.Where(training => training.Steps == _selectedUser.Trainings.Min(y => y.Steps));

                MinStepsPoints =
                    daysWithMinSteps.Select(training => new Point {X = training.Number, Y = training.Steps});
                MaxStepsPoints =
                    daysWithMaxSteps.Select(training => new Point {X = training.Number, Y = training.Steps});
                UserName = _selectedUser.Name;
            }
        }

        private string _userName = "User Name";
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                NotifyPropertyChanged();
            }
        }

        private IEnumerable<Point> _trainingResultPoint = new List<Point>();

        public IEnumerable<Point> TrainingResultPoint
        {
            get => _trainingResultPoint;
            set
            {
                _trainingResultPoint = value;
                NotifyPropertyChanged();
            }
        }

        private IEnumerable<Point> _averageSteps = new List<Point>();
        public IEnumerable<Point> AverageSteps
        {
            get => _averageSteps;
            set
            {
                _averageSteps = value;
                NotifyPropertyChanged();
            }
        }

        private IEnumerable<Point> _maxStepPoints = new List<Point> ();
        public IEnumerable<Point> MaxStepsPoints
        {
            get => _maxStepPoints;
            set
            {
                _maxStepPoints = value;
                NotifyPropertyChanged();
            }
        }

        private IEnumerable<Point> _minStepsPoints = new List<Point>();
        public IEnumerable<Point> MinStepsPoints
        {
            get => _minStepsPoints;
            set
            {
                _minStepsPoints = value;
                NotifyPropertyChanged();
            }
        }
        
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