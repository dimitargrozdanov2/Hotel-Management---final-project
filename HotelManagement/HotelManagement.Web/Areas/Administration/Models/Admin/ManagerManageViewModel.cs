using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Web.Areas.Administration.Models.Admin
{
    public class ManagerManageViewModel
    {
        public string LogbookName { get; set; }

        [Required]
        public string ManagerEmail { get; set; }
    }
}