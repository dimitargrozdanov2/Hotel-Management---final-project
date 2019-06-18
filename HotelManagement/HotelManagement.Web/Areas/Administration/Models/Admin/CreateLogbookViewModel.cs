using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Web.Areas.Administration.Models.Admin
{
    public class CreateLogbookViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}