using HotelManagement.DataModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.DataModels
{
    public class Logbook : Entity
    {
        public string Name { get; set; }

        public ICollection<Note> Notes { get; set; }

        public ICollection<LogbookManagers> LogbookManagers { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
