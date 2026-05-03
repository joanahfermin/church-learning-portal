using LearningPortal.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPortal.Data.Model
{
    //Represents a person's profile and learning identity. Linked one-to-one with User. All learning activity belongs to Client, not User.
    public class Client : AuditableEntity
    {
        public long UserId { get; set; }
        public string FullName { get; set; } = String.Empty;

        public User User { get; set; } = null!;
        public ICollection<GroupMember> GroupMembers{ get; set; } = [];
    }
}
