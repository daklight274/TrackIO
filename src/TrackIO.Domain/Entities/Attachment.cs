using System;
using System.Collections.Generic;
using System.Text;

namespace TrackIO.Domain.Entities
{
    public class Attachment
    {
        public string Id { get; set; }
        public string TaskId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSizeBytes { get; set; }
        public DateTime UploadedAt { get; set; }
        
        // Navigation properties
        public ProjectTask Task { get; set; }
    }
}
