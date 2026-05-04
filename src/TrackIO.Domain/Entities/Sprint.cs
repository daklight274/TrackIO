using System;
using System.Collections.Generic;
using System.Text;
using TrackIO.Domain.Enums;

namespace TrackIO.Domain.Entities
{
    public class Sprint
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SprintStatusType Status { get; set; } = SprintStatusType.Planning;
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        // Navigation properties
        public Project Project { get; set; }
    }
}
