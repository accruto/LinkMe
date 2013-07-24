using LinkMe.Apps.Agents.Applications;
using LinkMe.Framework.Host;

namespace LinkMe.Utility.Configuration
{
    public class ApplicationChannel
        : IChannelAware
    {
        #region Implementation of IChannelAware

        void IChannelAware.OnOpen()
        {
            ApplicationContext.SetupApplications(WebSite.LinkMe); // set up the application to point to the web site if needed
        }

        void IChannelAware.OnClose()
        {
        }

        void IChannelAware.OnStart()
        {
        }

        void IChannelAware.OnStop()
        {
        }

        void IChannelAware.OnPause()
        {
        }

        void IChannelAware.OnContinue()
        {
        }

        void IChannelAware.OnShutdown()
        {
        }

        #endregion
    }
}
