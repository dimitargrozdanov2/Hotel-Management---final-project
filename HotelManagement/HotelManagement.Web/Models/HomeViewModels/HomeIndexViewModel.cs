using HotelManagement.ViewModels;
using System.Collections.Generic;

namespace HotelManagement.Web.Models.HomeViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<BusinessViewModel> Businesses { get; set; }
    }
}