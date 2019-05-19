using HotelManagement.DataModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.DataModels
{
    public class Note : Entity
    {
        public string Text { get; set; }

        public string CategoryID { get; set; }
        public Category Category { get; set; }

        public string UserID { get; set; }
        public User User { get; set; }
    }
}
