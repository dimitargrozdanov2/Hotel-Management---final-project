using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.ViewModels
{
    public class BusinessViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<LogbookViewModel> BusinessUnits { get; set; }

        public ICollection<FeedbackViewModel> Feedback { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }
    }
}