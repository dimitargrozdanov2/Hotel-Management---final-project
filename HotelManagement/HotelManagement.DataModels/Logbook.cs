using HotelManagement.DataModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.DataModels
{
    public class Logbook : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string BusinessId { get; set; }
        public Business Business { get; set; }

        public ICollection<Note> Notes { get; set; }

        public ICollection<LogbookManagers> LogbookManagers { get; set; }
    }
}
