using LinkMe.Domain.Roles.JobAds.Messages;
using LinkMe.Query.JobAds;
using NServiceBus;

namespace LinkMe.Apps.Search.JobAds
{
    public class JobAdSearchSubscriber :
        IHandleMessages<JobAdOpened>,
        IHandleMessages<JobAdUpdated>,
        IHandleMessages<JobAdClosed>
    {
        private readonly IJobAdSearchService _service;

        public JobAdSearchSubscriber(IJobAdSearchService service)
        {
            _service = service;
        }

        public void Handle(JobAdOpened message)
        {
            _service.UpdateJobAd(message.JobAd);
        }

        public void Handle(JobAdUpdated message)
        {
            _service.UpdateJobAd(message.JobAd);
        }

        public void Handle(JobAdClosed message)
        {
            _service.RemoveJobAd(message.JobAdId);
        }
    }
}
