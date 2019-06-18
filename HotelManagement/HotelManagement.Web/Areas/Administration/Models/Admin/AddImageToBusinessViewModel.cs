using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Web.Areas.Administration.Models.Admin
{
    public class AddImageToBusinessViewModel
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string ImageName { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}