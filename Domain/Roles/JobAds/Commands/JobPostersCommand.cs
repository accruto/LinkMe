using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.JobAds.Commands
{
    public class JobPostersCommand
        : IJobPostersCommand
    {
        private readonly IJobAdsRepository _repository;

        public JobPostersCommand(IJobAdsRepository repository)
        {
            _repository = repository;
        }

        void IJobPostersCommand.CreateJobPoster(JobPoster poster)
        {
            poster.Prepare();
            poster.Validate();
            _repository.CreateJobPoster(poster);
        }

        void IJobPostersCommand.UpdateJobPoster(JobPoster poster)
        {
            poster.Validate();
            _repository.UpdateJobPoster(poster);
        }

        JobPoster IJobPostersCommand.GetJobPoster(Guid id)
        {
            return _repository.GetJobPoster(id);
        }
    }
}