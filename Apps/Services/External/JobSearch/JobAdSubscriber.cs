using System;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Apps.Services.External.JobSearch
{
    public class JobAdSubscriber
    {
        private readonly IChannelManager<IJobAdExporter> _channelManager;

        public JobAdSubscriber(IChannelManager<IJobAdExporter> channelManager)
        {
            _channelManager = channelManager;
        }

        #region Event Handlers

        [SubscribesTo(PublishedEvents.JobAdOpened)]
        public void OnJobAdOpened(object sender, JobAdOpenedEventArgs e)
        {
            var channel = _channelManager.Create();
            try
            {
                channel.Add(e.JobAdId);
                _channelManager.Close(channel);
            }
            catch (Exception)
            {
                _channelManager.Abort(channel);
                throw;
            }
        }

        [SubscribesTo(PublishedEvents.JobAdUpdated)]
        public void OnJobAdUpdated(object sender, JobAdEventArgs e)
        {
            var channel = _channelManager.Create();
            try
            {
                channel.Update(e.JobAdId);
                _channelManager.Close(channel);
            }
            catch (Exception)
            {
                _channelManager.Abort(channel);
                throw;
            }
        }

        [SubscribesTo(PublishedEvents.JobAdClosed)]
        public void OnJobAdClosed(object sender, JobAdClosedEventArgs e)
        {
            var channel = _channelManager.Create();
            try
            {
                channel.Delete(e.JobAdId);
                _channelManager.Close(channel);
            }
            catch (Exception)
            {
                _channelManager.Abort(channel);
                throw;
            }
        }

        #endregion
    }
}
