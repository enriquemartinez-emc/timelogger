using FluentValidation;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Timelogger.Api.Features.TimeLogs
{
    public class CreateTimeLogCommand : IRequest<TimeLogResponse>
    {
        public string Description { get; set; }
        public int Duration { get; set; }
        public int ProjectId { get; set; }
    }

    public class CreateTimeLogCommandValidator : AbstractValidator<CreateTimeLogCommand>
    {
        public CreateTimeLogCommandValidator()
        {
            RuleFor(x => x.Duration).NotNull().NotEmpty().GreaterThanOrEqualTo(30);
            RuleFor(x => x.Description).NotNull().NotEmpty();
        }
    }

    public class CreateTimeLogCommandHandler : IRequestHandler<CreateTimeLogCommand, TimeLogResponse>
    {
        private readonly ApiContext _context;

        public CreateTimeLogCommandHandler(ApiContext context)
        {
            _context = context;
        }

        public async Task<TimeLogResponse> Handle(CreateTimeLogCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FindAsync(request.ProjectId);

            if (project == null)
            {
                throw new RestException(HttpStatusCode.NotFound, new { Project = "Not found" });
            }

            if (project.IsCompleted)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { Project = "Cannot add timelogs to completed project" });
            }

            var timeLog = project.AddTimeLog(request.Description, request.Duration);

            await _context.SaveChangesAsync(cancellationToken);

            return new TimeLogResponse()
            {
                TimeLog = timeLog
            };
        }
    }
}
