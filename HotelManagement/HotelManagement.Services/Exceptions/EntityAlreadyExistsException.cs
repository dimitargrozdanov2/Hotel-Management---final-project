using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.Services.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string message)
           : base(message)
        {
        }
    }
}
