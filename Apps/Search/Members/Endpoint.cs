using System;
using LinkMe.Framework.Host.Wcf;
using NServiceBus;

namespace LinkMe.Apps.Search.Members
{
    public class Endpoint
        : IWantToRunAtStartup
    {
        private readonly WcfTcpHost _host;

        public Endpoint(WcfTcpHost host)
        {
            _host = host;
        }

        #region Implementation of IWantToRunAtStartup

        public void Run()
        {
            _host.Start();
        }

        public void Stop()
        {
            _host.Stop();
            _host.Close();
        }

        #endregion
    }
}
