using HotelManagement.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelManagement.ViewModels
{
    public class BusinessViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime? CreatedOn { get; set; } // might remove

        public DateTime? ModifiedOn { get; set; } // might remove

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<Logbook> BusinessUnits { get; set; } // TODO: Change this to LogbookViewModel

        public ICollection<Feedback> Feedback { get; set; } // TODO: Change this to FeedbackViewModel

        public ICollection<Image> Images { get; set; } // TODO: Change this to FeedbackViewModel
    }
}
