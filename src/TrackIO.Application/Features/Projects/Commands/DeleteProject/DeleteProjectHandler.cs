using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIO.Domain.Enums;
using TrackIO.Infrastructure.Data;

namespace TrackIO.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectHandler(AppDbContext db)
    : IRequestHandler<DeleteProjectCommand>
    {
        public async Task Handle(
            DeleteProjectCommand request,
            CancellationToken ct)
        {
            var project = await db.Projects
                .FirstOrDefaultAsync(p => p.Id == request.ProjectId, ct)
                ?? throw new NotFoundException("Project không tồn tại");

            // Business rule 1: không archive project đã archived
            if (project.IsArchived)
                throw new DomainException("Project đã được archive rồi");

            // Business rule 2: không archive khi đang có sprint Active
            var hasActiveSprint = await db.Sprints
                .AnyAsync(s => s.ProjectId == request.ProjectId
                            && s.Status == SprintStatusType.Active, ct);

            if (hasActiveSprint)
                throw new DomainException(
                    "Không thể archive project đang có sprint Active. " +
                    "Hãy close sprint trước.");

            // Soft delete
            project.IsArchived = true;
            await db.SaveChangesAsync(ct);
            // Không trả về gì — Controller trả 204 No Content
        }
    }
}
