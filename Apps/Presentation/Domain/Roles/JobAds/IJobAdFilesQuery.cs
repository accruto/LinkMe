using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Apps.Presentation.Domain.Roles.JobAds
{
    public interface IJobAdFilesQuery
    {
        JobAdFile GetJobAdFile(JobAd jobAd);
    }
}
