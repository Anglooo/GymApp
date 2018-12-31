using System;
using System.Collections.Generic;
using GymApp.ViewModels;
using Newtonsoft.Json;
using SQLite;

namespace GymApp.Model
{
    public class Workout
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string TemplateID { get; set; }
        public DateTime StartedTime { get; set; }
        public bool Completed { get; set; }
        public DateTime CompletedTime { get; set; }
        public string LoggedExcersizesJson { get; set; }

        [Ignore]
        public List<ExcersizeLogWrapper> LoggedExcersizes
        {
            get
            {
                if (LoggedExcersizesJson == null)
                {
                    return new List<ExcersizeLogWrapper>();
                }
                return JsonConvert.DeserializeObject<List<ExcersizeLogWrapper>>(LoggedExcersizesJson);
            }
            set
            {
                LoggedExcersizesJson = JsonConvert.SerializeObject(value);
            }
        }

    }
}
