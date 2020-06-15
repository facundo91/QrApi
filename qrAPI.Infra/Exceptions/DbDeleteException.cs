using System;

namespace qrAPI.Infra.Exceptions
{
    public class DbDeleteException : Exception
    {
        public DbDeleteException()
        {
        }

        public DbDeleteException(string message)
            : base(message)
        {
        }

        public DbDeleteException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
