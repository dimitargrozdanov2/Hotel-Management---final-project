using HotelManagement.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

        public ICollection<NoteViewModel> Notes { get; set; }
    }
}
