using System;

namespace GymApp.Model
{
    public class Workout
    {
        public string ID { get; set; }
        public DateTime StartedTime { get; set; }
        public bool Completed { get; set; }
        public DateTime CompletedTime { get; set; }
    }
}
