using System;

namespace LinkMe.Domain.Roles.JobAds.Commands
{
    public class JobAdExportCommand
        : IJobAdExportCommand
    {
        private readonly IJobAdsRepository _repository;

        public JobAdExportCommand(IJobAdsRepository repository)
        {
            _repository = repository;
        }

        void IJobAdExportCommand.CreateJobSearchId(Guid jobAdId, long vacancyId)
        {
            _repository.CreateJobSearchId(jobAdId, vacancyId);
        }

        long? IJobAdExportCommand.GetJobSearchId(Guid jobAdId)
        {
            return _repository.GetJobSearchId(jobAdId);
        }

        void IJobAdExportCommand.DeleteJobSearchId(Guid jobAdId)
        {
            _repository.DeleteJobSearchId(jobAdId);
        }
    }
}