using HotelManagement.DataModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelManagement.DataModels
{
    public class Business : BaseEntity
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<Logbook> BusinessUnits { get; set; }

        public ICollection<Feedback> Feedback { get; set; }
    }
}
