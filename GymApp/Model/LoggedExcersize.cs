using System.Collections.Generic;
using GymApp.ViewModels;

namespace GymApp.Model
{
    public class LoggedExcersize
    {
        public string ExcersizeID { get; set; }
        public string ExcersizeName { get; set; }
        public List<LoggingSet> Sets { get; set; }
        public string SetOverview 
        {
            get
            {
                string output = "";
                if(Sets != null)
                {
                    foreach (var item in Sets)
                    {
                        output = output + item.Reps + "x" + item.Weight + " " + item.Denom;
                        if(Sets[Sets.Count-1] != item)
                        {
                            output = output + ",";
                        }
                    }
                }
                return output;
            }
        }
    }

    public class Set
    {
        public int Reps { get; set; }
        public string Weight { get; set; }
    }
}