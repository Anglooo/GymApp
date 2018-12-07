﻿using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace GymApp.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;


        public HomeViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}