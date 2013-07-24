using System;

namespace LinkMe.Domain.Roles.JobAds.Queries
{
    public class JobPostersQuery
        : IJobPostersQuery
    {
        private readonly IJobAdsRepository _repository;

        public JobPostersQuery(IJobAdsRepository repository)
        {
            _repository = repository;
        }

        JobPoster IJobPostersQuery.GetJobPoster(Guid id)
        {
            return _repository.GetJobPoster(id);
        }
    }
}
