using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Web.Areas.Administration.Models.Admin
{
    public class CreateCategoryViewModel
    {
        public string LogbookName { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}