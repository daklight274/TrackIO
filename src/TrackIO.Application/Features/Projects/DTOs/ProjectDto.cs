using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIO.Application.Features.Projects.DTOs
{
    // Dùng cho danh sách — chỉ chứa thông tin tóm tắt
    public record ProjectDto(
        string Id,
        string Name,
        string? Description,
        string OwnerName,
        int ParticipantCount,
        int ActiveSprintCount,
        string status,
        DateTime CreatedAt
    );
}
