using HotelManagement.DataModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<LogbookManagers> LogbookManagers { get; set; }

        [JsonIgnore]
        public ICollection<NoteViewModel> Notes { get; set; }
    }
}