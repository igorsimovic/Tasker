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
        public object PasswordHash { get; internal set; }
        public string Email { get; internal set; }
    }
}
