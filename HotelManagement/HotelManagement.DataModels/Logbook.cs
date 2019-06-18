using HotelManagement.DataModels.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HotelManagement.DataModels
{
    public class Logbook : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string BusinessId { get; set; }
        public Business Business { get; set; }

        [JsonIgnore]
        public ICollection<Category> Categories { get; set; }

        [JsonIgnore]
        public ICollection<Note> Notes { get; set; }

        [JsonIgnore]
        public ICollection<LogbookManagers> LogbookManagers { get; set; }
    }
}