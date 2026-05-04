using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Xml.Linq;
using TrackIO.Domain.Enums;

namespace TrackIO.Domain.Entities
{
    public class ProjectTask
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string? SprintId { get; set; }      // null = Backlog chưa vào sprint
        public string? AssigneeId { get; set; }    // null = chưa assign
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskStatusType Status { get; set; } = TaskStatusType.Backlog;
        public PriorityType Priority { get; set; } = PriorityType.Medium;
        public int StoryPoints { get; set; }
        public DateTime? Deadline { get; set; }
        public string? BlockedReason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Project Project { get; set; } = null!;
        public Sprint? Sprint { get; set; }
        public User? Assignee { get; set; }
        public ICollection<Comment> Comments { get; set; } = [];
        public ICollection<Attachment> Attachments { get; set; } = [];
    }
}
