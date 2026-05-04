using System;
using System.Collections.Generic;
using System.Text;

namespace TrackIO.Domain.Entities
{
    public class RefreshToken
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public User User { get; set; }
    }
}
