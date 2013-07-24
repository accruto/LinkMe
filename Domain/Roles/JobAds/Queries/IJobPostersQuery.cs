using System;

namespace LinkMe.Domain.Roles.JobAds.Queries
{
    public interface IJobPostersQuery
    {
        JobPoster GetJobPoster(Guid id);
    }
}
