using HotelManagement.DataModels.Base;
using HotelManagement.DataModels.Enums;
using Newtonsoft.Json;

namespace HotelManagement.DataModels
{
    public class Note : BaseEntity
    {
        public string Text { get; set; }

        public string LogbookId { get; set; }
        public Logbook Logbook { get; set; }

        public string CategoryId { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }

        public string UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public PriorityType PriorityType { get; set; }
    }
}