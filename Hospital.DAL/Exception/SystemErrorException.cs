using System;

namespace Hospital.DAL.Exceptions
{
    public class SystemErrorException : Exception
    {
        public SystemErrorException() : base("A system error has occurred.")
        {
        }

        public SystemErrorException(string message) : base(message)
        {
        }

        public SystemErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
