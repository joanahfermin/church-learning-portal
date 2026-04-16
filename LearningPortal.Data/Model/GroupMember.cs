using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPortal.Data.Model
{
    public class GroupMember : AuditableEntity
    {
        public long GroupId { get; set; }
        public long ClientId { get; set; }

        public Group Group { get; set; }
        public Client Client { get; set; }
    }
}
