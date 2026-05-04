using System;
using System.Collections.Generic;
using System.Text;
using TrackIO.Domain.Enums;

namespace TrackIO.Domain.Entities
{
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ProjectStatusType Status { get; set; } = ProjectStatusType.ACTIVE;
        public string OwnerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ParticipantCount { get; set; }

        // Navigation Properties
        public User Owner { get; set; } = null!;
        public ICollection<ProjectMember> ProjectMembers { get; set; } = [];
        public ICollection<Sprint> Sprints { get; set; } = [];

    }
}
