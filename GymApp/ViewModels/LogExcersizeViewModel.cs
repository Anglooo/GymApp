using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymApp.Model;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace GymApp.ViewModels
{
    public class LogExcersizeViewModel : MvxViewModel<ExcersizeLogWrapper, ExcersizeLogWrapper>
    {
        private readonly IMvxNavigationService _navigationService;

        public MvxCommand SaveCommand { get; private set; }
        public MvxCommand AddSetCommand { get; private set; }


        public LogExcersizeViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            SaveCommand = new MvxCommand(async() => { await SaveLog(); });
            AddSetCommand = new MvxCommand(async () => { AddSet(); });
        }

        public Excersize CurrentTemplate { get; set; }

        private ExcersizeLogWrapper excersizeLogWrapper { get; set; }

        private LoggedExcersize _currentLoggedExcersize { get; set; }
        public LoggedExcersize CurrentLoggedExcersize
        {
            get
            {
                return _currentLoggedExcersize;
            }
            set
            {
                _currentLoggedExcersize = value;
                RaisePropertyChanged("CurrentLoggedExcersize");
            }
        }

        public override void Prepare(ExcersizeLogWrapper parameter)
        {
            excersizeLogWrapper = parameter;
            if(parameter.LoggedExcersizeTemp.ExcersizeID == null)
            {
                CurrentLoggedExcersize = new LoggedExcersize();
            }

            CurrentLoggedExcersize = parameter.LoggedExcersizeTemp;

            CurrentLoggedExcersize.ExcersizeID = parameter.ExcersizeTemplate.ID;
            CurrentLoggedExcersize.ExcersizeName = parameter.ExcersizeTemplate.Name;

            if(parameter.LogComplete)
            {
                CurrentLoggedExcersize.Sets = parameter.LoggedExcersizeTemp.Sets;
                LoggingSets = new MvxObservableCollection<LoggingSet>(parameter.LoggedExcersizeTemp.Sets);
                CurrentTemplate = parameter.ExcersizeTemplate;
            }
            else
            {
                CurrentLoggedExcersize.Sets = new List<LoggingSet>();

                CurrentTemplate = parameter.ExcersizeTemplate;

                int setsToLog = int.Parse(parameter.ExcersizeTemplate.Sets);
                LoggingSets = new MvxObservableCollection<LoggingSet>();

                for (int i = 0; i < setsToLog; i++)
                {
                    LoggingSet toAdd = new LoggingSet();
                    LoggingSets.Add(toAdd);
                }
            }
        }

        private MvxObservableCollection<LoggingSet> _loggingSets { get; set; }
        public MvxObservableCollection<LoggingSet> LoggingSets
        {
            get
            {
                return _loggingSets;
            }
            set
            {
                _loggingSets = value;
                RaisePropertyChanged("LoggingSets");
            }
        }

        private void AddSet()
        {
            LoggingSets.Add(new LoggingSet());
            RaisePropertyChanged("LoggingSets");
        }

        private async Task SaveLog()
        {
            bool complete = true;
            foreach (var item in LoggingSets)
            {
                if(item.Weight == 0 || item.Reps == 0 || item.Denom == null)
                {
                    complete = false;
                    break;
                }
            }

            if(!complete)
            {
                await Application.Current.MainPage.DisplayAlert("Please complete all sets", "", "Ok");
                return;
            }

            CurrentLoggedExcersize.Sets = LoggingSets.ToList();

            excersizeLogWrapper.LoggedExcersizeTemp = CurrentLoggedExcersize;
            excersizeLogWrapper.LogComplete = true;

            CurrentTemplate.LastWorkoutOverview = CurrentLoggedExcersize.SetOverview + " On " + DateTime.Now.ToShortDateString();
            await App.ExcersizeDatabase.SaveItemAsync(CurrentTemplate);

            await _navigationService.Close<ExcersizeLogWrapper>(this, excersizeLogWrapper);
        }
    }

    public class LoggingSet
    {
        public int Reps { get; set; }
        public int Weight { get; set; }
        public string Denom { get; set; }
    }
}
