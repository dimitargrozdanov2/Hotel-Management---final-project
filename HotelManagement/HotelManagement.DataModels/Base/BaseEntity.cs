﻿using System;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.DataModels.Base
{
    public class BaseEntity : IDeletable, IModifiable
    {
        [Key]
        public string Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}