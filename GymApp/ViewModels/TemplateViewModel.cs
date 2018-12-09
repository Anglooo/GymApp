using System.Threading.Tasks;
using GymApp.Model;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace GymApp
{
    public class TemplateViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MvxCommand ShowNewTemplateCommand { get; set; }

        public TemplateViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            ShowNewTemplateCommand = new MvxCommand(async() => 
            {
                await _navigationService.Navigate<NewWTemplateViewModel>();
            });
        }

        public async override void ViewAppearing()
        {
            base.ViewAppearing();
            Templates = new MvxObservableCollection<WorkoutTemplate>(await App.WorkoutTemplateDatabase.GetItemsAsync());
        }

        public async Task<bool> EditTemplate(WorkoutTemplate template)
        {
            await _navigationService.Navigate<NewWTemplateViewModel, WorkoutTemplate>(template);
            return true;
        }

        private MvxObservableCollection<WorkoutTemplate> _templates;
        public MvxObservableCollection<WorkoutTemplate> Templates
        {
            get
            {
                return _templates;
            }
            set
            {
                _templates = value;
                RaisePropertyChanged("Templates");
            }
        }
    }
}