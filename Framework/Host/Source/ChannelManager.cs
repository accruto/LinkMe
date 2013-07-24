using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Framework.Host
{
	public class ChannelManager
		:	System.MarshalByRefObject
	{
	    private Channel _channel;

		public void Start()
		{
            Container.Current.AddConfiguration("linkme.host.container");
            
            // Initialize and start the container.

            _channel = new Channel();
            _channel.Open();
            _channel.Start();
		}

		public void Stop()
		{
            if (_channel != null)
            {
                _channel.Stop();
                _channel.Close();
            }
		}

		public void Pause()
		{
            if (_channel != null)
                _channel.Pause();
		}

		public void Continue()
		{
            if (_channel != null)
                _channel.Continue();
		}

        public ChannelStatus GetStatus()
        {
            return _channel != null ? _channel.Status : ChannelStatus.Stopped;
        }

	    public override object InitializeLifetimeService()
        {
            return null;
        }
	}
}
