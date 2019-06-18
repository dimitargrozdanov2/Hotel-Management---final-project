using System;

namespace HotelManagement.Services.Exceptions
{
    public class EntityInvalidException : Exception
    {
        public EntityInvalidException(string message)
            : base(message)
        {
        }
    }
}