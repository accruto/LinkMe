using System;

namespace LinkMe.Framework.Host.Service
{
	internal class ServiceManager
	{
		public ServiceManager(ServiceParameters parameters)
		{
			_parameters = parameters;
		}

		public void Start()
		{
            // Need to determine of there is an application root folder.

            if (string.IsNullOrEmpty(_parameters.ApplicationRootFolder) && string.IsNullOrEmpty(_parameters.ConfigurationFile))
            {
                _appDomain = AppDomain.CreateDomain(_parameters.ServiceName);
            }
            else
            {
                var info = new AppDomainSetup();
                if (!string.IsNullOrEmpty(_parameters.ApplicationRootFolder))
                    info.ApplicationBase = _parameters.ApplicationRootFolder;
                if (!string.IsNullOrEmpty(_parameters.ConfigurationFile))
                    info.ConfigurationFile = _parameters.ConfigurationFile;
                _appDomain = AppDomain.CreateDomain(_parameters.ServiceName, null, info);
            }

            _channelManager = (ChannelManager)_appDomain.CreateInstanceAndUnwrap(typeof(ChannelManager).Assembly.FullName, typeof(ChannelManager).FullName);
			_channelManager.Start();
		}

	    public void Stop()
		{
			if ( _appDomain != null )
			{
				_channelManager.Stop();
				AppDomain.Unload(_appDomain);
				_appDomain = null;
			}
		}

		public void Pause()
		{
			_channelManager.Pause();
		}

		public void Continue()
		{
			_channelManager.Continue();
		}

        public ChannelStatus Status
		{
            get { return _appDomain == null ? ChannelStatus.Stopped : _channelManager.GetStatus(); }
		}

	    private readonly ServiceParameters _parameters;
		private AppDomain _appDomain;
        private ChannelManager _channelManager;
	}
}
