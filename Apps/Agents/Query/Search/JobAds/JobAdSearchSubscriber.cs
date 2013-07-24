using System;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.PublishedEvents;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Query.JobAds;
using PublishedEvents = LinkMe.Domain.Roles.JobAds.PublishedEvents;

namespace LinkMe.Apps.Agents.Query.Search.JobAds
{
    public class JobAdSearchSubscriber
    {
        private static readonly EventSource EventSource = new EventSource<JobAdSearchSubscriber>();
        private readonly IChannelManager<IJobAdSearchService> _serviceManager;
        private readonly IJobAdSearchEngineCommand _jobAdSearchEngineCommand;

        public JobAdSearchSubscriber(IChannelManager<IJobAdSearchService> serviceManager, IJobAdSearchEngineCommand jobAdSearchEngineCommand)
        {
            _serviceManager = serviceManager;
            _jobAdSearchEngineCommand = jobAdSearchEngineCommand;
        }

        #region Event Handlers

        [SubscribesTo(PublishedEvents.JobAdOpened)]
        public void OnJobAdOpened(object sender, JobAdOpenedEventArgs e)
        {
            const string method = "OnJobAdOpened";
            EventSource.Raise(Event.Trace, method, "Job ad '" + e.JobAdId + "' opened.");

            UpdateJobAd(e.JobAdId);
        }

        [SubscribesTo(PublishedEvents.JobAdUpdated)]
        public void OnJobAdChanged(object sender, JobAdEventArgs e)
        {
            const string method = "OnJobAdChanged";
            EventSource.Raise(Event.Trace, method, "Job ad '" + e.JobAdId + "' changed.");

            UpdateJobAd(e.JobAdId);
        }

        [SubscribesTo(PublishedEvents.JobAdClosed)]
        public void OnJobAdClosed(object sender, JobAdClosedEventArgs e)
        {
            const string method = "OnJobAdClosed";
            EventSource.Raise(Event.Trace, method, "Job ad '" + e.JobAdId + "' closed.");

            UpdateJobAd(e.JobAdId);
        }

        #endregion

        private void UpdateJobAd(Guid jobAdId)
        {
            const string method = "UpdateJobAd";
            EventSource.Raise(Event.Trace, method, "Updating modification for job ad '" + jobAdId + "'.");

            // Update all search engines.

            _jobAdSearchEngineCommand.SetModified(jobAdId);

            // Make sure the local one is updated now.

            EventSource.Raise(Event.Trace, method, "Updating local search engine for job ad '" + jobAdId + "'.");

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