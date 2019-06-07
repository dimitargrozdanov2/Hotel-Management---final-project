using HotelManagement.DataModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelManagement.DataModels
{
    public class Feedback : BaseEntity
    {
        public string Name { get; set; }

        public string Comment { get; set; }

        public string Email { get; set; }

        public double? Rating { get; set; }

        public List<Feedback> Replies { get; set; }

        public string FeedbackParentId { get; set; }
        public Feedback FeedbackParent { get; set; }

        public string BusinessId { get; set; }
        public Business Business { get; set; }
    }
}
