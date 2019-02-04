using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace GymApp.Model
{
    public class Excersize
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Machine { get; set; }
        public string Description { get; set; }
        public string RepRange { get; set;}
        public string Sets { get; set; }
        public string LastWorkoutOverview { get; set; }
        public string TemplateIDJson { get; set; }

        [Ignore]
        public List<string> TemplateID
        {
            get
            {
                if (TemplateIDJson == null)
                {
                    return new List<string>();
                }
                return JsonConvert.DeserializeObject<List<string>>(TemplateIDJson);
            }
            set
            {
                TemplateIDJson = JsonConvert.SerializeObject(value);
            }
        }
    }
}