using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Web.Areas.Administration.Models.Admin
{
    public class DeleteLogbookViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}