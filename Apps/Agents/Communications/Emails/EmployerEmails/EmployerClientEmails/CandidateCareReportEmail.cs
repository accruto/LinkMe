using System;
using LinkMe.Apps.Agents.Reports;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails.EmployerClientEmails
{
    public class CandidateCareReportEmail
        : EmployerReportEmail
    {
        private readonly string _referralCount;

        public CandidateCareReportEmail(ICommunicationUser to, ICommunicationUser accountManager, Report report, IOrganisation organisation, DateTime startDate, DateTime endDate, string referralCount)
            : base(to, accountManager, report, organisation, startDate, endDate)
        {
            _referralCount = referralCount;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("referralCount", _referralCount);
        }
    }
}