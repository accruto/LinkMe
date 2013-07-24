using System;
using LinkMe.Apps.Services.External.JobSearch;
using LinkMe.Domain.Roles.JobAds.Messages;
using NServiceBus;

namespace LinkMe.Apps.JobExport.JobSearch
{
    public class JobAdSubscriber :
        IHandleMessages<JobAdOpened>,
        IHandleMessages<JobAdUpdated>,
        IHandleMessages<JobAdClosed>

    {
        private readonly IJobAdExporter _exporter;

        public JobAdSubscriber(IJobAdExporter exporter)
        {
            _exporter = exporter;
        }

        #region Message Handlers

        public void Handle(JobAdOpened message)
        {
            _exporter.Add(message.JobAd);
        }

        public void Handle(JobAdUpdated message)
        {
            _exporter.Update(message.JobAd);
        }

        public void Handle(JobAdClosed message)
        {
            _exporter.Delete(message.JobAdId);
        }

        #endregion
    }
}