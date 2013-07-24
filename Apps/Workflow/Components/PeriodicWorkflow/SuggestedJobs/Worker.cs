using System;
using System.Linq;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Domain.Accounts.Queries;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.Search.JobAds.Commands;

namespace LinkMe.Workflow.Components.PeriodicWorkflow.SuggestedJobs
{
    public class Worker
        : IWorker
    {
        #region Private Fields

        private static readonly EventSource<Worker> Logger = new EventSource<Worker>();
 
        private readonly ICandidatesWorkflowCommand _workflowCommand;
        private readonly IExecuteJobAdSearchCommand _jobAdSearchCommand;
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IEmailsCommand _emailsCommand;
        private readonly IMembersQuery _membersQuery;
        private readonly IUserAccountsQuery _userAccountsQuery;

        #endregion

        #region Optional Dependency Injection Properties

        public int MaxResults { private get; set; }

        #endregion

        public Worker(ICandidatesWorkflowCommand workflowCommand, IExecuteJobAdSearchCommand searchCommand, IJobAdsQuery jobAdsQuery, IEmailsCommand emailsCommand, IMembersQuery membersQuery, IUserAccountsQuery userAccountsQuery)
        {
            _workflowCommand = workflowCommand;
            _jobAdSearchCommand = searchCommand;
            _jobAdsQuery = jobAdsQuery;
            _emailsCommand = emailsCommand;
            _membersQuery = membersQuery;
            _userAccountsQuery = userAccountsQuery;

            MaxResults = 10; // default
        }

        #region Implementation of IWorker

        public Guid? GetWorkflowId(Guid userId)
        {
            return _workflowCommand.GetSuggestedJobsWorkflowId(userId);
        }

        public void AttachWorkflow(Guid userId, Guid workflowId)
        {
            _workflowCommand.AddSuggestedJobsWorkflow(userId, workflowId);
        }

        public void DetachWorkflow(Guid userId)
        {
            _workflowCommand.DeleteSuggestedJobsWorkflow(userId);
        }

        public void Run(Guid userId, DateTime lastRunTime)
        {
            const string method = "Run";

            // The user has to be active to receive this email.

            if (!_userAccountsQuery.IsActive(userId))
            {
                #region Log

                Logger.Raise(Event.Trace, method, "The user is not active and therefore won't get any suggested jobs.",
                    Event.Arg("userId", userId));

                #endregion
                return;
            }

            var member = _membersQuery.GetMember(userId);

            var execution = _jobAdSearchCommand.SearchSuggested(member, DateTime.Now - lastRunTime, new Range(0, MaxResults));

            if (execution.Results.JobAdIds.Count == 0)
            {
                #region Log

                Logger.Raise(Event.Trace, method, "No new suggested jobs were found.", Event.Arg("userId", userId));

                #endregion
                return;
            }

            var jobAdIds = execution.Results.JobAdIds.Take(MaxResults).ToList();
            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(jobAdIds).ToDictionary(j => j.Id, j => j);

            var emailJobAds = from i in jobAdIds
                              where jobAds.ContainsKey(i)
                              select jobAds[i];

            var to = _membersQuery.GetMember(userId);
            var email = SuggestedJobsEmail.Create(to, emailJobAds, execution.Results.JobAdIds.Count);

            var ok = _emailsCommand.TrySend(email);

            #region Log

            if (ok)
                Logger.Raise(Event.Trace, method, "SuggestedJobsEmail sent.", Event.Arg("userId", userId));
            else
                Logger.Raise(Event.Warning, method, "Unable to send SuggestedJobsEmail.",
                    Event.Arg("userId", userId));

            #endregion
        }

        #endregion
    }
}
