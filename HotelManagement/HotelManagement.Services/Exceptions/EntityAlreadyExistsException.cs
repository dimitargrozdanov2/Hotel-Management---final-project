using System;

namespace HotelManagement.Services.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException()
        {
        }

        public EntityAlreadyExistsException(string message)
           : base(message)
        {
        }
    }
}