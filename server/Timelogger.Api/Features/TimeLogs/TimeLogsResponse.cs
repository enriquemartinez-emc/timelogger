using System.Collections.Generic;
using System.Linq;
using Timelogger.Entities;

namespace Timelogger.Api.Features.TimeLogs
{
    public class TimeLogsResponse
    {
        public IEnumerable<TimeLog> TimeRegistrations { get; set; } = Enumerable.Empty<TimeLog>();
    }
}
