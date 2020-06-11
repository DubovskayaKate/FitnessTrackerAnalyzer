using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
//using Caliburn.Micro;
using FitnessTrackerAnalyzer.Model;

namespace FitnessTrackerAnalyzer.ViewModel
{
    public class UserInfoViewModel 
    {
        public List<UserInfo> Users { get; set; }

        public UserInfoViewModel()
        {
            Users = new List<UserInfo>{new UserInfo{Name = "kate"}};
        }
    }
}