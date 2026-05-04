using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackIO.Application.Features.Projects.Commands.CreateProject;
using TrackIO.Application.Features.Projects.Commands.DeleteProject;
using TrackIO.Application.Features.Projects.Commands.UpdateProject;
using TrackIO.Application.Features.Projects.Queries;

namespace TrackIO.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    //[Authorize]  // tất cả endpoint đều cần đăng nhập
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/projects?page=1&pageSize=20&includeArchived=false
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] bool includeArchived = false,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
            => Ok(await _mediator.Send(
                   new GetProjectsQuery(includeArchived, page, pageSize)));

        // GET api/projects/{projectId}
        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetById(string projectId)
            => Ok(await _mediator.Send(new GetProjectByIdQuery(projectId)));

        // POST api/projects
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateProjectCommand command)
        {
            var result = await _mediator.Send(command);
            // 201 Created với Location header trỏ đến resource mới
            return CreatedAtAction(
                nameof(GetById),
                new { projectId = result.Id },
                result);
        }

        // PUT api/projects/{projectId}
        // Chỉ Admin của project mới được sửa
        [HttpPut("{projectId}")]
        [Authorize(Policy = "ProjectAdmin")]
        public async Task<IActionResult> Update(
            string projectId,
            [FromBody] UpdateProjectRequest request)
            => Ok(await _mediator.Send(
                   new UpdateProjectCommand(
                       projectId,
                       request.Name,
                       request.Description)));

        // DELETE api/projects/{projectId}  (thực ra là Archive)
        [HttpDelete("{projectId}")]
        [Authorize(Policy = "ProjectAdmin")]
        public async Task<IActionResult> SoftDelete(string projectId)
        {
            await _mediator.Send(new DeleteProjectCommand(projectId));
            return NoContent();  // 204 — thành công, không có body
        }
    }
    // Request body riêng cho Update — tách khỏi Command
    // để ProjectId lấy từ route, không để client truyền vào body
    public record UpdateProjectRequest(string Name, string? Description);
}
