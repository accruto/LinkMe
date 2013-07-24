using System.Linq;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Query.Search.Members.Queries;

namespace LinkMe.TaskRunner.Tasks.Communications
{
    public class EmailResumeSearchAlertsTask
        : ResumeSearchAlertsTask
    {
        private readonly IEmailsCommand _emailsCommand;
        private readonly IIndustriesQuery _industriesQuery;
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery;

        public EmailResumeSearchAlertsTask(IExecuteMemberSearchCommand executeMemberSearchCommand, IMemberSearchesQuery memberSearchesQuery, IMemberSearchAlertsCommand memberSearchAlertsCommand, IMemberSearchAlertsQuery memberSearchAlertsQuery, IEmployersQuery employersQuery, IEmailsCommand emailsCommand, IIndustriesQuery industriesQuery, IEmployerMemberViewsQuery employerMemberViewsQuery)
            : base(executeMemberSearchCommand, memberSearchesQuery, memberSearchAlertsCommand, memberSearchAlertsQuery, employersQuery, AlertType.Email)
        {
            _emailsCommand = emailsCommand;
            _industriesQuery = industriesQuery;
            _employerMemberViewsQuery = employerMemberViewsQuery;
        }

        protected override void Alert(Employer employer, MemberSearch search, MemberSearchAlert alert, MemberSearchResults results)
        {
            var industries = search.Criteria.IndustryIds == null ? null : (from i in search.Criteria.IndustryIds select _industriesQuery.GetIndustry(i)).ToList();
            _emailsCommand.TrySend(new ResumeSearchAlertEmail(employer, search.Criteria, industries, results, _employerMemberViewsQuery.GetEmployerMemberViews(employer, results.MemberIds), alert.MemberSearchId));
        }
    }
}