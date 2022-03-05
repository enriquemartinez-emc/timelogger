using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Timelogger.Api.Features.TimeLogs
{
    public class DeleteTimeLogCommand : IRequest
    {
        public DeleteTimeLogCommand(int projectId, int timeLogId)
        {
            ProjectId = projectId;
            TimeLogId = timeLogId;
        }

        public int ProjectId { get; }
        public int TimeLogId { get; }
    }

    public class DeleteTimeLogCommandHandler : IRequestHandler<DeleteTimeLogCommand>
    {
        private readonly ApiContext _context;

        public DeleteTimeLogCommandHandler(ApiContext context) => _context = context;

        public async Task<Unit> Handle(DeleteTimeLogCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects
                .Include(x => x.TimeLogs)
                .FirstOrDefaultAsync(x => x.Id == request.ProjectId, cancellationToken);

            var timeLog = project.TimeLogs.FirstOrDefault(x => x.Id == request.TimeLogId);

            _context.TimeLogs.Remove(timeLog);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
