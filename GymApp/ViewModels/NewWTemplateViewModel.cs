using System;
using System.Collections.Generic;
using System.Linq;
using GymApp.Model;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace GymApp
{
    public class NewWTemplateViewModel : MvxViewModel<WorkoutTemplate>
    {
        private readonly IMvxNavigationService _navigationService;
        WorkoutTemplate thisTemplate;

        public MvxCommand LinkExcersizeCommand { get; set; }
        public MvxCommand SaveCommand { get; set; }

        public NewWTemplateViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            if(thisTemplate == null)
            {
                thisTemplate = new WorkoutTemplate();
                thisTemplate.ID = Guid.NewGuid().ToString();
            }

            LinkExcersizeCommand = new MvxCommand(async() => 
            {
                thisTemplate.Name = TemplateName;
                await _navigationService.Navigate<LinkExcersizeViewModel,WorkoutTemplate>(thisTemplate);
            });

            SaveCommand = new MvxCommand(() =>
            {
                thisTemplate.Name = TemplateName;
                App.WorkoutTemplateDatabase.SaveItemAsync(thisTemplate);
                _navigationService.Close(this);
            });
        }

        public async override void ViewAppearing()
        {
            base.ViewAppearing();
            List<Excersize> allExcersizes = await App.ExcersizeDatabase.GetItemsAsync();
            List<Excersize> templateExcersizes = allExcersizes.Where(x => x.TemplateID.Contains(thisTemplate.ID)).ToList();
            LinkedExcersizes = new MvxObservableCollection<Excersize>(templateExcersizes);
        }

        public override void Prepare(WorkoutTemplate parameter)
        {
            thisTemplate = parameter;
            TemplateName = thisTemplate.Name;
        }

        private string _templateName { get; set; }
        public string TemplateName
        {
            get
            {
                return _templateName;
            }
            set
            {
                _templateName = value;
                RaisePropertyChanged("TemplateName");
            }
        }

        private string _templateDescription { get; set; }
        public string TemplateDescription
        {
            get
            {
                return _templateDescription;
            }
            set
            {
                _templateDescription = value;
                RaisePropertyChanged("TemplateDescription");
            }
        }

        private MvxObservableCollection<Excersize> _linkedExcersizes;
        public MvxObservableCollection<Excersize> LinkedExcersizes
        {
            get
            {
                return _linkedExcersizes;
            }
            set
            {
                _linkedExcersizes = value;
                RaisePropertyChanged("LinkedExcersizes");
            }
        }
    }
}