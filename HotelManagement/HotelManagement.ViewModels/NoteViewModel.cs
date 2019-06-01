using HotelManagement.DataModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelManagement.ViewModels
{
    public class NoteViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public LogbookViewModel Logbook { get; set; }

        public CategoryViewModel Category { get; set; }

        public UserViewModel User { get; set; }

        public PriorityType PriorityType { get; set; }
    }
}
