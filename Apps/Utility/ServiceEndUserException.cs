using System;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Utility
{
	/// <summary>
	/// An exception with a message that can safely be displayed to the end user of a web service. Exceptions
    /// of this type are also emailed to a different mailbox (not the usual logging one).
    /// 
    /// Note: if you want the error displayed to the user, but not emailed - throw EndUserException instead.
	/// </summary>
	[Serializable]
	public class ServiceEndUserException : UserException
	{
        private readonly string _requestData;

		public ServiceEndUserException()
		{
		}

		public ServiceEndUserException(string message)
			: base(message)
		{
		}

        public ServiceEndUserException(string message, string requestData)
            : base(message)
        {
            _requestData = requestData;
        }

		public ServiceEndUserException(string message, Exception innerEx)
			: base(message, innerEx)
		{
		}

        // This is a (somewhat dodgy) way to "convert" an EndUserException into a ServiceEndUserException.
        public ServiceEndUserException(UserException originalEx)
            : this(originalEx.Message, originalEx)
        {
        }

	    public string RequestData
	    {
	        get { return _requestData; }
	    }
	}
}
