using LinkMe.Framework.Utility.Event;

namespace LinkMe.Framework.Instrumentation.Message
{
	public class MessageThreadDetailFactory
		:	IEventDetailFactory
	{
		private static readonly IEventDetailFactory Factory = new ThreadDetailFactory();

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
