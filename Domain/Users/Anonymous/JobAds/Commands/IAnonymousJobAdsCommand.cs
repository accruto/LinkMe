using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Anonymous.JobAds.Commands
{
    public interface IAnonymousJobAdsCommand
    {
        void CreateJobAd(AnonymousUser user, JobAd jobAd);
        void UpdateJobAd(AnonymousUser user, JobAd jobAd);
    }
}
