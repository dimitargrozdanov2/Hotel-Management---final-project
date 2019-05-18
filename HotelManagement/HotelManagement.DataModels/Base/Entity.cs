using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.DataModels.Base
{
    public class Entity :IDeletable, IModifiable
    {
        public string Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
