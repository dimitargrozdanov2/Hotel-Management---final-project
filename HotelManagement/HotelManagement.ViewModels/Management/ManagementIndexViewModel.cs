using System.Collections.Generic;

namespace HotelManagement.ViewModels.Management
{
    public class ManagementIndexViewModel
    {
        public IEnumerable<LogbookViewModel> Logbooks { get; set; }

        public LogbookViewModel SpecifiedLogbook { get; set; }
    }
}