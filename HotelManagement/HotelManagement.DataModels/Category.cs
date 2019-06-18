using HotelManagement.DataModels.Base;
using System.Collections.Generic;

namespace HotelManagement.DataModels
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public string LogbookId { get; set; }
        public Logbook Logbook { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}