using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Timelogger.Api.Features.Projects
{
    public class CompleteProjectCommand : IRequest
    {
        public CompleteProjectCommand(int projectId)
        {
            ProjectId = projectId;
        }

        public int ProjectId { get; set; }
    }

    public class CompleteProjectCommandHandler : IRequestHandler<CompleteProjectCommand>
    {
        private readonly ApiContext _context;

        public CompleteProjectCommandHandler(ApiContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FindAsync(request.ProjectId);

            project.SetCompletedStatus();

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
