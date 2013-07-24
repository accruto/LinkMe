namespace LinkMe.Framework.Instrumentation
{
	internal sealed class NativeMethods
	{
		public const int MQ_ACTION_RECEIVE = 0;
		public const int MQ_ACTION_PEEK_CURRENT = -2147483648;
		public const int MQ_ACTION_PEEK_NEXT = -2147483647;
		public const int MQ_OK = 0;

		private NativeMethods()
		{
		}
	}
}
