using System;
using System.Diagnostics;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Host.Exceptions;
using LinkMe.Framework.Utility.Unity;
using Exception=System.Exception;

namespace LinkMe.Framework.Host
{
	public enum ChannelStatus
	{
		Stopped,
		Running,
		Paused,
	}

	public class Channel
	{
		#region Private Fields

		private ChannelStatus _status;
		private volatile bool _initialised;
	    private readonly object _initialiseLock = new object();

		private IChannelSource _source;

		private static readonly EventSource EventSource = new EventSource<Channel>();

		#endregion

	    #region Public Methods

        public ChannelStatus Status
		{
			get { return _status; }
		}

		public void Open()
		{
			if ( !_initialised )
			{
				lock ( _initialiseLock )
				{
					if ( !_initialised )
					{
                        try
                        {
                            InitializeCore();
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException(string.Format("Failed to open the container."), ex);
                        }

					    _initialised = true;
					}
				}
			}
		}

		public void Close()
		{
			// Method entry trace

			const string method = "Close";

			if (EventSource.IsEnabled(Event.MethodEnter))
				EventSource.Raise(Event.MethodEnter, method);

		    try
		    {
			    // Finalize all the sources

                _source.Close();
			    _source = null;

			    _initialised = false;
            }
            catch (Exception ex)
            {
                if (EventSource.IsEnabled(Event.CriticalError))
                    EventSource.Raise(Event.CriticalError, method, "Cannot close the channel.", ex);
            }

			// Method exit trace

			if (EventSource.IsEnabled(Event.MethodExit))
				EventSource.Raise(Event.MethodExit, method);
		}

		public void Start()
		{
			// Method entry trace

			const string method = "Start";

			if (EventSource.IsEnabled(Event.MethodEnter))
				EventSource.Raise(Event.MethodEnter, method);

			if (!_initialised)
				throw new ContainerNotInitialisedException(typeof(Channel), method);

            try
            {
                // Start the source.

                _source.Start();

                // Update the status.

                _status = ChannelStatus.Running;
            }
            catch (Exception ex)
            {
                if (EventSource.IsEnabled(Event.CriticalError))
                    EventSource.Raise(Event.CriticalError, method, "Cannot start the channel.", ex);
            }

		    // Method exit trace

			if (EventSource.IsEnabled(Event.MethodExit))
				EventSource.Raise(Event.MethodExit, method);
		}

		public void Stop()
		{
			// Method entry trace

			const string method = "Stop";

			if (EventSource.IsEnabled(Event.MethodEnter))
				EventSource.Raise(Event.MethodEnter, method);

			if (!_initialised)
                throw new ContainerNotInitialisedException(typeof(Channel), method);

            try
            {
			    // Stop the source

                _source.Stop();

			    // Update the status.

                _status = ChannelStatus.Stopped;
            }
            catch (Exception ex)
            {
                if (EventSource.IsEnabled(Event.CriticalError))
                    EventSource.Raise(Event.CriticalError, method, "Cannot stop the channel.", ex);
            }

			// Method exit trace

			if (EventSource.IsEnabled(Event.MethodExit))
				EventSource.Raise(Event.MethodExit, method);
		}

		public void Pause()
		{
			// Method entry trace

			const string method = "Pause";

			if (EventSource.IsEnabled(Event.MethodEnter))
				EventSource.Raise(Event.MethodEnter, method);

			if (!_initialised)
                throw new ContainerNotInitialisedException(typeof(Channel), method);

            try
            {
			    // Pause the source.

                _source.Pause();

			    // Update the status.

                _status = ChannelStatus.Paused;
            }
            catch (Exception ex)
            {
                if (EventSource.IsEnabled(Event.CriticalError))
                    EventSource.Raise(Event.CriticalError, method, "Cannot pause the channel.", ex);
            }

			// Method exit trace

			if (EventSource.IsEnabled(Event.MethodExit))
				EventSource.Raise(Event.MethodExit, method);
		}

		public void Continue()
		{
			// Method entry trace

			const string method = "Continue";

			if (EventSource.IsEnabled(Event.MethodEnter))
				EventSource.Raise(Event.MethodEnter, method);

			if (!_initialised)
                throw new ContainerNotInitialisedException(typeof(Channel), method);

            try
            {
			    // Continue the source.

                _source.Continue();

			    // Update the status.

                _status = ChannelStatus.Running;
            }
            catch (Exception ex)
            {
                if (EventSource.IsEnabled(Event.CriticalError))
                    EventSource.Raise(Event.CriticalError, method, "Cannot continue the channel.", ex);
            }

			// Method exit trace

			if (EventSource.IsEnabled(Event.MethodExit))
				EventSource.Raise(Event.MethodExit, method);
		}

		#endregion

		#region Private Methods

		private void InitializeCore()
		{
		    Debug.Assert(!_initialised, "!_initialised");

		    var source = Container.Current.Resolve<IChannelSource>();
            source.Open();
		    _source = source;
		}

		#endregion
	}
}
