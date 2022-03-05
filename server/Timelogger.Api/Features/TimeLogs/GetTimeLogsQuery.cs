using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Timelogger.Api.Features.TimeLogs
{
    public class GetTimeLogsQuery : IRequest<TimeLogsResponse>
    {
        public GetTimeLogsQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class GetTimeRegistrationsQueryHandler : IRequestHandler<GetTimeLogsQuery, TimeLogsResponse>
    {
        private readonly ApiContext _context;

        public GetTimeRegistrationsQueryHandler(ApiContext context) => _context = context;

        public async Task<TimeLogsResponse> Handle(GetTimeLogsQuery request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects
                .Include(x => x.TimeLogs)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (project == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { Project = "Project not found" });
            }

            return new TimeLogsResponse()
            {
                TimeRegistrations = project.TimeLogs
            };
        }
    }
}
