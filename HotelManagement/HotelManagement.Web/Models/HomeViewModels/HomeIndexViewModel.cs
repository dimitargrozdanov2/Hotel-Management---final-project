using HotelManagement.DataModels;
using HotelManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Web.Models.HomeViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<BusinessViewModel> Businesses { get; set; }
    }
}
