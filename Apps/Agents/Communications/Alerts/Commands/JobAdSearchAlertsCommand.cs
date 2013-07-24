using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Query.Search.JobAds.Queries;

namespace LinkMe.Apps.Agents.Communications.Alerts.Commands
{
    public class JobAdSearchAlertsCommand
        : IJobAdSearchAlertsCommand
    {
        private readonly ISearchAlertsRepository _repository;
        private readonly IJobAdSearchesCommand _jobAdSearchesCommand;
        private readonly IJobAdSearchesQuery _jobAdSearchesQuery;

        public JobAdSearchAlertsCommand(ISearchAlertsRepository repository, IJobAdSearchesCommand jobAdSearchesCommand, IJobAdSearchesQuery jobAdSearchesQuery)
        {
            _repository = repository;
            _jobAdSearchesCommand = jobAdSearchesCommand;
            _jobAdSearchesQuery = jobAdSearchesQuery;
        }

        void IJobAdSearchAlertsCommand.CreateJobAdSearch(Guid creatorId, JobAdSearch search)
        {
            _jobAdSearchesCommand.CreateJobAdSearch(creatorId, search);
        }

        void IJobAdSearchAlertsCommand.UpdateJobAdSearch(Guid updaterId, JobAdSearch search)
        {
            _jobAdSearchesCommand.UpdateJobAdSearch(updaterId, search);
        }

        void IJobAdSearchAlertsCommand.CreateJobAdSearchAlert(Guid creatorId, Guid searchId, DateTime lastRunTime)
        {
            CreateJobAdSearchAlert(searchId, lastRunTime);
        }

        void IJobAdSearchAlertsCommand.CreateJobAdSearchAlert(Guid creatorId, JobAdSearch search, DateTime lastRunTime)
        {
            // Create the search first.

            _jobAdSearchesCommand.CreateJobAdSearch(creatorId, search);

            // Create the alert.

            CreateJobAdSearchAlert(search.Id, lastRunTime);
        }

        void IJobAdSearchAlertsCommand.DeleteJobAdSearchAlert(Guid deleterId, Guid searchId)
        {
            var search = _jobAdSearchesQuery.GetJobAdSearch(searchId);
            if (search == null)
                return;

            // Delete.

            _repository.DeleteJobAdSearchAlert(searchId);
        }

        void IJobAdSearchAlertsCommand.DeleteJobAdSearch(Guid deleterId, Guid searchId)
        {
            // Delete the alert and then the search.

            _repository.DeleteJobAdSearchAlert(searchId);
            _jobAdSearchesCommand.DeleteJobAdSearch(deleterId, searchId);
        }

        void IJobAdSearchAlertsCommand.UpdateLastRunTime(Guid searchId, DateTime lastRunTime)
        {
            _repository.UpdateJobAdSearchLastRunTime(searchId, lastRunTime);
        }

        private void CreateJobAdSearchAlert(Guid searchId, DateTime lastRunTime)
        {
            var alert = new JobAdSearchAlert { JobAdSearchId = searchId };
            alert.Prepare();
            alert.Validate();
            _repository.CreateJobAdSearchAlert(alert);

            // Set the last run time.

            _repository.UpdateJobAdSearchLastRunTime(searchId, lastRunTime);
        }
    }
}