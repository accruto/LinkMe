using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.AdministratorEmails;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails.EmployerClientEmails;
using LinkMe.Apps.Agents.Reports;
using LinkMe.Apps.Agents.Reports.Employers;
using LinkMe.Apps.Agents.Reports.Employers.Commands;
using LinkMe.Apps.Agents.Reports.Employers.Queries;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Users.Administrators.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Files;
using LinkMe.Framework.Utility.Linq;
using LinkMe.Framework.Instrumentation;
using LinkMe.Query.Reports.Accounts.Queries;

namespace LinkMe.TaskRunner.Tasks.Communications
{
    public class EmailCustomerReportsTask
        : CommunicationsTask
    {
        private static readonly EventSource EventSource = new EventSource<EmailCustomerReportsTask>();

        private int _memberCount;

        private readonly IEmployerReportsQuery _employerReportsQuery;
        private readonly IEmployerReportsCommand _employerReportsCommand;
        private readonly IExecuteEmployerReportsCommand _executeEmployerReportsCommand;
        private readonly IOrganisationsQuery _organisationsQuery;
        private readonly IAdministratorsQuery _administratorsQuery;
        private readonly IAccountReportsQuery _accountReportsQuery;

        public EmailCustomerReportsTask(IEmailsCommand emailsCommand, IEmployerReportsQuery employerReportsQuery, IEmployerReportsCommand employerReportsCommand, IExecuteEmployerReportsCommand executeEmployerReportsCommand, IOrganisationsQuery organisationsQuery, IAdministratorsQuery administratorsQuery, IAccountReportsQuery accountReportsQuery)
            : base(EventSource, emailsCommand)
        {
            _employerReportsQuery = employerReportsQuery;
            _employerReportsCommand = employerReportsCommand;
            _executeEmployerReportsCommand = executeEmployerReportsCommand;
            _organisationsQuery = organisationsQuery;
            _administratorsQuery = administratorsQuery;
            _accountReportsQuery = accountReportsQuery;
        }

        public override void ExecuteTask()
        {
            const string method = "ExecuteTask";

            // Run all reports for the last calendar month.

            var now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
            var endDate = new DateTime(now.Year, now.Month, 1).AddDays(-1);

            var reportsToRun = (from r in _employerReportsQuery.GetReportsToRun(startDate, endDate)
                                group r by r.ClientId).ToDictionary(r => r, r => r.ToList());

            if (reportsToRun.Count == 0)
            {
                EventSource.Raise(Event.Information, method, string.Format("There are no customer reports to run for time period from {0} to {1}.",
                    startDate.ToString(Common.Constants.DATE_FORMAT), endDate.ToString(Common.Constants.DATE_FORMAT)));
                return;
            }

            EventSource.Raise(Event.Information, method, string.Format("Running customer reports for {0} organisations for time period from {1} to {2}.",
                reportsToRun.Count, startDate.ToString(Common.Constants.DATE_FORMAT), endDate.ToString(Common.Constants.DATE_FORMAT)));

            _memberCount = _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now);

            int emailCount = 0;
            foreach (var reportToRun in reportsToRun)
            {
                emailCount += SendReports(reportToRun.Value, startDate, endDate);
            }

            EventSource.Raise(Event.Information, method, string.Format("{0} report emails were sent.", emailCount));
        }

        private int SendReports(IEnumerable<EmployerReport> reports, DateTime startDate, DateTime endDate)
        {
            const string method = "SendReports";

            var allEmails = new List<TemplateEmail>();

            // Generate the emails for all reports.

            foreach (var definition in reports)
            {
                try
                {
                    var organisation = (VerifiedOrganisation)_organisationsQuery.GetOrganisation(definition.ClientId);
                    var accountManager = _administratorsQuery.GetAdministrator(organisation.AccountManagerId);
                    var reportEmails = RunReport(definition, organisation, startDate, endDate, accountManager);
                    allEmails.AddRange(reportEmails.Where(r => r != null));
                }
                catch (Exception ex)
                {
                    EventSource.Raise(Event.Error, method, string.Format("Failed to send report of type '{0}' (definition ID = {1})", definition.Type, definition.Id), ex, new StandardErrorHandler());
                }
            }

            // Combine multiple emails to the same receipients into one.

            var groupedEmails = CombineReports(allEmails);

            // Send the emails

            foreach (var email in groupedEmails)
            {
                _emailsCommand.TrySend(email);
            }
            DateTime sentTime = DateTime.Now;

            // Record that the reports were sent.

            foreach (var definition in reports)
            {
                // Rather than duplicate the logic which determines whether an email should have been sent
                // to the client or account manager here look through the emails collection to find it.

                var clientEmailAddress = (definition.SendToClient ? FindClientEmailAddress(allEmails, definition) : null);
                var accountManager = (definition.SendToAccountManager ? FindAccountManager(allEmails, definition) : null);

                if (clientEmailAddress != null || accountManager != null)
                {
                    var sent = new ReportRun {ReportId = definition.Id, SentTime = sentTime, PeriodStart = startDate, PeriodEnd = endDate, SentToAccountManagerId = accountManager == null ? (Guid?)null : accountManager.Id, SentToClientEmail = clientEmailAddress};
                    _employerReportsCommand.CreateReportRun(sent);
                }
            }

            return groupedEmails.Count;
        }

