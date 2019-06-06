using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Web.Areas.Business.Models
{
    public class CreateFeedbackViewModel
    {
        public string BusinessId { get; set; }

        [Required]
        public string AuthorName { get; set; }

        public string Comment { get; set; }

        public string Email { get; set; }
    }
}
