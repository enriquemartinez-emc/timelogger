using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Timelogger.Api.Features.Projects
{
    public class GetProjectsQuery : IRequest<ProjectsResponse>
    {
    }

    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, ProjectsResponse>
    {
        private readonly ApiContext _context;

        public GetProjectsQueryHandler(ApiContext context)
        {
            _context = context;
        }

        public async Task<ProjectsResponse> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _context.Projects
                .OrderByDescending(x => x.Deadline)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new ProjectsResponse()
            {
                Projects = projects
            };
        }
    }
}
