
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace GymApp.ViewModels
{
    public class MenuViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MvxCommand NewWorkoutOpenCommand { get; private set; }
        public MvxCommand ExcersizeHistoryOpenCommand { get; private set; }
        public MvxCommand DashboardOpenCommand { get; private set; }
        public MvxCommand TemplateWorkoutOpenCommand { get; private set; }
        public MvxCommand WorkoutHistoryOpenCommand { get; private set; }


        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            NewWorkoutOpenCommand = new MvxCommand(() => { _navigationService.Navigate<NewWorkoutViewModel>(); });
            ExcersizeHistoryOpenCommand = new MvxCommand(() => { _navigationService.Navigate<ExcersizeHistoryViewModel>(); });
            DashboardOpenCommand = new MvxCommand(() => { _navigationService.Navigate<HomeViewModel>(); });
            TemplateWorkoutOpenCommand = new MvxCommand(() => { _navigationService.Navigate<TemplateViewModel>(); });
            WorkoutHistoryOpenCommand = new MvxCommand(() => { _navigationService.Navigate<WorkoutHistoryViewModel>(); });
        }
    }
}

