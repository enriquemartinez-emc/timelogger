using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Timelogger.Api.Features.TimeLogs
{
    [Route("api/projects")]
    [ApiController]
    public class TimeLogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimeLogsController(IMediator mediator) => _mediator = mediator;
        
        [HttpGet("{id}/timelogs")]
        public Task<TimeLogsResponse> Get(int id)
        {
            return _mediator.Send(new GetTimeLogsQuery(id));
        }

        [HttpPost("{id}/timelogs")]
        public Task<TimeLogResponse> Create(int id, [FromBody] CreateTimeLogCommand request)
        {
            request.ProjectId = id;
            return _mediator.Send(request);
        }

        [HttpDelete("{id}/timelogs/{timeLogId}")]
        public Task Delete(int id, int timeLogId)
        {
            return _mediator.Send(new DeleteTimeLogCommand(id, timeLogId));
        }
    }
}
