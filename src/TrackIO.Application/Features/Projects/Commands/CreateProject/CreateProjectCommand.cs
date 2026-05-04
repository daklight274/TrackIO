using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIO.Application.Features.Projects.DTOs;

namespace TrackIO.Application.Features.Projects.Commands.CreateProject
{
    // Command là một "yêu cầu" gửi vào hệ thống
    // IRequest<T> nghĩa là khi send command này, kết quả trả về là T
    public record CreateProjectCommand(string Name,string? Description, int ParticipantCount) : IRequest<ProjectDto>;
}
