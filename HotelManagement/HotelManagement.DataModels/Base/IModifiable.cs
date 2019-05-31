using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.DataModels.Base
{
    public interface IModifiable
    {
        DateTime? CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
