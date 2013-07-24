using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Domain.Users.Employers.JobAds.Queries
{
    public interface IJobAdProcessingQuery
    {
        JobAdProcessing GetJobAdProcessing(IJobAd jobAd);
    }
}
