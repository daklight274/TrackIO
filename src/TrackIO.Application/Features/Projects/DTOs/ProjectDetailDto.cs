using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIO.Application.Features.Projects.DTOs
{
    // Dùng cho detail — đầy đủ thành viên và sprint
    public record ProjectDetailDto(
        string Id,
        string Name,
        string? Description,
        string OwnerName,
        List<ProjectMemberDto> Members,
        List<SprintSummaryDto> Sprints,
        DateTime CreatedAt
    );
}
