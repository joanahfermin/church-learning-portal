using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPortal.Data.Model
{
    public class Group : AuditableEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<GroupMember> Members { get; set; }
    }
}
