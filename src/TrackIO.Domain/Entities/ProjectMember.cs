using System;
using System.Collections.Generic;
using System.Text;
using TrackIO.Domain.Enums;

namespace TrackIO.Domain.Entities
{
    public class ProjectMember
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string UserId { get; set; }
        public DateTime JoinedAt { get; set; }
        public ProjectRoleType Role { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Project Project { get; set; }
    }
}
