using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace GymApp.ViewModels
{
    public class ExcersizeHistoryViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MvxCommand NewExcersizeCommand { get; private set; }

        public ExcersizeHistoryViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            NewExcersizeCommand = new MvxCommand(() => { _navigationService.Navigate<NewExcersizeViewModel>(); });
        }
    }
}