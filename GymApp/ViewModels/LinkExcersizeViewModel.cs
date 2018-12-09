using System.Collections.Generic;
using GymApp.Model;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace GymApp
{
    public class LinkExcersizeViewModel : MvxViewModel<WorkoutTemplate>
    {
        private readonly IMvxNavigationService _navigationService;

        public WorkoutTemplate ThisTemplate { get; set; }

        public MvxCommand SaveCommand { get; set; }

        public LinkExcersizeViewModel(IMvxNavigationService navigationService)
        {
            //TODO: Write View
            _navigationService = navigationService;
            SaveCommand = new MvxCommand(() => 
            {
                foreach (var item in Excersizes)
                {
                    List<string> links = new List<string>();

                    if(item.IsSelected)
                    {
                        links.Add(ThisTemplate.ID);
                    }
                    item.Excersize.TemplateID = links;
                    App.ExcersizeDatabase.SaveItemAsync(item.Excersize);
                }
                _navigationService.Close(this);
            });
        }

        public override void Prepare(WorkoutTemplate parameter)
        {
            ThisTemplate = parameter;
        }

        public async override void ViewAppearing()
        {
            base.ViewAppearing();
            List<Excersize> excersizesList = await App.ExcersizeDatabase.GetItemsAsync();
            List<ExcersizeWrapper> wrappedList = new List<ExcersizeWrapper>();

            foreach (var item in excersizesList)
            {
                ExcersizeWrapper itemToAdd = new ExcersizeWrapper(item);
                if (item.TemplateID != null)
                {
                    if (item.TemplateID.Contains(ThisTemplate.ID))
                    {
                        itemToAdd.IsSelected = true;
                    }
                }
                wrappedList.Add(itemToAdd);
            }
            Excersizes = new MvxObservableCollection<ExcersizeWrapper>(wrappedList);
        }

        MvxObservableCollection<ExcersizeWrapper> _excersizes;
        public MvxObservableCollection<ExcersizeWrapper> Excersizes
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
    }

    public class ExcersizeWrapper 
    {
        public ExcersizeWrapper(Excersize _excersize)
        {
            Excersize = _excersize;
        }
        public Excersize Excersize { get; set; }
        public bool IsSelected { get; set; }
    }
}