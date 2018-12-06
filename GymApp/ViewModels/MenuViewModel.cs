
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace GymApp.ViewModels
{
    public class MenuViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MvxCommand NewWorkoutOpenCommand { get; private set; }

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            NewWorkoutOpenCommand = new MvxCommand(() => { _navigationService.Navigate<NewWorkoutViewModel>(); });
        }
    }
}

