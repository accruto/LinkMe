using System;
using System.Threading;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Host.Msmq
{
	public class MsmqSource
		:	IChannelSource
	{
		#region Private Fields

        private static readonly EventSource EventSource = new EventSource(typeof(MsmqSource));
	    private readonly IChannelSink _sink;
	    private readonly int _threadCount;
	    private readonly string _queues;
	    private readonly int _receiveTimeout;
        private readonly int _retryTimeout;
		private MsmqSourceWorker[] _workers;
		private readonly ManualResetEvent _stopSignal = new ManualResetEvent(false);

        public MsmqSource(IChannelSink sink, string queues, int threadCount, int receiveTimeout, int retryTimeout)
	    {
            _sink = sink;
            _queues = queues;
            _threadCount = threadCount;
	        _retryTimeout = retryTimeout;
	        _receiveTimeout = receiveTimeout;
	    }

	    #endregion

	    #region IChannelSource Members

        void IChannelSource.Open()
		{
			const string method = "Open";

			// Do some checks on the configuration values.

			if (_threadCount < 1)
			{
				throw new ParameterOutOfRangeException(GetType(), method, "ThreadCount", _threadCount,
					1, int.MaxValue);
			}

			string[] queues = _queues.Split(';');
			if (queues.Length == 0)
				throw new ParameterStringTooShortException(GetType(), method, "Queues", string.Empty, 1);

			int receiveTimeout = _receiveTimeout;
			if (receiveTimeout < 0)
				throw new ParameterOutOfRangeException(GetType(), method, "ReceiveTimeout", _receiveTimeout, 0, int.MaxValue);

			int retryTimeout = _retryTimeout;
			if (retryTimeout < 0)
				throw new ParameterOutOfRangeException(GetType(), method, "RetryTimeout", _retryTimeout, 0, int.MaxValue);

			// Initialise the workers.

			_workers = new MsmqSourceWorker[_threadCount * queues.Length];

			int workerIndex = 0;
			foreach ( string queue in queues )
			{
				for ( int thread = 0; thread < _threadCount; thread++ )
				{
					_workers[workerIndex] = new MsmqSourceWorker(EventSource, _sink, queue,
						new TimeSpan(0, 0, 0, 0, receiveTimeout),
						new TimeSpan(0, 0, 0, 0, retryTimeout), _stopSignal);
					++workerIndex;
				}
			}
		}

        void IChannelSource.Start()
		{
			_stopSignal.Reset();

			for ( int i = 0; i < _workers.Length; i++ )
				_workers[i].Start();
		}

        void IChannelSource.Stop()
		{
			_stopSignal.Set();

			for ( int i = 0; i < _workers.Length; i++ )
				_workers[i].Stop();

			for ( int i = 0; i < _workers.Length; i++ )
				_workers[i].Join();
		}

        void IChannelSource.Pause()
		{
		}

        void IChannelSource.Continue()
		{
		}

        void IChannelSource.Close()
		{
		}

        void IChannelSource.Shutdown()
        {
        }

		#endregion
	}
}
