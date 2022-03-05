using Newtonsoft.Json;

namespace Timelogger.Entities
{
    public class TimeLog : Entity<int>
    {
        private TimeLog()
        {
        }

        public TimeLog(Project project, string description, int duration)
        {
            Project = project;
            Description = description;
            Duration = duration;
        }

        public string Description { get; private set; }
        public int Duration { get; private set; }


        [JsonIgnore]
        public Project Project { get; set; }
    }
}
