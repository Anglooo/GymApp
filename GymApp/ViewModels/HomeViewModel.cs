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
                var pushtemplate = new List<string>();
                pushtemplate.Add(pushTemplate.ID);
                bpress.TemplateID = pushtemplate;
                await App.ExcersizeDatabase.SaveItemAsync(bpress);

                Excersize bpress2 = new Excersize();
                bpress2.ID = Guid.NewGuid().ToString();
                bpress2.Name = "DB Bench Press";
                bpress2.Machine = "Bench";
                bpress2.Sets = "3";
                bpress2.RepRange = "8-12";
                bpress2.TemplateID = pushtemplate;
                await App.ExcersizeDatabase.SaveItemAsync(bpress2);

                Excersize tcpush = new Excersize();
                tcpush.ID = Guid.NewGuid().ToString();
                tcpush.Name = "V Bar Push Down";
                tcpush.Machine = "Cable Machine";
                tcpush.Sets = "3";
                tcpush.RepRange = "8-12";
                tcpush.TemplateID = pushtemplate;
                await App.ExcersizeDatabase.SaveItemAsync(tcpush);

                Excersize latRaise = new Excersize();
                latRaise.ID = Guid.NewGuid().ToString();
                latRaise.Name = "Lat Raise";
                latRaise.Machine = "Dumb Bells";
                latRaise.Sets = "3";
                latRaise.RepRange = "8-12";
                latRaise.TemplateID = pushtemplate;
                await App.ExcersizeDatabase.SaveItemAsync(latRaise);

                Excersize chestFly = new Excersize();
                chestFly.ID = Guid.NewGuid().ToString();
                chestFly.Name = "Chest Fly";
                chestFly.Machine = "Fly Machine";
                chestFly.Sets = "3";
                chestFly.RepRange = "8-12";
                chestFly.TemplateID = pushtemplate;
                await App.ExcersizeDatabase.SaveItemAsync(chestFly);

                Excersize shoulderpress2 = new Excersize();
                shoulderpress2.ID = Guid.NewGuid().ToString();
                shoulderpress2.Name = "DB Shoulder press";
                shoulderpress2.Machine = "Bench";
                shoulderpress2.Sets = "3";
                shoulderpress2.RepRange = "8-12";
                shoulderpress2.TemplateID = pushtemplate;
                await App.ExcersizeDatabase.SaveItemAsync(shoulderpress2);

                Excersize shoulderpress = new Excersize();
                shoulderpress.ID = Guid.NewGuid().ToString();
                shoulderpress.Name = "Shoulder press";
                shoulderpress.Machine = "Squat Rack";
                shoulderpress.Sets = "3";
                shoulderpress.RepRange = "8-12";
                shoulderpress.TemplateID = pushtemplate;
                await App.ExcersizeDatabase.SaveItemAsync(shoulderpress);

                Excersize tcpushbehind = new Excersize();
                tcpushbehind.ID = Guid.NewGuid().ToString();
                tcpushbehind.Name = "Tricep Push Behind";
                tcpushbehind.Machine = "Cable Machine/Rope";
                tcpushbehind.Sets = "3";
                tcpushbehind.RepRange = "8-12";
                tcpushbehind.TemplateID = pushtemplate;
                await App.ExcersizeDatabase.SaveItemAsync(tcpushbehind);

                Excersize squat = new Excersize();
                squat.ID = Guid.NewGuid().ToString();
                squat.Name = "Squat";
                squat.Machine = "Squat Rack";
                squat.Sets = "3";
                squat.RepRange = "8-12";
                var legstemplate = new List<string>();
                legstemplate.Add(legsTemplate.ID);
                squat.TemplateID = legstemplate;
                await App.ExcersizeDatabase.SaveItemAsync(squat);

                Excersize deadLift = new Excersize();
                deadLift.ID = Guid.NewGuid().ToString();
                deadLift.Name = "Deadlift";
                deadLift.Machine = "Straight Bar";
                deadLift.Sets = "5";
                deadLift.RepRange = "3-5";
                var pulltemplate = new List<string>();
                pulltemplate.Add(pullTemplate.ID);
                deadLift.TemplateID = pulltemplate;
                await App.ExcersizeDatabase.SaveItemAsync(deadLift);

                Excersize latpulldown = new Excersize();
                latpulldown.ID = Guid.NewGuid().ToString();
                latpulldown.Name = "Lat Pulldown";
                latpulldown.Machine = "Pulldown Cable Machine";
                latpulldown.Sets = "3";
                latpulldown.RepRange = "8-12";
                latpulldown.TemplateID = pulltemplate;
                await App.ExcersizeDatabase.SaveItemAsync(latpulldown);

                Excersize seatedrow = new Excersize();
                seatedrow.ID = Guid.NewGuid().ToString();
                seatedrow.Name = "Seated Row";
                seatedrow.Machine = "Seated Row Cable";
                seatedrow.Sets = "3";
                seatedrow.RepRange = "8-12";
                seatedrow.TemplateID = pulltemplate;
                await App.ExcersizeDatabase.SaveItemAsync(seatedrow);

                Excersize HammerCurl = new Excersize();
                HammerCurl.ID = Guid.NewGuid().ToString();
                HammerCurl.Name = "Rope Hammer Curl";
                HammerCurl.Machine = "Cable Machine/Rope";
                HammerCurl.Sets = "3";
                HammerCurl.RepRange = "8-12";
                HammerCurl.TemplateID = pulltemplate;
                await App.ExcersizeDatabase.SaveItemAsync(HammerCurl);

                Excersize FacePull = new Excersize();
                FacePull.ID = Guid.NewGuid().ToString();
                FacePull.Name = "Face Pull";
                FacePull.Machine = "Cable Machine/Straight Bar";
                FacePull.Sets = "3";
                FacePull.RepRange = "8-12";
                FacePull.TemplateID = pulltemplate;
                await App.ExcersizeDatabase.SaveItemAsync(FacePull);

                Excersize BicepCurl = new Excersize();
                BicepCurl.ID = Guid.NewGuid().ToString();
                BicepCurl.Name = "Rope Bicep Curl";
                BicepCurl.Machine = "Cable Machine/Straight Bar";
                BicepCurl.Sets = "3";
                BicepCurl.RepRange = "8-12";
                BicepCurl.TemplateID = pulltemplate;
                await App.ExcersizeDatabase.SaveItemAsync(BicepCurl);

            }

        }
    }
}