using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIO.Application.Features.Projects.DTOs;
using TrackIO.Domain.Entities;
using TrackIO.Domain.Enums;
using TrackIO.Infrastructure.Data;

namespace TrackIO.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectHandler(
        AppDbContext db,
        ICurrentUserService currentUser)
        : IRequestHandler<CreateProjectCommand, ProjectDto>
    {
        public async Task<ProjectDto> Handle(
            CreateProjectCommand request,
            CancellationToken ct)
        {
            // 1. Tạo Project entity
            var project = new Project
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description,
                ParticipantCount = request.ParticipantCount,
                Status = ProjectStatusType.ACTIVE,
                OwnerId = currentUser.UserId  // lấy từ JWT token
            };
            db.Projects.Add(project);

            // 2. Owner tự động là PM của project vừa tạo
            var ownerMember = new ProjectMember
            {
                ProjectId = project.Id,
                UserId = currentUser.UserId,
                Role = ProjectRoleType.PM
            };
            db.ProjectMembers.Add(ownerMember);

            // 3. Lưu cả 2 record cùng lúc (1 transaction)
            await db.SaveChangesAsync(ct);

            // 4. Trả về DTO — không trả Entity thô
            return new ProjectDto(
                project.Id,
                project.Name,
                project.Description,
                currentUser.DisplayName,
                ParticipantCount: 1,   // vừa tạo, chỉ có owner
                ActiveSprintCount: 0,
                project.Status.ToString(),
                project.CreatedAt
            );
        }
    }
}
