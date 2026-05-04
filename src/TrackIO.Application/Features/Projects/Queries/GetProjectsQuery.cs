using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using TrackIO.Application.Features.Projects.DTOs;
using TrackIO.Domain.Enums;
using TrackIO.Infrastructure.Data;

namespace TrackIO.Application.Features.Projects.Queries
{
    public record GetProjectsQuery(
        bool IncludeArchived = false,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<PagedResult<ProjectDto>>;

    public class GetProjectsHandler(
        AppDbContext db,
        ICurrentUserService currentUser)
        : IRequestHandler<GetProjectsQuery, PagedResult<ProjectDto>>
    {
        public async Task<PagedResult<ProjectDto>> Handle(
            GetProjectsQuery request,
            CancellationToken ct)
        {
            // Chỉ lấy project mà user hiện tại là member
            var query = db.Projects
                .Where(p => p.ProjectMembers
                    .Any(m => m.UserId == currentUser.UserId))
                .Where(p => request.IncludeArchived || p.Status != ProjectStatusType.CANCEL)
                .OrderByDescending(p => p.CreatedAt);

            // Đếm tổng trước khi phân trang (cho TotalCount)
            var total = await query.CountAsync(ct);

            // Dùng Select() projection — EF chỉ SELECT đúng field cần
            // Không dùng .ToList() rồi mới map — như vậy SELECT *
            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new ProjectDto(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Owner.FullName,
                    p.ProjectMembers.Count(),
                    p.Sprints.Count(s => s.Status == SprintStatusType.Active),
                    p.Status.ToString(),
                    p.CreatedAt
                ))
                .ToListAsync(ct);

            return new PagedResult<ProjectDto>(
                items, total, request.Page, request.PageSize);
        }
    }
}
