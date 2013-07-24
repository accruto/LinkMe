using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Query.Reports.Accounts.Queries;
using LinkMe.Query.Reports.Roles.Candidates.Queries;

namespace LinkMe.TaskRunner.Tasks.Communications
{
    public class EmailEmployerUsageTask : CommunicationsTask
    {
        private const int RecruiterPeriod = 14;     // days
        private const int EmployerPeriod = 2;       // months

        private static readonly EventSource EventSource = new EventSource(typeof(EmailEmployerUsageTask));
        private readonly IResumeReportsQuery _resumeReportsQuery;
        private readonly IAccountReportsQuery _accountReportsQuery;
        private readonly IEmployersQuery _employersQuery;

        public EmailEmployerUsageTask(IEmailsCommand emailsCommand, IResumeReportsQuery resumeReportsQuery, IAccountReportsQuery accountReportsQuery, IEmployersQuery employersQuery)
            : base(EventSource, emailsCommand)
        {
            _resumeReportsQuery = resumeReportsQuery;
            _accountReportsQuery = accountReportsQuery;
            _employersQuery = employersQuery;
        }

        public override void ExecuteTask()
        {
            const string method = "ExecuteTask";

            try
            {
                var end = DateTime.Now.Date;

                // Send to employers.

                var start = end.AddMonths(-1 * EmployerPeriod);
                var ids = _accountReportsQuery.GetLastLogIns(UserType.Employer, new DayRange(start));
                var employers = _employersQuery.GetEmployers(ids);
                Execute(start, end, employers, EmployerSubRole.Employer);

                // Send to recruiters.

                start = end.AddDays(-1 * RecruiterPeriod);
                ids = _accountReportsQuery.GetLastLogIns(UserType.Employer, new DayRange(start));
                employers = _employersQuery.GetEmployers(ids);
                Execute(start, end, employers, EmployerSubRole.Recruiter);
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.Error, method, "Failed to send usage reminder emails.", ex, new StandardErrorHandler());
            }
        }

        private void Execute(DateTime start, DateTime end, IList<Employer> employers, EmployerSubRole subRole)
        {
            const string method = "Execute";

            // Grab the number of new candidates.

            var joinedUserIds = _accountReportsQuery.GetNewUserIds(UserType.Member, new DateTimeRange(start, end));
            var updatedResumeCandidateIds = _resumeReportsQuery.GetUpdatedResumeCandidateIds(new DateTimeRange(start, end));
            var newCandidates = joinedUserIds.Union(updatedResumeCandidateIds).Count();

            // Only send emails if in fact there are new candidates.

            if (newCandidates > 0)
            {
                employers = (from e in employers where e.SubRole == subRole select e).ToList();

                EventSource.Raise(Event.Information, method, string.Format("Sending usage reminder emails to {0} {1}...", employers.Count, subRole == EmployerSubRole.Employer ? "employers" : "recruiters"));

                foreach (var employer in employers.OrderBy(e => e.EmailAddress.Address))
                {
                    try
                    {
                        // Only send an email if this employer has not already received one.

                        _emailsCommand.TrySend(new EmployerUsageEmail(employer, newCandidates), DateTime.MinValue);
                    }
                    catch (Exception ex)
                    {
                        EventSource.Raise(Event.Error, method, string.Format("Failed to send a usage reminder email to '{0}'", employer), ex, new StandardErrorHandler());
                    }
                }

                EventSource.Raise(Event.Information, method, string.Format("Finished sending {0} usage reminder emails to {1}.", employers.Count, subRole == EmployerSubRole.Employer ? "employers" : "recruiters"));
            }

            return;
        }
    }
}
