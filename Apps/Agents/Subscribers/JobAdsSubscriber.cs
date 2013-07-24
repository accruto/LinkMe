using LinkMe.Apps.Agents.Domain.Roles.JobAds.Handlers;
using LinkMe.Domain.Users.Employers.JobAds;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Apps.Agents.Subscribers
{
    public class JobAdsSubscriber
    {
        private readonly IJobAdsHandler _jobAdsHandler;

        public JobAdsSubscriber(IJobAdsHandler jobAdsHandler)
        {
            _jobAdsHandler = jobAdsHandler;
        }

        [SubscribesTo(PublishedEvents.ApplicationSubmitted)]
        public void OnApplicationSubmitted(object sender, ApplicationSubmittedEventArgs args)
        {
            _jobAdsHandler.OnApplicationSubmitted(args.ApplicationId);
        }
    }
}
