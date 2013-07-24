using System;
using System.Linq;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.EmployerUpdates;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Accounts.Queries;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.TaskRunner.Tasks.Communications
{
    public class EmailEmployerNewsletterTask
        : CommunicationsTask
    {
        private static readonly EventSource EventSource = new EventSource<EmailEmployerNewsletterTask>();

        private readonly IEmployersQuery _employersQuery;
        private readonly IUserAccountsQuery _userAccountsQuery;
        private readonly IAllocationsQuery _allocationsQuery;
        private readonly IRecruitersQuery _recruitersQuery;

        public EmailEmployerNewsletterTask(IEmailsCommand emailsCommand, IEmployersQuery employersQuery, IUserAccountsQuery userAccountsQuery, IAllocationsQuery allocationsQuery, IRecruitersQuery recruitersQuery)
            : base(EventSource, emailsCommand)
        {
            _employersQuery = employersQuery;
            _userAccountsQuery = userAccountsQuery;
            _allocationsQuery = allocationsQuery;
            _recruitersQuery = recruitersQuery;
        }

        public override void ExecuteTask()
        {
            ExecuteTask(new string[0]);
        }

        public override void ExecuteTask(string[] args)
        {
            const string method = "ExecuteTask";
            EventSource.Raise(Event.FlowEnter, method, "Entering task.", Event.Arg("args", args));

            var totalEmployers = 0;
            var totalSent = 0;

            try
            {
                // Get all enabled employers.

                var employerIds = _employersQuery.GetEmployerIds();
                employerIds = _userAccountsQuery.GetEnabledAccountIds(employerIds);
                totalEmployers = employerIds.Count;

                EventSource.Raise(Event.Flow, method, "Total enabled employers: " + totalEmployers, Event.Arg("totalEmployers", totalEmployers));

                // Send each one an email.

                foreach (var employerId in employerIds)
                    totalSent+= Send(employerId) ? 1 : 0;
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.NonCriticalError, method, "Failed to send all employer newsletter emails.", ex, new StandardErrorHandler());
            }
            finally
            {
                EventSource.Raise(Event.Flow, method, totalSent + " employer newsletter emails have been sent.", Event.Arg("totalEmployers", totalEmployers), Event.Arg("totalSent", totalSent));
            }
        }

        private bool Send(Guid employerId)
        {
            const string method = "Send";

            try
            {
                // Only send if there are allocations that expire in the future.

                var today = DateTime.Today;
                var allocations = from x in _allocationsQuery.GetAllocationsByOwnerId(_recruitersQuery.GetOrganisationHierarchyPath(employerId))
                                  from a in x.Value
                                  where a.ExpiryDate == null || a.ExpiryDate >= today
                                  select a;

                if (allocations.Count() > 0)
                {
                    var employer = _employersQuery.GetEmployer(employerId);
                    return _emailsCommand.TrySend(new EmployerNewsletterEmail(employer));
                }

                return false;
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.NonCriticalError, method, "Failed to send an employer newsletter to an member.", ex, new StandardErrorHandler(), Event.Arg("employerId", employerId));
                return false;
            }
        }
    }
}
