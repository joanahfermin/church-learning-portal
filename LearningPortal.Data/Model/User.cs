using LearningPortal.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPortal.Data.Model
{
    //Represents an authenticated identity. Contains only auth-related fields — profile and learning activity are handled by the Client entity.
    public class User
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role{ get; set; }  = UserRole.Client;
        public bool IsActive { get; set; } = true;

        public Client? Client { get; set; }
    }
}
