using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Web.Areas.Administration.Models.Admin
{
    public class DeleteLogbookViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
