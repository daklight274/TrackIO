using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIO.Application.Features.Projects.DTOs
{
    public record SprintSummaryDto(
        string Id,
        string Name,
        string Status,
        DateTime StartDate,
        DateTime EndDate
    );
}
