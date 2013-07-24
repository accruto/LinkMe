using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Reports;
using LinkMe.Apps.Agents.Reports.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Framework.Content.Templates;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Agents.Communications.Emails.AdministratorEmails
{
    /// <summary>
    /// The same report as CustomerReportEmail, but sent to the account manager.
    /// </summary>
    public class AccountManagerReportEmail
        : AdministratorEmail
    {
        private readonly Report _report;
        private readonly VerifiedOrganisation _organisation;
        private readonly ICommunicationUser _accountManager;
        private readonly List<string> _noActivity = new List<string>();
        private readonly List<string> _customHtmlSnippets = new List<string>();
        private readonly DateTime _startDate, _endDate;
        internal const string DateFormat = "dd-MMM-yy";

        public AccountManagerReportEmail(ICommunicationUser accountManager, Report report, VerifiedOrganisation organisation, DateTime startDate, DateTime endDate)
            : base(accountManager)
        {
            if (report == null)
                throw new ArgumentNullException("report");
            if (organisation == null)
                throw new ArgumentNullException("organisation");
            if (accountManager == null)
                throw new ArgumentNullException("accountManager");

            _report = report;
            _organisation = organisation;
            _accountManager = accountManager;
            _startDate = startDate;
            _endDate = endDate;
        }

        public Report Report
        {
            get { return _report; }
        }

        public VerifiedOrganisation VerifiedOrganisation
        {
            get { return _organisation; }
        }

        public ICommunicationUser AccountManager
        {
            get { return _accountManager; }
        }

        public List<string> NoActivity
        {
            get { return _noActivity; }
        }

        public List<string> CustomHtmlSnippets
        {
            get { return _customHtmlSnippets; }
        }

        public void AddNoActivityReport(EmployerReport report)
        {
            if (report == null)
                throw new ArgumentNullException("report");

            _noActivity.Add(report.Name);
        }

        public void AddCustomHtml(string customHtml)
        {
            _customHtmlSnippets.Add(customHtml);
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);

            properties.Add("OrgName", _organisation.FullName);
            properties.Add("ReportPeriod", GetReportPeriodDescription(_startDate, _endDate));
            properties.Add("NoActivity", GetNoActivityDisplayString());
            properties.Add("HaveAttachments", !GetAttachments().IsNullOrEmpty());
            properties.Add("CustomHtmlSnippets", _customHtmlSnippets.Count == 0
                ? string.Empty
                : "<p>" + string.Join("</p><p>", _customHtmlSnippets.ToArray()) + "</p>");
        }

        public override bool RequiresActivation
        {
            get { return false; }
        }

        private string GetNoActivityDisplayString()
        {
            if (_noActivity.Count == 0)
                return null;

            _noActivity.Sort(); // Make sure the order is consistent.

            string text = "No " + _noActivity[0].ToLower();

            for (int i = 1; i < _noActivity.Count - 1; i++)
            {
                text += ", no " + _noActivity[i].ToLower();
            }

            if (_noActivity.Count > 1)
            {
                text += " and no " + _noActivity[_noActivity.Count - 1].ToLower();
            }

            return text;
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