using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;

namespace LinkMe.Domain.Roles.Test.JobAds
{
    public static class JobAdTestExtensions
    {
        public static void PostJobAd(this IJobAdsCommand jobAdsCommand, JobAd jobAd)
        {
            // Creating the job ad puts it into the Draft state so open it as well.

            jobAdsCommand.CreateJobAd(jobAd);
            jobAdsCommand.OpenJobAd(jobAd);
        }
    }
}
