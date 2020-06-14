using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using FitnessTrackerAnalyzer.Model;

namespace FitnessTrackerAnalyzer.ViewModel
{
    public class UserInfoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<UserTrainingInfo> _users = new List<UserTrainingInfo>();
        private UserTrainingInfo _selectedUserTraining;
        private string _userName;
        private IEnumerable<Point> _trainingResultPoint = new List<Point>();
        private IEnumerable<Point> _maxStepPoints = new List<Point>();
        private IEnumerable<Point> _averageSteps = new List<Point>();
        private IEnumerable<Point> _minStepsPoints = new List<Point>();

        public List<UserTrainingInfo> Users
        {
            get => _users;
            set
            {
                _users = value;
                NotifyPropertyChanged();
            }
        }
        
        public UserTrainingInfo SelectedUserTrainingInfo
        {
            get => _selectedUserTraining;
            set
            {
                _selectedUserTraining = value;
                SetSelectedUserTrainingInfo();
            }
        }
        
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                NotifyPropertyChanged();
            }
        }
        
        public IEnumerable<Point> TrainingResultPoint
        {
            get => _trainingResultPoint;
            set
            {
                _trainingResultPoint = value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<Point> AverageSteps
        {
            get => _averageSteps;
            set
            {
                _averageSteps = value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<Point> MaxStepsPoints
        {
            get => _maxStepPoints;
            set
            {
                _maxStepPoints = value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<Point> MinStepsPoints
        {
            get => _minStepsPoints;
            set
            {
                _minStepsPoints = value;
                NotifyPropertyChanged();
            }
        }
        
        public void NotifyPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void SetSelectedUserTrainingInfo()
        {
            if (_selectedUserTraining == null) return;

            TrainingResultPoint =  _selectedUserTraining.Trainings
                .Select(training => new Point(training.Number, training.Steps));

            var maxDayNumber = _selectedUserTraining.Trainings.Max(item => item.Number);
            var minDayNumber = _selectedUserTraining.Trainings.Min(item => item.Number);

            AverageSteps = new List<Point>
            {
                new Point {X = minDayNumber, Y = _selectedUserTraining.AverageSteps},
                new Point {X = maxDayNumber, Y = _selectedUserTraining.AverageSteps}
            };

            var daysWithMaxSteps = _selectedUserTraining.Trainings
                .Where(training => 
                    training.Steps == _selectedUserTraining.Trainings.Max(y => y.Steps));
            var daysWithMinSteps = _selectedUserTraining.Trainings
                .Where(training =>
                    training.Steps == _selectedUserTraining.Trainings.Min(y => y.Steps));

            MinStepsPoints = daysWithMinSteps
                .Select(training => new Point { X = training.Number, Y = training.Steps });
            MaxStepsPoints = daysWithMaxSteps
                .Select(training => new Point { X = training.Number, Y = training.Steps });

            UserName = _selectedUserTraining.Name;
        }
    }
}