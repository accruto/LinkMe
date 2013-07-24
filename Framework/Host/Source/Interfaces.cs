using System;

namespace LinkMe.Framework.Host
{
    public interface IChannelSink
    {
        IAsyncResult BeginProcessRequest(object message, AsyncCallback callback, object asyncState);
        object EndProcessRequest(IAsyncResult asyncResult);
    }

	public interface IChannelSource
	{
        void Open();
		void Close();

		void Start();
		void Stop();
		void Pause();
		void Continue();
		void Shutdown();
	}

    public interface IChannelAware
    {
        void OnOpen();
        void OnClose();

        void OnStart();
        void OnStop();
        void OnPause();
        void OnContinue();
        void OnShutdown();
    }
}
