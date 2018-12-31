using System.Collections.Generic;
using System.Threading.Tasks;
using GymApp.Model;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace GymApp.ViewModels
{
    public class WorkoutHistoryViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public WorkoutHistoryViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        MvxObservableCollection<Workout> _workouts;
        public MvxObservableCollection<Workout> Workouts
        {
            get
            {
                return _workouts;
            }
            set
            {
                _workouts = value;
                RaisePropertyChanged("Workouts");
            }
        }

        private Workout _selectedWorkout { get; set; }
        public Workout SelectedWorkout
        {
            get
            {
                return _selectedWorkout;
            }
            set
            {
                _selectedWorkout = value;
                OpenOldWorkout();
                RaisePropertyChanged("SelectedWorkout");
            }
        }

        private async Task OpenOldWorkout()
        {
            await _navigationService.Navigate<NewWorkoutViewModel, Workout>(SelectedWorkout);
        }

        public async override void ViewAppearing()
        {
            base.ViewAppearing();
            List<Workout> excersizesList = await App.WorkoutDatabase.GetItemsAsync();
            Workouts = new MvxObservableCollection<Workout>(excersizesList);
        }
    }
}
