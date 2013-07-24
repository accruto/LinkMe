using System;

namespace LinkMe.Framework.Utility.Exceptions
{
    public class UserException
        : Exception
    {
        public UserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UserException(string message)
            : base(message)
        {
        }

        public UserException()
        {
        }

        public virtual object[] GetErrorMessageParameters()
        {
            return new object[0];
        }
    }
}