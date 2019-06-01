using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.DataModels
{
    public class LogbookManagers
    {
        public string LogbookId { get; set; }
        public Logbook Logbook { get; set; }

        public string ManagerId { get; set; }
        public User Manager { get; set; }
    }
}
