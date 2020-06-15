using System;

namespace qrAPI.Infra.Exceptions
{
    public class InvalidIdException : Exception
    {

        public InvalidIdException()
        {
        }

        public InvalidIdException(string message)
            : base(message)
        {
        }

        public InvalidIdException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
