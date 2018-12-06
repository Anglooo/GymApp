using System;
using System.IO;
using System.Threading.Tasks;
using GymApp.Repositories;
using GymApp.ViewModels;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace GymApp
{
    public class App : MvxApplication
    {
        /// <summary>
        /// Breaking change in v6: This method is called on a background thread. Use
        /// Startup for any UI bound actions
        /// </summary>
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            
            RegisterAppStart<RootViewModel>();
        }

        /// <summary>
        /// Do any UI bound startup actions here
        /// </summary>
        public override Task Startup()
        {
            return base.Startup();
        }

        /// <summary>
        /// If the application is restarted (eg primary activity on Android 
        /// can be restarted) this method will be called before Startup
        /// is called again
        /// </summary>
        public override void Reset()
        {
            base.Reset();
        }

        static WorkoutDatabase workoutDatabase;

        public static WorkoutDatabase WorkoutDatabase
        {
            get
            {
                if (workoutDatabase == null)
                {
                    workoutDatabase = new WorkoutDatabase(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WorkoutSQLite.db3"));
                }
                return workoutDatabase;
            }
        }
    }
}