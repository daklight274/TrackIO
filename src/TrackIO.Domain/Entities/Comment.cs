using System;
using System.Collections.Generic;
using System.Text;

namespace TrackIO.Domain.Entities
{
    public class Comment
    {
        public string Id { get; set; }
        public string TaskId { get; set; }
        public string AuthorId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        
        // Navigation properties
        public ProjectTask Task { get; set; }
        public User Author  { get; set; }
    }
}
