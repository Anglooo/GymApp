using System;
using GymApp.Model;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace GymApp.ViewModels
{
    public class NewExcersizeViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public MvxCommand SaveExcersizeCommand { get; private set; }


        public NewExcersizeViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            RepNumbers = new MvxObservableCollection<string>();
            for (int i = 1; i <= 20; i++)
            {
                RepNumbers.Add(i.ToString());
            }

            SaveExcersizeCommand = new MvxCommand(async () => 
            {
                if(string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(Machine) && string.IsNullOrWhiteSpace(RepRangeLow) && string.IsNullOrWhiteSpace(RepRangeHigh) && string.IsNullOrWhiteSpace(Sets))
                {
                    await Application.Current.MainPage.DisplayAlert("Insufficient Data", "Insufficient Data", "OK");
                    return;
                }

                int repLowInt;
                bool parsedLow = int.TryParse(RepRangeLow, out repLowInt);

                int repHighInt;
                bool parsedHigh = int.TryParse(RepRangeHigh, out repHighInt);

                if(parsedHigh && parsedLow)
                {
                    if(repLowInt > repHighInt)
                    {
                        await Application.Current.MainPage.DisplayAlert("Rep Range Problem", "Low rep range is higher than high rep range.", "OK");
                        return;
                    }
                }

                Excersize excersize = new Excersize();
                excersize.ID = Guid.NewGuid().ToString();
                excersize.Name = Name;
                excersize.Description = Description;
                excersize.Machine = Machine;
                excersize.RepRange = RepRangeLow + "-" + RepRangeHigh;
                excersize.Sets = Sets;

                await App.ExcersizeDatabase.SaveItemAsync(excersize);
                await _navigationService.Close(this);
            });
        }

        private string _name { get; set; }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        private string _description { get; set; }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        private string _machine { get; set; }
        public string Machine
        {
            get
            {
                return _machine;
            }
            set
            {
                _machine = value;
                RaisePropertyChanged("Machine");
            }
        }

        private string _repRangeLow { get; set; }
        public string RepRangeLow
        {
            get
            {
                return _repRangeLow;
            }
            set
            {
                _repRangeLow = value;
                RaisePropertyChanged("RepRangeLow");
            }
        }

        private string _repRangeHigh { get; set; }
        public string RepRangeHigh
        {
            get
            {
                return _repRangeHigh;
            }
            set
            {
                _repRangeHigh = value;
                RaisePropertyChanged("RepRangeHigh");
            }
        }

        private string _sets { get; set; }
        public string Sets
        {
            get
            {
                return _sets;
            }
            set
            {
                _sets = value;
                RaisePropertyChanged("Sets");
            }
        }

        public MvxObservableCollection<string> RepNumbers { get; private set; }
    }
}