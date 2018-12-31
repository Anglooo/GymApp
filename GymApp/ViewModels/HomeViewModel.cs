using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GymApp.Model;
using MvvmCross.Navigation;
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

        public override void Start()
        {
            base.Start();
            Task.Run(async () => { await FirstInit(); });
        }

        public async Task FirstInit()
        {

            List<Excersize> x = await App.ExcersizeDatabase.GetItemsAsync();
            List<WorkoutTemplate> y = await App.WorkoutTemplateDatabase.GetItemsAsync();
            if(x.Count == 0 && y.Count == 0)
            {
                WorkoutTemplate pushTemplate = new WorkoutTemplate();
                pushTemplate.ID = "pushTemplateID";
                pushTemplate.Name = "Push";
                await App.WorkoutTemplateDatabase.SaveItemAsync(pushTemplate);

                WorkoutTemplate pullTemplate = new WorkoutTemplate();
                pullTemplate.ID = "pullTemplateID";
                pullTemplate.Name = "Pull";
                await App.WorkoutTemplateDatabase.SaveItemAsync(pullTemplate);

                WorkoutTemplate legsTemplate = new WorkoutTemplate();
                legsTemplate.ID = "legsTemplateID";
                legsTemplate.Name = "Legs";
                await App.WorkoutTemplateDatabase.SaveItemAsync(legsTemplate);

                Excersize bpress = new Excersize();
                bpress.ID = Guid.NewGuid().ToString();
                bpress.Name = "Bench Press";
                bpress.Machine = "Bench";
                bpress.Sets = "3";
                bpress.RepRange = "8-12";
                bpress.TemplateID = new List<string>();
                bpress.TemplateID.Add(pushTemplate.ID);
                await App.ExcersizeDatabase.SaveItemAsync(bpress);

                Excersize shoulderpress = new Excersize();
                shoulderpress.ID = Guid.NewGuid().ToString();
                shoulderpress.Name = "Shoulder press";
                shoulderpress.Machine = "Squat Rack";
                shoulderpress.Sets = "3";
                shoulderpress.RepRange = "8-12";
                shoulderpress.TemplateID = new List<string>();
                shoulderpress.TemplateID.Add(pushTemplate.ID);
                await App.ExcersizeDatabase.SaveItemAsync(shoulderpress);

                Excersize squat = new Excersize();
                squat.ID = Guid.NewGuid().ToString();
                squat.Name = "Squat";
                squat.Machine = "Squat Rack";
                squat.Sets = "3";
                squat.RepRange = "8-12";
                squat.TemplateID = new List<string>();
                squat.TemplateID.Add(legsTemplate.ID);
                await App.ExcersizeDatabase.SaveItemAsync(squat);

                Excersize deadLift = new Excersize();
                deadLift.ID = Guid.NewGuid().ToString();
                deadLift.Name = "Deadlift";
                deadLift.Machine = "Straight Bar";
                deadLift.Sets = "5";
                deadLift.RepRange = "3-5";
                deadLift.TemplateID = new List<string>();
                deadLift.TemplateID.Add(pullTemplate.ID);
                await App.ExcersizeDatabase.SaveItemAsync(deadLift);

            }

        }
    }
}