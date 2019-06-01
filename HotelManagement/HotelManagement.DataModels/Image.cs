﻿using HotelManagement.DataModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.DataModels
{
    public class Image : BaseEntity
    {
        public string Name { get; set; }

        public string BusinessId { get; set; }
        public Business Business { get; set; }
    }
}
