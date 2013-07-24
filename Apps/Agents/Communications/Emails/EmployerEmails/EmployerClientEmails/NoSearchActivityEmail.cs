using System;
using LinkMe.Apps.Agents.Reports;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails.EmployerClientEmails
{
    public class NoSearchActivityEmail
        : EmployerReportEmail
    {
        // Hardcoded for now, but ideally this should be worked out from the stats.
        private const int WeeklyMemberGrowth = 3000;
        public const string MemberCountFormatString = "N0";

        private readonly int _totalMemberCount;

        public NoSearchActivityEmail(ICommunicationUser to, ICommunicationUser accountManager, Report report, IOrganisation organisation, DateTime startDate, DateTime endDate, int totalMemberCount)
            : base(to, accountManager, report, organisation, startDate, endDate)
        {
            _totalMemberCount = totalMemberCount;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("memberCount", _totalMemberCount.ToString(MemberCountFormatString));
            properties.Add("weeklyMemberGrowth", WeeklyMemberGrowth.ToString(MemberCountFormatString));
        }
    }
}