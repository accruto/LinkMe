using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Employers.JobAds.Queries;

namespace LinkMe.Domain.Users.Test.Employers.JobAds.Queries
{
    public class MockJobAdProcessingQuery
        : IJobAdProcessingQuery
    {
        JobAdProcessing IJobAdProcessingQuery.GetJobAdProcessing(IJobAd jobAd)
        {
            return jobAd.Integration == null || string.IsNullOrEmpty(jobAd.Integration.ExternalApplyUrl)
                ? JobAdProcessing.ManagedInternally
                : JobAdProcessing.ManagedExternally;
        }
    }
}
