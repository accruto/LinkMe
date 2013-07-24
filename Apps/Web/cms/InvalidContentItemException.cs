using System;
using System.Runtime.Serialization;

namespace LinkMe.Web.Cms
{
    [Serializable]
    public class InvalidContentItemException : Exception
    {
        public InvalidContentItemException(string message)
            : base(message)
        {
        }

        public InvalidContentItemException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected InvalidContentItemException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
