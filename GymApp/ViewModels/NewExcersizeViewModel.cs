using System;
using System.Collections.Generic;
using GymApp.Model;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace GymApp.ViewModels
{
    public class NewExcersizeViewModel : MvxViewModel<string, Excersize>
    {
        private readonly IMvxNavigationService _navigationService;
        public MvxCommand SaveExcersizeCommand { get; private set; }

        private string templateID { get; set; }

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
                if (string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(Machine) && string.IsNullOrWhiteSpace(RepRangeLow) && string.IsNullOrWhiteSpace(RepRangeHigh) && string.IsNullOrWhiteSpace(Sets))
                {
                    await Application.Current.MainPage.DisplayAlert("Insufficient Data", "Insufficient Data", "OK");
                    return;
                }

                int repLowInt;
                bool parsedLow = int.TryParse(RepRangeLow, out repLowInt);

                int repHighInt;
                bool parsedHigh = int.TryParse(RepRangeHigh, out repHighInt);

                if (parsedHigh && parsedLow)
                {
                    if (repLowInt > repHighInt)
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

                if (ReturnData)
                {
                    if (AddToTemplate)
                    {
                        var templateIDs = new List<string>();
                        templateIDs.Add(PassedTemplate.ID);
                        excersize.TemplateID = templateIDs;
                        await App.ExcersizeDatabase.SaveItemAsync(excersize);
                    }
                    
                    await _navigationService.Close(this, excersize);
                }
                else
                {
                    await _navigationService.Close(this);
                }

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

        private bool _addToTemplate { get; set; }
        public bool AddToTemplate
        {
            get
            {
                return _addToTemplate;
            }
            set
            {
                _addToTemplate = value;
                RaisePropertyChanged("AddToTemplate");
            }
        }

        private bool _returnData { get; set; }
        public bool ReturnData
        {
            get
            {
                return _returnData;
            }
            set
            {
                _returnData = value;
                RaisePropertyChanged("ReturnData");
            }
        }

        private WorkoutTemplate _passedTemplate;
        public WorkoutTemplate PassedTemplate
        {
            get
            {
                return _passedTemplate;
            }
            set
            {
                _passedTemplate = value;
                RaisePropertyChanged("PassedTemplate");
            }
        }

        public MvxObservableCollection<string> RepNumbers { get; private set; }

        public async override void Prepare(string parameter)
        {
            if(!string.IsNullOrEmpty(parameter))
            {
                ReturnData = true;
                templateID = parameter;
                PassedTemplate = await App.WorkoutTemplateDatabase.GetItemAsync(templateID);
            }
            
        }
    }
}