        private static string FindClientEmailAddress(IEnumerable<TemplateEmail> emails, Report report)
        {
            foreach (var email in emails)
            {
                var clientEmail = email as EmployerReportEmail;
                if (clientEmail != null && clientEmail.Report == report)
                    return clientEmail.To.EmailAddress;
            }

            return null; // No email sent to the client - there was probably no activity.
        }

        private static ICommunicationUser FindAccountManager(IEnumerable<TemplateEmail> emails, Report report)
        {
            foreach (var email in emails)
            {
                var amEmail = email as AccountManagerReportEmail;
                if (amEmail != null && amEmail.Report == report)
                    return amEmail.AccountManager;
            }

            return null; // No email sent to the client - there was probably no activity.
        }

        private static IList<TemplateEmail> CombineReports(IEnumerable<TemplateEmail> all)
        {
            var grouped = new List<TemplateEmail>(all);

            for (int iToKeep = 0; iToKeep < grouped.Count - 1; iToKeep++)
            {
                // Are any emails following this one to the same receipients? If so, add their
                // attachments to this one and remove them.

                int iCopy = iToKeep + 1;
                while (iCopy < grouped.Count)
                {
                    if (ShouldCombineEmails(grouped[iToKeep], grouped[iCopy]))
                    {
                        // If one of the emails is a NoSearchActivity email that's the one we want to keep.
                        // It takes priority over the standard activity email. Little bit of a hack.

                        if (grouped[iCopy] is NoSearchActivityEmail && !(grouped[iToKeep] is NoSearchActivityEmail))
                        {
                            MiscUtils.SwapItems(grouped, iCopy, iToKeep);
                        }

                        // Combine the attachments

                        grouped[iToKeep].AddAttachments(grouped[iCopy].GetAttachments());

                        // Combine properties specific to the type of email

                        var amToKeep = grouped[iToKeep] as AccountManagerReportEmail;
                        var amCopy = grouped[iCopy] as AccountManagerReportEmail;
                        if (amToKeep != null && amCopy != null)
                        {
                            amToKeep.NoActivity.AddRange(amCopy.NoActivity);
                            amToKeep.CustomHtmlSnippets.AddRange(amCopy.CustomHtmlSnippets);
                        }

                        grouped.RemoveAt(iCopy);
                    }
                    else
                    {
                        iCopy++;
                    }
                }
            }

            return grouped;
        }

        private static bool ShouldCombineEmails(TemplateEmail one, TemplateEmail two)
        {
            return GetComparisonType(one) == GetComparisonType(two)
                && IsSameAddress(one.To, two.To)
                && IsSameAddress(one.Copy, two.Copy)
                && IsAccountManagerNoActivityEmail(one) == IsAccountManagerNoActivityEmail(two);
        }

        private static Type GetComparisonType(TemplateEmail email)
        {
            // It's OK to combine NoSearchActivityEmail with CustomerReportEmail, but otherwise
            // keep different types of emails separate.

            Type type = email.GetType();
            return (type == typeof(NoSearchActivityEmail) ? typeof(EmployerReportEmail) : type);
        }

        private static bool IsSameAddress(IList<ICommunicationUser> one, IList<ICommunicationUser> two)
        {
            if (one == null && two == null)
                return true;
            if (one == null ^ two == null)
                return false;
            if (one.Count != two.Count)
                return false;
            if (one.Count != 1)
                return false;
            return IsSameAddress(one[0], two[0]);
        }

        private static bool IsSameAddress(ICommunicationRecipient one, ICommunicationRecipient two)
        {
            if (one == null && two == null)
                return true;
            if (one == null ^ two == null)
                return false;

            return one.EmailAddress == two.EmailAddress;
        }

        private static bool IsAccountManagerNoActivityEmail(TemplateEmail email)
        {
            var accountManagerEmail = email as AccountManagerReportEmail;
            return (accountManagerEmail != null && accountManagerEmail.NoActivity.Count > 0);
        }

