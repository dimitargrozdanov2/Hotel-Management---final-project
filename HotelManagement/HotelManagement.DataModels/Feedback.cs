using HotelManagement.DataModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.DataModels
{
    public class Feedback : BaseEntity
    {
        public string Name { get; set; }

        public string Comment { get; set; }

        public string Number { get; set; }

        public double? Rating { get; set; }

        public ICollection<Reply> Replies { get; set; }

        public string BusinessId { get; set; }
        public Business Business { get; set; }
    }
}
