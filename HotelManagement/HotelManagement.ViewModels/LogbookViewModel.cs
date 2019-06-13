using HotelManagement.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelManagement.ViewModels
{
    public class LogbookViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public BusinessViewModel Business { get; set; }

        [JsonIgnore]
        public ICollection<NoteViewModel> Notes { get; set; }

        public ICollection<LogbookManagers> LogbookManagers { get; set; } // TODO: Should it be just the entity for many to many
    }
}
