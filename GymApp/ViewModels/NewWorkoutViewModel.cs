using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymApp.Model;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace GymApp.ViewModels
{
    public class NewWorkoutViewModel : MvxViewModel<Workout>
    {
        private readonly IMvxNavigationService _navigationService;

        private bool EdittingWorkout;
        private bool ShownTemplateAlert;

        public MvxCommand SaveCommand { get; private set; }
        public MvxCommand NewExcersizeCommand { get; private set; }


        public NewWorkoutViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            SaveCommand = new MvxCommand(async() => { await SaveWorkout(); });
            NewExcersizeCommand = new MvxCommand(async () => { await CreateNewExcersize(); });

        }

        private MvxObservableCollection<WorkoutTemplate> _templates { get; set; }
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

        private WorkoutTemplate _selectedTemplate { get; set; }
        public WorkoutTemplate SelectedTemplate
        {
            get
            {
                return _selectedTemplate;
            }

            set
            {
                _selectedTemplate = value;
                RaisePropertyChanged("SelectedTemplate");

                if (value != null)
                {
                    LoadTemplatedExcersizes();
                }
            }
        }

        private MvxObservableCollection<ExcersizeLogWrapper> _excersizes { get; set; }
        public MvxObservableCollection<ExcersizeLogWrapper> Excersizes
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

        private ExcersizeLogWrapper _selectedExcersize { get; set; }
        public ExcersizeLogWrapper SelectedExcersize
        {
            get
            {
                return _selectedExcersize;
            }
            set
            {
                _selectedExcersize = value;
                RaisePropertyChanged("SelectedExcersize");
                LogSelectedExcersize(_selectedExcersize);
            }
        }


        private Workout _currentWorkout { get; set; }
        public Workout CurrentWorkout
        {
            get
            {
                return _currentWorkout;
            }
            set
            {
                _currentWorkout = value;
                RaisePropertyChanged("CurrentWorkout");
            }
        }

        public async override void ViewAppearing()
        {
            base.ViewAppearing();
            List<WorkoutTemplate> templateList = await App.WorkoutTemplateDatabase.GetItemsAsync();
            Templates = new MvxObservableCollection<WorkoutTemplate>(templateList);
            if(CurrentWorkout?.TemplateID != null)
            {
                SelectedTemplate = Templates.Where(x => x.ID == CurrentWorkout.TemplateID).FirstOrDefault();
            }
        }

        private async Task LoadTemplatedExcersizes()
        {
            List<Excersize> excersizesList = await App.ExcersizeDatabase.GetItemsAsync();
            List<ExcersizeLogWrapper> templatedExcersizes = new List<ExcersizeLogWrapper>();
            CurrentWorkout.TemplateID = SelectedTemplate.ID;

            foreach (var item in excersizesList)
            {
                if (item.TemplateID != null)
                {
                    if (item.TemplateID.Contains(SelectedTemplate.ID))
                    {
                        templatedExcersizes.Add(new ExcersizeLogWrapper(item));
                    }
                }
            }

            if(Excersizes == null|| Excersizes.Count() == 0)
            {
                Excersizes = new MvxObservableCollection<ExcersizeLogWrapper>(templatedExcersizes);
            }
            else
            {
                foreach (var item in templatedExcersizes)
                {
                    bool exists = false;
                    foreach (var ex in Excersizes)
                    {
                        if(item.ExcersizeTemplate.ID == ex.ExcersizeTemplate.ID)
                        {
                            exists = true;
                        }
                    }

                    if(!exists)
                    {
                        Excersizes.Add(item);
                    }
                }
            }
        }

        private async Task LogSelectedExcersize(ExcersizeLogWrapper selectedExcersize)
        {
            var x = await _navigationService.Navigate<LogExcersizeViewModel, ExcersizeLogWrapper, ExcersizeLogWrapper>(selectedExcersize);
            var y = x.LoggedExcersizeTemp;

            for (int i = 0; i < Excersizes.Count; i++)
            {
                if(Excersizes[i] == selectedExcersize)
                {
                    Excersizes[i] = x;
                    await RaisePropertyChanged("Excersizes");
                    break;
                }
            }
        }

        private async Task CreateNewExcersize()
        {
            string newExcersize = "New Excersize";
            string chooseExcersize = "Select Excersize";

            var x = await Application.Current.MainPage.DisplayActionSheet("Add Excersize", "cancel", null, newExcersize, chooseExcersize);

            if (x == newExcersize)
            {
                Excersize excersize = await _navigationService.Navigate<NewExcersizeViewModel, string, Excersize>(SelectedTemplate?.ID);
                await App.ExcersizeDatabase.SaveItemAsync(excersize);
                if (Excersizes == null)
                {
                    Excersizes = new MvxObservableCollection<ExcersizeLogWrapper>();
                }
                Excersizes.Add(new ExcersizeLogWrapper(excersize));
                await RaisePropertyChanged("Excersizes");
            }
            else if(x == chooseExcersize)
            {
                return;
            }
        }

        public override void Start()
        {
            base.Start();

            if (CurrentWorkout == null)
            {
                CurrentWorkout = new Workout();
                CurrentWorkout.StartedTime = DateTime.Now;
                CurrentWorkout.Completed = false;
                CurrentWorkout.ID = Guid.NewGuid().ToString();
            }
        }

        public async Task Initialize()
        {
            await base.Initialize();


        }

        public async Task SaveWorkout(bool quietSave = false)
        {
            CurrentWorkout.LoggedExcersizes = Excersizes.ToList().Where(x => x.LogComplete == true).ToList();

            if (!CurrentWorkout.Completed && !quietSave)
            {
                if (CurrentWorkout.LoggedExcersizes.Count == 0)
                {
                    bool decision = await Application.Current.MainPage.DisplayAlert("No Excersizes completed. Continue?", "", "Yes", "No");
                    if (!decision)
                    {
                        return;
                    }
                }

                bool decision2 = await Application.Current.MainPage.DisplayAlert("Is this workout Complete?", "", "Yes","No");
                if (decision2)
                {
                    CurrentWorkout.Completed = true;
                    CurrentWorkout.CompletedTime = DateTime.Now;
                }
            }

            await App.WorkoutDatabase.SaveItemAsync(CurrentWorkout);
            if (!quietSave)
            {
                await _navigationService.Navigate<HomeViewModel>();
            }
        }

        public override void Prepare(Workout parameter)
        {
            if(parameter != null)
            {
                ShownTemplateAlert = true;
                EdittingWorkout = true;
                Excersizes = new MvxObservableCollection<ExcersizeLogWrapper>(parameter.LoggedExcersizes);
                CurrentWorkout = parameter;
            }
        }
    }

    public class ExcersizeLogWrapper
    {
        public ExcersizeLogWrapper(Excersize excersize)
        {
            ExcersizeTemplate = excersize;
            LoggedExcersizeTemp = new LoggedExcersize();
        }

        public bool LogComplete { get; set; }
        public Excersize ExcersizeTemplate { get; set; }
        public LoggedExcersize LoggedExcersizeTemp { get; set; }
    }
}