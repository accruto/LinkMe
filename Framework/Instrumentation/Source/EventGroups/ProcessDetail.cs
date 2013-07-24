using LinkMe.Framework.Utility.Event;

namespace LinkMe.Framework.Instrumentation.Message
{
	public class MessageProcessDetailFactory
		:	IEventDetailFactory
	{
		private static readonly IEventDetailFactory Factory = new ProcessDetailFactory();

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
