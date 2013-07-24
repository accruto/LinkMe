using System;
using System.Collections.Generic;
using System.Web;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.MemberAlerts;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Apps.Presentation.Domain.Search;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Presentation.Query.Search.JobAds;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Anonymous.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Query.Search.JobAds.Queries;

namespace LinkMe.TaskRunner.Tasks.Communications
{
    public class EmailJobSearchAlertsTask
        : CommunicationsTask
    {
        private static readonly EventSource EventSource = new EventSource<EmailJobSearchAlertsTask>();

        private const int MaximumResults = 20;
        private const string StartHighlightTag = "<span style=\"background-color: #ffff99\">";
        private const string EndHighlightTag = "</span>";

        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand;
        private readonly IJobAdSearchAlertsCommand _jobAdSearchAlertsCommand;
        private readonly IJobAdSearchAlertsQuery _jobAdSearchAlertsQuery;
        private readonly IJobAdSearchesQuery _jobAdSearchesQuery;
        private readonly IMembersQuery _membersQuery;
        private readonly IAnonymousUsersQuery _anonymousUsersQuery;

        public EmailJobSearchAlertsTask(IEmailsCommand emailsCommand, IJobAdsQuery jobAdsQuery, IExecuteJobAdSearchCommand executeJobAdSearchCommand, IJobAdSearchAlertsCommand jobAdSearchAlertsCommand, IJobAdSearchAlertsQuery jobAdSearchAlertsQuery, IJobAdSearchesQuery jobAdSearchesQuery, IMembersQuery membersQuery, IAnonymousUsersQuery anonymousUsersQuery)
            : base(EventSource, emailsCommand)
        {
            _jobAdsQuery = jobAdsQuery;
            _executeJobAdSearchCommand = executeJobAdSearchCommand;
            _jobAdSearchAlertsCommand = jobAdSearchAlertsCommand;
            _jobAdSearchAlertsQuery = jobAdSearchAlertsQuery;
            _jobAdSearchesQuery = jobAdSearchesQuery;
            _membersQuery = membersQuery;
            _anonymousUsersQuery = anonymousUsersQuery;
        }

        public override void ExecuteTask()
        {
            const string method = "ExecuteTask";

            // Get all saved job search alerts.

            var alerts = _jobAdSearchAlertsQuery.GetJobAdSearchAlerts();
            EventSource.Raise(Event.Information, method, string.Format("Processing {0} saved job searches...", alerts.Count));

            var errors = 0;
            for (var index = 0; index < alerts.Count; index++)
            {
                // Primitive progress indicator.

                if (index % 1000 == 0 && index > 0)
                    EventSource.Raise(Event.Information, method, string.Format("Processed {0} out of {1} job search alerts.", index, alerts.Count));

                var alert = alerts[index];
                var search = GetJobAdSearch(alert.JobAdSearchId);
                if (search == null)
                {
                    ++errors;
                }
                else
                {
                    try
                    {
                        var user = GetUser(search.OwnerId);
                        if (user != null)
                        {
                            // Clone the original criteria before the search is done, so the user doesn't get a huge list of synonyms inserted.

                            var originalCriteria = search.Criteria.Clone();
                            var execution = Search(search, alert);

                            EventSource.Raise(Event.Trace, method, string.Format("Job alert for user '{0}' returned {1} results. Criteria: {2}", search.OwnerId, execution == null || execution.Results == null ? 0 : execution.Results.JobAdIds.Count, search.Criteria));

                            SendAlert(user, search.Id, execution, search.Criteria, originalCriteria);
                        }
                    }
                    catch (Exception ex)
                    {
                        EventSource.Raise(Event.Error, method, string.Format("Failed to execute search or send email alert for saved job" + " search {0} (criteria set ID {1}).", search.Id.ToString("B"), search.Criteria.Id.ToString("B")), ex, new StandardErrorHandler());
                        errors++;
                    }
                }
            }

            ReportResults(alerts.Count, errors);
        }

        public IList<JobSearchAlertEmailResult> CreateEmailResults(JobAdSearchResults searchResults, JobAdSearchCriteria criteria, int maximumResults)
        {
            var emailResults = new List<JobSearchAlertEmailResult>();
            if (searchResults.JobAdIds.Count > 0)
            {
                var highlighter = new JobSearchHighlighter(criteria, StartHighlightTag, EndHighlightTag);
                AppendResults(emailResults, searchResults, maximumResults, highlighter, criteria.KeywordsExpression != null);
            }
            return emailResults;
        }

        private void AppendResults(ICollection<JobSearchAlertEmailResult> emailResults, JobAdSearchResults searchResults, int maximumResults, JobSearchHighlighter highlighter, bool haveKeywords)
        {
            foreach (var jobAdId in searchResults.JobAdIds)
            {
                // Get the job ad for the result.

                var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
                if (jobAd != null)
                {
                    AppendResult(emailResults, jobAd, highlighter, haveKeywords);
                    if (emailResults.Count == maximumResults)
                        return;
                }
            }
        }

        private static void AppendResult(ICollection<JobSearchAlertEmailResult> emailResults, JobAd jobAd, JobSearchHighlighter highlighter, bool haveKeywords)
        {
            var emailResult = new JobSearchAlertEmailResult
            {
                JobAdId = jobAd.Id.ToString("n"),
                Title = highlighter.HighlightTitle(jobAd.Title),
                Location = jobAd.GetLocationDisplayText()
            };

            if (jobAd.Description.Salary != null)
                emailResult.Salary = jobAd.Description.Salary.GetJobAdDisplayText();

            emailResult.PostedAge = jobAd.GetPostedDisplayText();
            emailResult.PostedDate = jobAd.CreatedTime.ToShortDateString();
            emailResult.JobType = jobAd.Description.JobTypes.GetDisplayText(", ", false, false);

            if (jobAd.Description.Industries != null)
                emailResult.Industry = jobAd.Description.Industries.GetCriteriaIndustriesDisplayText();

            Summarize(jobAd, highlighter, haveKeywords, out emailResult.Digest, out emailResult.Fragments);

            emailResults.Add(emailResult);
        }

        private static void Summarize(JobAd jobAd, JobSearchHighlighter highlighter, bool haveKeywords, out string digestHtml, out string fragmentsHtml)
        {
            if (haveKeywords)
            {
                // Show highlighted short summary + best highlighted content fragments.

                string digestText;
                string bodyText;
                highlighter.Summarize(jobAd.Description.Summary, jobAd.Description.BulletPoints, jobAd.Description.Content, false, out digestText, out bodyText);

                digestHtml = highlighter.HighlightContent(digestText, false);
                fragmentsHtml = highlighter.GetBestContent(bodyText);
            }
            else
            {
                // Show long summary without highlighting.

                string digestText;
                string bodyText;
                highlighter.Summarize(jobAd.Description.Summary, jobAd.Description.BulletPoints, jobAd.Description.Content, true, out digestText, out bodyText);

                digestHtml = HttpUtility.HtmlEncode(digestText).Replace("\x2022", "&#x2022;");
                fragmentsHtml = string.Empty;
            }
        }

        private ICommunicationUser GetUser(Guid ownerId)
        {
            // Look for member first.

            var member = _membersQuery.GetMember(ownerId);
            if (member != null)
                return member.IsEnabled && member.IsActivated && member.GetBestEmailAddress().IsVerified ? member : null;

            // Anonymous user.

            return _anonymousUsersQuery.GetContact(ownerId);
        }

        private JobAdSearch GetJobAdSearch(Guid id)
        {
            const string method = "GetJobAdSearch";

            var search = _jobAdSearchesQuery.GetJobAdSearch(id);
            if (search == null)
            {
                EventSource.Raise(Event.Error, method, "JobAdSearch '" + id + "' cannot be found.", Event.Arg("Id", id));
                return null;
            }

            if (search.Criteria == null)
            {
                // Handle failures in loading search criteria.

                EventSource.Raise(Event.Error, method, "Criteria for JobAdSearch '" + id + "' not loaded.");
                return null;
            }

            return search;
        }

        private static void ReportResults(int total, int errors)
        {
            const string method = "ReportResults";
            
            if (errors == 0)
            {
                EventSource.Raise(Event.Information, method, string.Format("{0} saved job searches were processed successfully.", total));
            }
            else
            {
                var message = string.Format("{0} saved job searches failed out of a total of {1}.",
                    errors, total);
                EventSource.Raise(Event.Information, method, string.Format(message));
            }
        }

        private JobAdSearchExecution Search(JobAdSearch search, JobAdSearchAlert alert)
        {
            // Set the criteria recency to ensure only recently added jobs are included in the alert results.

            var criteria = search.Criteria.Clone();
            criteria.Recency = DateTime.Now - alert.LastRunTime;

            // Run the search as the owner.

            var member = new Member { Id = search.OwnerId };

            // Search.

            var execution = new JobAdSearchExecution
            {
                SearcherId = search.OwnerId,
                Criteria = search.Criteria,
                Context = "Alert",
                Results = _executeJobAdSearchCommand.Search(member, criteria, new Range(0, MaximumResults)).Results,
            };

            // Update the last run time for next time.

            _jobAdSearchAlertsCommand.UpdateLastRunTime(search.Id, DateTime.Now);
            return execution;
        }

        private void SendAlert(ICommunicationUser user, Guid savedJobSearchId, JobAdSearchExecution execution, JobAdSearchCriteria criteria, JobAdSearchCriteria originalCriteria)
        {
            if (execution == null || execution.Results == null || execution.Results.JobAdIds.Count == 0)
                return;

            var emailResults = CreateEmailResults(execution.Results, criteria, MaximumResults);
            if (emailResults.Count > 0)
                _emailsCommand.TrySend(new JobSearchAlertEmail(user, execution.Results.JobAdIds.Count, emailResults, originalCriteria, savedJobSearchId));
        }
    }
}
