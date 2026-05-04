using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIO.Application.Features.Projects.DTOs;
using TrackIO.Domain.Enums;
using TrackIO.Infrastructure.Data;

namespace TrackIO.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectHandler(AppDbContext db)
    : IRequestHandler<UpdateProjectCommand, ProjectDto>
    {
        public async Task<ProjectDto> Handle(
            UpdateProjectCommand request,
            CancellationToken ct)
        {
            // 1. Tìm project — throw NotFoundException nếu không có
            var project = await db.Projects
                .Include(p => p.Owner)
                .Include(p => p.ProjectMembers)
                .Include(p => p.Sprints)
                .FirstOrDefaultAsync(
                    p => p.Id == request.ProjectId && !p.Status, ct)
                ?? throw new NotFoundException(
                    $"Project {request.ProjectId} không tồn tại");

            // 2. Cập nhật — chỉ những field được phép thay đổi
            project.Name = request.Name;
            project.Description = request.Description;
            // Không cho phép đổi OwnerId ở đây

            // 3. EF Core tự detect change, không cần gọi Update()
            await db.SaveChangesAsync(ct);

            // 4. Trả về DTO với dữ liệu mới nhất
            return new ProjectDto(
                project.Id,
                project.Name,
                project.Description,
                project.Owner.DisplayName,
                project.Members.Count,
                project.Sprints.Count(s => s.Status == SprintStatusType.Active),
                project.CreatedAt,
                project.IsArchived
            );
        }
    }
}
