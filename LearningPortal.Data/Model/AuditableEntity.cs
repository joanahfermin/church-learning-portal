using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LearningPortal.Data.Model
{
    public class AuditableEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public long CreatedById { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public long UpdatedById { get; set; }
        public uint Version { get; set; }
    }
}
