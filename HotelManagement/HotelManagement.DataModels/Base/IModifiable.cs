using System;

namespace HotelManagement.DataModels.Base
{
    public interface IModifiable
    {
        DateTime? CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}