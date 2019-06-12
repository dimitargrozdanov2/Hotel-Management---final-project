using HotelManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Web.Areas.Administration.Models.Admin
{
    public class ListBusinessesViewModel
    {
        public IEnumerable<BusinessViewModel> Businesses { get; set; }

        //All the below properties are from CreateBusinessView Model so they can be used in the modal for creating new user
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
