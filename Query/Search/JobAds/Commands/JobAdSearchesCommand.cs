using System;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Query.Search.JobAds.Commands
{
    public class JobAdSearchesCommand
        : IJobAdSearchesCommand
    {
        private const int MaxExecutionResults = 10;

        private readonly IJobAdsRepository _repository;

        public JobAdSearchesCommand(IJobAdsRepository repository)
        {
            _repository = repository;
        }

        void IJobAdSearchesCommand.CreateJobAdSearch(Guid creatorId, JobAdSearch search)
        {
            search.OwnerId = creatorId;
            if (search.Criteria != null)
                search.Criteria.Prepare();
            search.Name = search.Name.NullIfEmpty();
            search.Prepare();
            search.Validate();
            _repository.CreateJobAdSearch(search);
        }

        void IJobAdSearchesCommand.DeleteJobAdSearch(Guid deleterId, JobAdSearch search)
        {
            DeleteJobAdSearch(deleterId, search);
        }

        void IJobAdSearchesCommand.DeleteJobAdSearch(Guid deleterId, Guid id)
        {
            var search = _repository.GetJobAdSearch(id);
            if (search != null)
                DeleteJobAdSearch(deleterId, search);
        }

        void IJobAdSearchesCommand.UpdateJobAdSearch(Guid updaterId, JobAdSearch search)
        {
            if (!CanAccessSearch(updaterId, search))
                throw new JobAdSearchesPermissionsException(updaterId, search.Id);

            if (search.Criteria != null)
                search.Criteria.Prepare();
            search.Name = search.Name.NullIfEmpty();
            search.Validate();
            _repository.UpdateJobAdSearch(search);
        }

        void IJobAdSearchesCommand.CreateJobAdSearchExecution(JobAdSearchExecution execution)
        {
            execution.Prepare();
            execution.Validate();
            _repository.CreateJobAdSearchExecution(execution, MaxExecutionResults);
        }

        private void DeleteJobAdSearch(Guid deleterId, JobAdSearch search)
        {
            if (!CanAccessSearch(deleterId, search))
                throw new JobAdSearchesPermissionsException(deleterId, search.Id);

            _repository.DeleteJobAdSearch(search.Id);
        }

        private static bool CanAccessSearch(Guid userId, JobAdSearch search)
        {
            return userId == search.OwnerId;
        }
    }
}