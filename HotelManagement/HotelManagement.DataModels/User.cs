using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.DataModels
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        public ICollection<LogbookManagers> LogbookManagers { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
