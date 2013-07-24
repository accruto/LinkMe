using LinkMe.Framework.Utility.Event;

namespace LinkMe.Framework.Instrumentation.Message
{
	public class MessageNetDetailFactory
		:	IEventDetailFactory
	{
		private static readonly IEventDetailFactory Factory = new NetDetailFactory();

		#region IEventDetailFactory Members

		public string Name
		{
			get { return Factory.Name; }
		}

		public IEventDetail CreateInstance()
		{
			return Factory.CreateInstance();
		}

		#endregion
	}
}
