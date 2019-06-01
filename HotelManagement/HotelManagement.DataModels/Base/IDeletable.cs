using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.DataModels.Base
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
    }
}
