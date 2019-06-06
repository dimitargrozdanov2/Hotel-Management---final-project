using HotelManagement.DataModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.DataModels
{
    public class Reply : BaseEntity
    {
        public string Name { get; set; }

        public string Comment { get; set; }

        public string FeedbackId { get; set; }
        public Feedback Feedback { get; set; }
    }
}
