using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelManagement.ViewModels
{
    public class FeedbackViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Comment { get; set; }

        public string Number { get; set; }

        public double? Rating { get; set; }

        [Required]
        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public BusinessViewModel Business { get; set; }
    }
}
