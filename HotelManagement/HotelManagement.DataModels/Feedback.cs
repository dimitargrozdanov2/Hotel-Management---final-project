using HotelManagement.DataModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.DataModels
{
    public class Feedback : Entity
    {
        public string Name { get; set; }

        public string Comment { get; set; }

        public string Number { get; set; }
    }
}
