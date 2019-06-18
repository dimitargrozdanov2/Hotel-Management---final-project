using HotelManagement.DataModels.Base;

namespace HotelManagement.DataModels
{
    public class Image : BaseEntity
    {
        public string Name { get; set; }

        public string BusinessId { get; set; }
        public Business Business { get; set; }
    }
}