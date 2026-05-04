using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIO.Application.Features.Projects.DTOs;
using TrackIO.Infrastructure.Data;

namespace TrackIO.Application.Features.Projects.Queries
{
    public record GetProjectByIdQuery(string ProjectId)
    : IRequest<ProjectDetailDto>;

    public class GetProjectByIdHandler(
        AppDbContext db,
        ICurrentUserService currentUser)
        : IRequestHandler<GetProjectByIdQuery, ProjectDetailDto>
    {
        public async Task<ProjectDetailDto> Handle(
            GetProjectByIdQuery request,
            CancellationToken ct)
        {
            var project = await db.Projects
                .Include(p => p.Owner)
                .Include(p => p.ProjectMembers).ThenInclude(m => m.User)
                .Include(p => p.Sprints)
                .FirstOrDefaultAsync(p => p.Id == request.ProjectId, ct)
                ?? throw new NotFoundException("Project không tồn tại");

            // Kiểm tra user có phải member không
            var isMember = project.Members
                .Any(m => m.UserId == currentUser.UserId);
            if (!isMember)
                throw new ForbiddenException(
                    "Bạn không có quyền xem project này");

            return new ProjectDetailDto(
                project.Id,
                project.Name,
                project.Description,
                project.Owner.DisplayName,
                project.Members.Select(m => new ProjectMemberDto(
                    m.UserId,
                    m.User.DisplayName,
                    m.Role.ToString()
                )).ToList(),
                project.Sprints.Select(s => new SprintSummaryDto(
                    s.Id, s.Name,
                    s.Status.ToString(),
                    s.StartDate, s.EndDate
                )).ToList(),
                project.CreatedAt
            );
        }
    }
}
