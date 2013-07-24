using System.Runtime.Serialization;

namespace LinkMe.Framework.Utility.Exceptions
{
	[System.Serializable]
	public sealed class SystemException
		:	System.Exception
	{
		internal SystemException(string source, string message, string stackTrace, string className, System.Exception innerException)
			:	base(message, innerException)
		{
			Source = source;
			m_stackTrace = stackTrace;
			m_className = className;
		}

		private SystemException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
            m_stackTrace = info.GetString(Constants.Serialization.Exception.StackTrace);
			m_className = info.GetString(Constants.Serialization.Exception.Class);
		}

		public override string StackTrace
		{
			get { return m_stackTrace; }
		}

		public string Class
		{
			get { return m_className; }
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue(Constants.Serialization.Exception.StackTrace, m_stackTrace);
			info.AddValue(Constants.Serialization.Exception.Class, m_className);
		}

		private string m_stackTrace;
		private string m_className;
	}
}
