namespace HotelManagement.DataModels.Base
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
    }
}