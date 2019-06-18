using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HotelManagement.DataModels
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        [JsonIgnore]
        public ICollection<LogbookManagers> LogbookManagers { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}