        private IEnumerable<TemplateEmail> RunReport(EmployerReport report, VerifiedOrganisation organisation, DateTime startDate, DateTime endDate, IAdministrator accountManager)
        {
            // First work out who we need to send it to

            var contactDetails = report.SendToClient
                ? _organisationsQuery.GetEffectiveContactDetails(organisation.Id)
                : null;

            var accountManagerEmail = report.SendToAccountManager && accountManager.IsEnabled
                ? new AccountManagerReportEmail((ICommunicationUser)accountManager, report, organisation, startDate, endDate)
                : null;
            
            if (contactDetails == null && accountManagerEmail == null)
                return new TemplateEmail[0]; // No-one to send the report to.

            // Run the report and attach it to the email(s).

            string fileName = GetFilename(report, organisation, ".pdf");

            var pdfStream = new MemoryStream();
            var sb = new StringBuilder();
            var outcome = _executeEmployerReportsCommand.RunReport(report, true, organisation, accountManager, new DateRange(startDate, endDate), null, pdfStream, sb);

            if (outcome == ReportRunOutcome.InvalidParameters)
                return new TemplateEmail[0];

            if (accountManagerEmail != null)
            {
                switch (outcome)
                {
                    case ReportRunOutcome.TextResultOnly:
                        accountManagerEmail.AddCustomHtml(GetResultsHtml(report, organisation, sb.ToString()));
                        break;

                    case ReportRunOutcome.NoResults:
                        accountManagerEmail.AddNoActivityReport(report);
                        break;
                }
            }

            var allEmails = new List<TemplateEmail>();
            if (accountManagerEmail != null)
                allEmails.Add(accountManagerEmail);

            if (contactDetails != null && (report.ReportAsFile || sb.Length != 0))
            {
                if (!string.IsNullOrEmpty(contactDetails.EmailAddress))
                {
                    allEmails.Add(CreateEmail(
                        new Employer
                            {
                                EmailAddress = new EmailAddress { Address = contactDetails.EmailAddress },
                                FirstName = contactDetails.FirstName,
                                LastName = contactDetails.LastName,
                                IsEnabled = true
                            },
                        (ICommunicationUser)accountManager,
                        report,
                        organisation,
                        startDate,
                        endDate,
                        outcome,
                        sb.ToString(),
                        _memberCount));
                }

                if (!string.IsNullOrEmpty(contactDetails.SecondaryEmailAddresses))
                {
                    var secondaryEmailAddresses = TextUtil.SplitEmailAddresses(contactDetails.SecondaryEmailAddresses);
                    if (secondaryEmailAddresses != null)
                    {
                        foreach (var secondaryEmailAddress in secondaryEmailAddresses)
                            allEmails.Add(CreateEmail(
                                new Employer
                                {
                                    EmailAddress = new EmailAddress { Address = secondaryEmailAddress },
                                    IsEnabled = true
                                },
                                (ICommunicationUser)accountManager,
                                report,
                                organisation,
                                startDate,
                                endDate,
                                outcome,
                                sb.ToString(),
                                _memberCount));
                    }
                }
            }

            if (outcome == ReportRunOutcome.FileResult || report.ReportFileEvenIfNoResults)
            {
                foreach (var email in allEmails)
                {
                    if (email != null)
                    {
                        // The same Stream can't be used for multiple attachments, so clone it.
                        var attachmentStream = new MemoryStream(pdfStream.ToArray());
                        email.AddAttachments(new[] { new ContentAttachment(attachmentStream, fileName, MediaType.Pdf) });
                    }
                }
            }

            return allEmails;
        }

        private static string GetResultsHtml(EmployerReport report, IOrganisation organisation, string result)
        {
            if (report is CandidateCareReport)
            {
                if (string.IsNullOrEmpty(result))
                    return string.Format("{0} has not referred any members to LinkMe during this reporting period.", HttpUtility.HtmlEncode(organisation.FullName));
                return string.Format("{0} has referred {1} member{2} to LinkMe during this reporting period.", HttpUtility.HtmlEncode(organisation.FullName), result, (result == "1" ? "" : "s"));
            }

            return null;
        }

        private static string GetFilename(EmployerReport report, IOrganisation organisation, string extension)
        {
            return FileSystem.GetValidFileName(report.Name + " - " + organisation.FullName + extension);
        }

        private static EmployerReportEmail CreateEmail(ICommunicationUser to, ICommunicationUser accountManager, Report report, IOrganisation organisation, DateTime startDate, DateTime endDate, ReportRunOutcome outcome, string result, int memberCount)
        {
            if (report is CandidateCareReport)
                return new CandidateCareReportEmail(to, accountManager, report, organisation, startDate, endDate, result);

            return report is ResumeSearchActivityReport && outcome == ReportRunOutcome.NoResults
                ? new NoSearchActivityEmail(to, accountManager, report, organisation, startDate, endDate, memberCount)
                : new EmployerReportEmail(to, accountManager, report, organisation, startDate, endDate);
        }
    }
}
