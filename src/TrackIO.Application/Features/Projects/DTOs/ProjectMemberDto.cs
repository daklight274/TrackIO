using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIO.Application.Features.Projects.DTOs
{
    public record ProjectMemberDto(
        string UserId,
        string DisplayName,
        string Role         // "Admin" | "PM" | "Developer"
    );
}
