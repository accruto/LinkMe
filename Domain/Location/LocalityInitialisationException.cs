using System;
using System.Runtime.Serialization;

namespace LinkMe.Domain.Location
{
    [Serializable]
    public class LocalityInitialisationException : Exception
    {
        internal LocalityInitialisationException(Exception inner)
            : base("Failed to initialise Locality data.", inner)
        {
        }

        protected LocalityInitialisationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
