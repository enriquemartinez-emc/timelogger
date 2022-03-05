using System;
using System.Collections.Generic;

namespace Timelogger.Entities
{
	public class Project : Entity<int>
    {
        private Project()
        {
        }

        public Project(string name, DateTime deadLine)
        {
            Name = name;
            Deadline = deadLine;
            IsCompleted = false;
        }

        public string Name { get; private set; }
        public DateTime Deadline { get; private set; }
        public bool IsCompleted { get; private set; }


        private readonly List<TimeLog> _timeLogs = new List<TimeLog>();

        public IEnumerable<TimeLog> TimeLogs => _timeLogs;

        public TimeLog AddTimeLog(string description, int duration)
        {
            // Method that could contain more complex business logic, candidate for Unit Test
            var timeLog = new TimeLog(this, description, duration);
            _timeLogs.Add(timeLog);

            return timeLog;
        }

        public void SetCompletedStatus()
        {
            IsCompleted = true;
        }
    }
}
