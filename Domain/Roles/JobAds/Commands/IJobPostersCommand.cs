using System;

namespace LinkMe.Domain.Roles.JobAds.Commands
{
    public interface IJobPostersCommand
    {
        void CreateJobPoster(JobPoster poster);
        void UpdateJobPoster(JobPoster poster);
        JobPoster GetJobPoster(Guid id);
    }
}