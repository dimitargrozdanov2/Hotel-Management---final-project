using HotelManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Web.Areas.Administration.Models.Admin
{
    public class ListLogbooksViewModel
    {
        public IEnumerable<LogbookViewModel> Logbooks { get; set; }
    }
}
