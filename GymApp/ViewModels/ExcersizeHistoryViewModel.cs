using System;
using System.Collections.Generic;
using GymApp.Model;
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

        MvxObservableCollection<Excersize> _excersizes;
        public MvxObservableCollection<Excersize> Excersizes
        {
            get
            {
                return _excersizes;
            }
            set
            {
                _excersizes = value;
                RaisePropertyChanged("Excersizes");
            }
       }


        public override void Prepare()
        {
            base.Prepare();
        }

        public async override void Start()
        {
            base.Start();

            try
            {
                List<Excersize> excersizesList = await App.ExcersizeDatabase.GetItemsAsync();
                Excersizes = new MvxObservableCollection<Excersize>(excersizesList);
            }
            catch(Exception e)
            {
                int i = 0;
            }
        }
    }
}