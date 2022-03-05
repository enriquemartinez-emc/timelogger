using System.Collections.Generic;
using Timelogger.Entities;

namespace Timelogger.Api.Features.Projects
{
    public class ProjectsResponse
    {
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
