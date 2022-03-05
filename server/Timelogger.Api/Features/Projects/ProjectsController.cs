using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Timelogger.Api.Features.Projects
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public Task<ProjectsResponse> Get(GetProjectsQuery request) => _mediator.Send(request);

        [HttpGet("{id}")]
        public Task<ProjectResponse> GetById(int id) => _mediator.Send(new GetProjectQuery(id));

        [HttpPut("{id}/complete")]
        public Task Complete(int id) => _mediator.Send(new CompleteProjectCommand(id));
    }
}
