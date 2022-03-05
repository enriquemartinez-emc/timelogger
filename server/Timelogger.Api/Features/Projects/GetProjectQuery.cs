using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Timelogger.Api.Features.Projects
{
    public class GetProjectQuery : IRequest<ProjectResponse>
    {
        public GetProjectQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectResponse>
    {
        private readonly ApiContext _context;

        public GetProjectQueryHandler(ApiContext context)
        {
            _context = context;
        }

        public async Task<ProjectResponse> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects
                .Include(x => x.TimeLogs)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return new ProjectResponse()
            {
                Project = project
            };
        }
    }
}
