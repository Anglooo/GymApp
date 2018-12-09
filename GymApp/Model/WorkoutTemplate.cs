using SQLite;

namespace GymApp.Model
{
    public class WorkoutTemplate
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string Name { get; set; }
    }
}

