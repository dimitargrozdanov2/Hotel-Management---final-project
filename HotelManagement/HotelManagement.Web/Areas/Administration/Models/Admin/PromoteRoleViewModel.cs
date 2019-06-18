using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Web.Areas.Administration.Models.Admin
{
    public class PromoteRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}