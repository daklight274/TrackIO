using System;
using System.Collections.Generic;
using System.Text;

namespace TrackIO.Domain.Entities
{
    public class User
    {
        public string Id { get; set;  }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public bool IsEmailVerifed { get; set; } = false;
        public bool IsAdmin { get; set; } = false;

        public ICollection<ProjectMember> ProjectMembers{ get; set; } = [];
        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
