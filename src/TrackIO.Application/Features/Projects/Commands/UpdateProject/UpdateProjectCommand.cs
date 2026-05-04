using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIO.Application.Features.Projects.DTOs;

namespace TrackIO.Application.Features.Projects.Commands.UpdateProject
{
    // ProjectId lấy từ route, không để client tự truyền vào body
    public record UpdateProjectCommand(
        string ProjectId,    // từ route: /api/projects/{projectId}
        string Name,
        string? Description
    ) : IRequest<ProjectDto>;
}
