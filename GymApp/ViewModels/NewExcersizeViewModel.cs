using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace GymApp.ViewModels
{
    public class NewExcersizeViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;


        public NewExcersizeViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}