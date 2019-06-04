using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Web.Areas.Administration.Models.Admin
{
    public class CreateBusinessViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
