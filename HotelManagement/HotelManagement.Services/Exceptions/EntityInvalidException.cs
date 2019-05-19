using System;
using System.Collections.Generic;
using System.Text;

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
