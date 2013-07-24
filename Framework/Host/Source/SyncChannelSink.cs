using System;

namespace LinkMe.Framework.Host
{
	public abstract class SyncChannelSink
        : IChannelSink
	{
	    public IAsyncResult BeginProcessRequest(object message, AsyncCallback userCallback, object userState)
		{
			var outerResult = new AsyncResult(userCallback, userState);
			object data;

			try
			{
				message = PreProcess(message, out data);
			}
			catch ( Exception e )
			{
				outerResult.SetComplete(null, e, true);
				return outerResult;
			}

			try
			{
				message = PostProcess(message, data);
			}
			catch ( Exception e )
			{
				outerResult.SetComplete(null, e, true);
				return outerResult;
			}

			outerResult.SetComplete(message, null, true);
			return outerResult;
		}

		public object EndProcessRequest(IAsyncResult result)
		{
			return ((AsyncResult) result).End();
		}

		protected abstract object PreProcess(object message, out object data);
        protected abstract object PostProcess(object message, object data);
	}
}
