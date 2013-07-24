using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Presentation.Domain.Users.Members.Search;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Query.Search.Members.Queries;

namespace LinkMe.TaskRunner.Tasks.Communications
{
    public class EmailSuggestedCandidatesTask
        : CommunicationsTask
    {
        private const int MaxResultsPerJobAd = 3;
        private const int MaxSearchResults = 99;
        private const int MaxJobAdsPerEmail = 10;

        private static readonly EventSource EventSource = new EventSource(typeof(EmailSuggestedCandidatesTask));
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly ISuggestedMembersQuery _suggestedMembersQuery;
        private readonly IEmployersQuery _employersQuery;
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery;
        private readonly IEmployerCreditsQuery _employerCreditsQuery;
        private readonly IMemberSearchesCommand _memberSearchesCommand;
        private readonly IExecuteMemberSearchCommand _executeMemberSearchCommand;

        private class SuggestedCandidatesResults
        {
            public JobAd JobAd { get; set; }
            public MemberSearchExecution Execution { get; set; }
        }

        public EmailSuggestedCandidatesTask(IEmailsCommand emailsCommand, IJobAdsQuery jobAdsQuery, ISuggestedMembersQuery suggestedMembersQuery, IEmployersQuery employersQuery, IEmployerMemberViewsQuery employerMemberViewsQuery, IEmployerCreditsQuery employerCreditsQuery, IMemberSearchesCommand memberSearchesCommand, IExecuteMemberSearchCommand executeMemberSearchCommand)
            : base(EventSource, emailsCommand)
        {
            _jobAdsQuery = jobAdsQuery;
            _suggestedMembersQuery = suggestedMembersQuery;
            _employersQuery = employersQuery;
            _employerMemberViewsQuery = employerMemberViewsQuery;
            _employerCreditsQuery = employerCreditsQuery;
            _memberSearchesCommand = memberSearchesCommand;
            _executeMemberSearchCommand = executeMemberSearchCommand;
        }

        public override void ExecuteTask()
        {
            const string method = "ExecuteTask";

            // Get the job ads to search for.

            var jobAds = GetJobsAds();
            if (jobAds.Count == 0)
            {
                EventSource.Raise(Event.Information, method, string.Format("There are no new job ads for which suggested candidates need to be emailed."));
                return;
            }

            EventSource.Raise(Event.Information, method, string.Format("Sending suggested candidates for {0} job ads to {1} contact email addresses.", jobAds.Sum(j => j.Value.Count), jobAds.Count));

            var emailsSent = 0;
            var failedEmails = 0;
            var failedSearches = 0;

            foreach (var contactJobAds in jobAds)
            {
                var results = new List<SuggestedCandidatesResults>();

                foreach (var jobAd in contactJobAds.Value)
                {
                    EventSource.Raise(Event.Trace, method, string.Format("Performing search for {0:b}.", jobAd.Id));

                    try
                    {
                        results.Add(new SuggestedCandidatesResults { JobAd = jobAd, Execution = Execute(jobAd) });
                    }
                    catch (Exception ex)
                    {
                        EventSource.Raise(Event.Error, method, string.Format("Cannot perform search for {0:b}.", jobAd.Id), ex);
                        failedSearches++;
                    }
                }

                if (SendEmail(contactJobAds.Key, results))
                    emailsSent++;
                else
                    failedEmails++;
            }

            EventSource.Raise(Event.Information, method, string.Format("{0} suggested candidates emails were sent, {1} emails failed to be sent, {2} searches failed.", emailsSent, failedEmails, failedSearches));
        }

        private IDictionary<string, IList<JobAd>> GetJobsAds()
        {
            // Get all open job ads that were created yesterday.

            var jobAdIds = _jobAdsQuery.GetOpenJobAdIds(new DateTimeRange(DateTime.Today.AddDays(-1), DateTime.Today));

            // Group the job ads by contact email address.

            var jobAdsByEmailAddress = new Dictionary<string, IList<JobAd>>(StringComparer.CurrentCultureIgnoreCase);

            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(jobAdIds);
            foreach (var jobAd in jobAds)
            {
                // Use the contact details email address.

                if (jobAd.ContactDetails == null)
                    continue;
                var emailAddress = jobAd.ContactDetails.EmailAddress;
                if (string.IsNullOrEmpty(emailAddress))
                    continue;

                IList<JobAd> list;
                if (!jobAdsByEmailAddress.TryGetValue(emailAddress, out list))
                {
                    list = new List<JobAd>();
                    jobAdsByEmailAddress.Add(emailAddress, list);
                }

                list.Add(jobAd);
            }

            return jobAdsByEmailAddress;
        }

        private bool SendEmail(string emailAddress, ICollection<SuggestedCandidatesResults> results)
        {
            const string method = "SendEmail";

            if (results.Count == 0)
                return false;

            // Find the registered employer for this address, if any.

            EventSource.Raise(Event.Trace, method, string.Format("Processing search results for '{0}'.", emailAddress));

            var employer = GetEmployer(emailAddress);

            // Only include searches that returned results and only up to the maximum. Put new jobs first.

            var totalJobs = 0;
            var totalCandidates = 0;
            var suggestedCandidates = new List<SuggestedCandidates>();
            foreach (var result in (from r in results where r.Execution.Results.TotalMatches > 0 orderby r.JobAd.CreatedTime descending select r).Take(MaxJobAdsPerEmail))
            {
                // Save the search for reporting purposes.

                _memberSearchesCommand.CreateMemberSearchExecution(result.Execution);

                // Process the search results.

                totalJobs++;
                var jobCandidates = result.Execution.Results.MemberIds.Count;
                totalCandidates += jobCandidates;

                suggestedCandidates.Add(new SuggestedCandidates { JobAd = result.JobAd, TotalCandidates = result.Execution.Results.MemberIds.Count, CandidateIds = result.Execution.Results.MemberIds.Take(MaxResultsPerJobAd).ToList() });
            }

            // Send the email if there are any results.

            if (totalCandidates == 0)
            {
                EventSource.Raise(Event.Trace, method, string.Format("No suggested candidates were found for any job ads for '{0}'.", emailAddress));
                return false;
            }

            var views = _employerMemberViewsQuery.GetEmployerMemberViews(employer, suggestedCandidates.SelectMany(e => e.CandidateIds).Distinct());
            _emailsCommand.TrySend(new SuggestedCandidatesEmail(emailAddress, employer, totalCandidates, totalJobs, suggestedCandidates, views));
            return true;
        }

        private Employer GetEmployer(string emailAddress)
        {
            var employers = _employersQuery.GetEmployers(emailAddress);
            if (employers.Count == 0)
                return null;
            if (employers.Count == 1)
                return employers[0];

            // There are multiple employers with this email address - choose one with unlimited contact credits, if possible.

            foreach (var employer in employers)
            {
                var allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ContactCredit>(employer);
                if (allocation.RemainingQuantity == null)
                    return employer;
            }

            // None of them have unlimited credits - just return the first one.

            return employers[0];
        }

        private MemberSearchExecution Execute(JobAd jobAd)
        {
            var criteria = _suggestedMembersQuery.GetCriteria(jobAd);

            var timer = Stopwatch.StartNew();
            var execution = _executeMemberSearchCommand.Search(null, criteria, new Range(0, MaxSearchResults));
            timer.Stop();

            return new MemberSearchExecution
            {
                Criteria = criteria,
                Context = MemberSearchContext.SuggestedCandidatesEmail,
                StartTime = DateTime.Now,
                Duration = timer.Elapsed,
                Results = execution.Results,
            };
        }
    }
}
