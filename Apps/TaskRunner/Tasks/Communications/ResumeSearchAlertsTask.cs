using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Query.Search.Members.Queries;

namespace LinkMe.TaskRunner.Tasks.Communications
{
    public abstract class ResumeSearchAlertsTask
        : Task
    {
        private static readonly EventSource EventSource = new EventSource<ResumeSearchAlertsTask>();
        private readonly AlertType _alertType;
        private const int MaxResults = 1000;

        private readonly IExecuteMemberSearchCommand _executeMemberSearchCommand;
        private readonly IMemberSearchesQuery _memberSearchesQuery;
        private readonly IMemberSearchAlertsCommand _memberSearchAlertsCommand;
        private readonly IMemberSearchAlertsQuery _memberSearchAlertsQuery;
        private readonly IEmployersQuery _employersQuery;
        
        private int _alertsSent;

        protected ResumeSearchAlertsTask(IExecuteMemberSearchCommand executeMemberSearchCommand, IMemberSearchesQuery memberSearchesQuery, IMemberSearchAlertsCommand memberSearchAlertsCommand, IMemberSearchAlertsQuery memberSearchAlertsQuery, IEmployersQuery employersQuery, AlertType alertType)
            : base(EventSource)
        {
            _executeMemberSearchCommand = executeMemberSearchCommand;
            _memberSearchesQuery = memberSearchesQuery;
            _memberSearchAlertsCommand = memberSearchAlertsCommand;
            _memberSearchAlertsQuery = memberSearchAlertsQuery;
            _employersQuery = employersQuery;
            _alertType = alertType;
        }

        public override void ExecuteTask()
        {
            ExecuteTask(null);
        }

        public override void ExecuteTask(string[] args)
        {
            IList<MemberSearchAlert> savedResumeSearches;

            if (args == null || args.Length == 0)
            {
                // Get all saved resume search alerts.

                savedResumeSearches = _memberSearchAlertsQuery.GetMemberSearchAlerts(_alertType);
            }
            else if (args.Length == 1)
            {
                // Get all the saved searches with IDs specified in the argument (separated by commas).

                var idStrings = args[0].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var ids = idStrings.Select(i => new Guid(i));

                savedResumeSearches = ids.Select(i => _memberSearchAlertsQuery.GetMemberSearchAlert(i, _alertType)).ToList();

                var iNull = savedResumeSearches.IndexOf(null);
                if (iNull != -1)
                    throw new ArgumentException("There is no saved resume search with ID " + idStrings[iNull]);
            }
            else
            {
                throw new ArgumentException("This task expects at most 1 argument: a list of saved search IDs.");
            }

            ExecuteSearchAlerts(savedResumeSearches);
        }

        private void ExecuteSearchAlerts(IList<MemberSearchAlert> alerts)
        {
            const string method = "ExecuteSearchAlerts";
            EventSource.Raise(Event.Information, method, string.Format("Processing {0} saved resume searches...", alerts.Count));

            var errors = 0;
            for (var index = 0; index < alerts.Count; index++)
            {
                // Primitive progress indicator.

                if (index % 200 == 0 && index > 0)
                    EventSource.Raise(Event.Information, method, string.Format("Processed {0} out of {1} resume search alerts so far...", index, alerts.Count));

                // Get the search.

                var alert = alerts[index];

                try
                {
                    var search = _memberSearchesQuery.GetMemberSearch(alert.MemberSearchId);
                    if (search.Criteria == null)
                    {
                        // Handle failures in loading search criteria.

                        EventSource.Raise(Event.Error, method, string.Format("Search criteria was NULL for saved resume search {0}." + " See earlier log messages for details.", search.Id));
                        errors++;
                    }
                    else
                    {
                        // Only send if the owner is enabled.

                        var employer = _employersQuery.GetEmployer(search.OwnerId);
                        if (employer.IsEnabled)
                        {
                            // Get the criteria text before the search is done, so the user doesn't get a huge list of synonyms inserted.

                            var lastRunTime = alert.LastRunTime;
                            var results = GetResults(employer, search, alert);

                            EventSource.Raise(Event.Trace, method, string.Format("Resume alert for user {0:b} returned {1} results. Modified since: {2} Criteria: {3}", search.OwnerId, results == null ? "no" : results.MemberIds.Count.ToString(), lastRunTime, search.Criteria));

                            if (results != null && results.MemberIds.Count > 0)
                            {
                                Alert(employer, search, alert, results);
                                _alertsSent++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    EventSource.Raise(Event.Error, method, string.Format("Failed to execute search or send alert for resume search alert {0}.", alert.Id.ToString("B")), ex, new StandardErrorHandler());
                    errors++;
                }
            }

            ReportResults(alerts.Count, errors);
        }

        private static void ReportResults(int total, int errors)
        {
            const string method = "ReportResults";

            if (errors == 0)
            {
                EventSource.Raise(Event.Information, method, string.Format("{0} saved resume searches were processed successfully.", total));
            }
            else
            {
                var message = string.Format("{0} saved resume searches failed out of a total of {1}.", errors, total);
                EventSource.Raise(Event.Information, method, string.Format(message));
            }
        }

        private MemberSearchResults GetResults(IEmployer employer, MemberSearch savedResumeSearch, MemberSearchAlert alert)
        {
            // Clone the criteria before adjusted it for this search.

            var criteria = savedResumeSearch.Criteria.Clone();
            criteria.Recency = DateTime.Now - alert.LastRunTime;

            var execution = _executeMemberSearchCommand.Search(employer, criteria, new Range(0, MaxResults));
            _memberSearchAlertsCommand.UpdateLastRunTime(alert.MemberSearchId, DateTime.Now, _alertType);
            return execution.Results;
        }

        protected abstract void Alert(Employer employer, MemberSearch search, MemberSearchAlert alert, MemberSearchResults results);
    }
}