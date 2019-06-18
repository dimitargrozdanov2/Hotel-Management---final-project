using HotelManagement.ViewModels;
using System.Collections.Generic;

namespace HotelManagement.Web.Areas.Administration.Models.Admin
{
    public class ListLogbooksViewModel
    {
        public IEnumerable<LogbookViewModel> Logbooks { get; set; }

        public string BusinessName { get; set; }
    }
}