using System;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility.PublishedEvents;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Query.JobAds;
using PublishedEvents = LinkMe.Domain.Roles.JobAds.PublishedEvents;

namespace LinkMe.Apps.Agents.Query.Search.JobAdsSupplemental
{
    public class JobAdSortSubscriber
    {
        private readonly IChannelManager<IJobAdSortService> _serviceManager;

        public JobAdSortSubscriber(IChannelManager<IJobAdSortService> serviceManager)
        {
            _serviceManager = serviceManager;
        }

        #region Event Handlers

        [SubscribesTo(PublishedEvents.JobAdOpened)]
        public void OnJobAdOpened(object sender, JobAdOpenedEventArgs e)
        {
            UpdateJobAd(e.JobAdId);
        }

        [SubscribesTo(PublishedEvents.JobAdUpdated)]
        public void OnJobAdChanged(object sender, JobAdEventArgs e)
        {
            UpdateJobAd(e.JobAdId);
        }

        [SubscribesTo(PublishedEvents.JobAdClosed)]
        public void OnJobAdClosed(object sender, JobAdClosedEventArgs e)
        {
            UpdateJobAd(e.JobAdId);
        }

        #endregion

        private void UpdateJobAd(Guid jobAdId)
        {
            // The pattern would say that _jobAdSortEngineCommand.SetModified(jobAdId) would go here
            // but going to assume that the JobAdSearchSubscriber is going to make that call
            // which will trigger the updates of the job ad sort engines.

            // Make sure the local one is updated now.

            var service = _serviceManager.Create();
            try
            {
                service.UpdateJobAd(jobAdId);
            }
            catch (Exception)
            {
                _serviceManager.Abort(service);
                throw;
            }
            _serviceManager.Close(service);
        }
    }
}