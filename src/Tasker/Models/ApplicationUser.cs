using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasker.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser 
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; internal set; }
        public string Email { get; internal set; }
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Initials { get; set; }
        public string Bio { get; set; }
        public string Picture { get; set; }
        public List<BoardDTO> Boards { get; set; }
    }
}
