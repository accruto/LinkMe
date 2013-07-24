using System;
using LinkMe.Apps.Agents.Reports;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails.EmployerClientEmails
{
    public class EmployerReportEmail
        : EmployerClientEmail
    {
        private readonly Report _report;
        private readonly IOrganisation _organisation;
        private readonly DateTime _startDate, _endDate;
        internal const string DateFormat = "dd-MMM-yy";

        public EmployerReportEmail(ICommunicationUser to, ICommunicationUser accountManager, Report report, IOrganisation organisation, DateTime startDate, DateTime endDate)
            : base(to, accountManager)
        {
            if (report == null)
                throw new ArgumentNullException("report");
            if (organisation == null)
                throw new ArgumentNullException("organisation");

            _report = report;
            _organisation = organisation;
            _startDate = startDate;
            _endDate = endDate;
        }

        public Report Report
        {
            get { return _report; }
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);

            properties.Add("toName", To.FirstName);
            properties.Add("orgName", _organisation.FullName);
            properties.Add("reportPeriod", GetReportPeriodDescription(_startDate, _endDate));
        }

        private static string GetReportPeriodDescription(DateTime startDate, DateTime endDate)
        {
            // If the report is for a calendar month (which it currently always is) then just write the month.

            if (startDate.Day == 1 && endDate == startDate.AddMonths(1).AddDays(-1))
                return "the month of " + startDate.ToString("MMMM yyyy");

            return startDate.ToString(DateFormat) + " - " + endDate.ToString(DateFormat);
        }
    }
}