using LinkMe.Framework.Utility.Event;

namespace LinkMe.Framework.Instrumentation.Message
{
	public class MessageSecurityDetailFactory
		:	IEventDetailFactory
	{
		private static readonly IEventDetailFactory m_factory = new SecurityDetailFactory();

		#region IEventDetailFactory Members

		public string Name
		{
			get { return m_factory.Name; }
		}

		public IEventDetail CreateInstance()
		{
			return m_factory.CreateInstance();
		}

		#endregion
	}
}
