using System;
using System.Runtime.Serialization;

namespace LinkMe.Utility
{
	[Serializable]
	public class LinkMeApplicationException : Exception
	{
		public LinkMeApplicationException() : base()
		{
		}

		public LinkMeApplicationException(string message) : base(message)
		{
		}

		public LinkMeApplicationException(string message, Exception cause) : base(message, cause)
		{
		}

		protected LinkMeApplicationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}