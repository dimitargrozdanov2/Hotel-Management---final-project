using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Web.Areas.Administration.Models.Admin
{
    public class AddImageToBusinessViewModel
    {
        [Required]
        public string BusinessName { get; set; }

        [Required]
        public string ImageName { get; set; }

        [Required]
        public IFormFile Image { get; set; }


    }
}
