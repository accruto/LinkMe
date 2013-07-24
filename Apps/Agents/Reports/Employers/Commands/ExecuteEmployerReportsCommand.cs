using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using FlexCel.Core;
using FlexCel.Render;
using FlexCel.XlsAdapter;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Reports.Credits.Queries;
using LinkMe.Query.Reports.Roles.JobAds.Queries;

namespace LinkMe.Apps.Agents.Reports.Employers.Commands
{
    public class ExecuteEmployerReportsCommand
        : IExecuteEmployerReportsCommand
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ICreditReportsQuery _creditReportsQuery;
        private readonly IJobAdReportsQuery _jobAdReportsQuery;
        private readonly IRecruitersQuery _recruitersQuery;
        private readonly IOrganisationsQuery _organisationsQuery;

        public ExecuteEmployerReportsCommand(IDbConnectionFactory connectionFactory, ICreditReportsQuery creditReportsQuery, IJobAdReportsQuery jobAdReportsQuery, IRecruitersQuery recruitersQuery, IOrganisationsQuery organisationsQuery)
        {
            _connectionFactory = connectionFactory;
            _creditReportsQuery = creditReportsQuery;
            _jobAdReportsQuery = jobAdReportsQuery;
            _recruitersQuery = recruitersQuery;
            _organisationsQuery = organisationsQuery;
        }

        ReportRunOutcome IExecuteEmployerReportsCommand.RunReport(EmployerReport report, bool includeCredits, IOrganisation organisation, IAdministrator accountManager, DateRange dateRange, Stream xlsOutput, Stream pdfOutput, StringBuilder stringOutput)
        {
            if (organisation == null)
                throw new ArgumentNullException("organisation");
            if (report.ReportAsFile && xlsOutput == null && pdfOutput == null)
                throw new ArgumentException("At least one output stream must be supplied.");

            using (var templateStream = GetTemplateStream(report, includeCredits))
            {
                XlsFile excelFile = null;
                if (templateStream != null)
                {
                    excelFile = new XlsFile(true);
                    excelFile.Open(templateStream);
                    FixHyperlink(excelFile, 1, "mailto:%3c%23AccountManagerEmail%3e", "mailto:" + HttpUtility.UrlEncode(accountManager.EmailAddress.Address));
                }

                var outcome = Run(report, includeCredits, organisation, accountManager, dateRange, excelFile, stringOutput);

                if (excelFile != null && (outcome == ReportRunOutcome.FileResult || report.ReportFileEvenIfNoResults))
                {
                    if (xlsOutput != null)
                        excelFile.Save(xlsOutput);

                    if (pdfOutput != null)
                    {
                        using (var export = new FlexCelPdfExport(excelFile, false))
                        {
                            export.Properties.Author = "LinkMe.com.au";
                            export.Export(pdfOutput);
                        }
                    }
                }

                return outcome;
            }
        }

        private ReportRunOutcome Run(EmployerReport report, bool includeCredits, IOrganisation organisation, IAdministrator accountManager, DateRange dateRange, XlsFile excelFile, StringBuilder stringOutput)
        {
            ReportRunner runner = null;
            if (report is JobBoardActivityReport)
                runner = new JobBoardActivityReportRunner(_creditReportsQuery, _jobAdReportsQuery, _recruitersQuery, _organisationsQuery, (JobBoardActivityReport)report, includeCredits, organisation, accountManager, dateRange);
            if (report is ResumeSearchActivityReport)
                runner = new ResumeSearchActivityReportRunner(_creditReportsQuery, (ResumeSearchActivityReport)report, includeCredits, organisation, accountManager, dateRange);
            if (report is CandidateCareReport)
                runner = new CandidateCareReportRunner((CandidateCareReport)report, organisation, accountManager, dateRange);

            return runner != null
                ? runner.Run(_connectionFactory, excelFile, stringOutput)
                : ReportRunOutcome.NoResults;
        }

        private static void FixHyperlink(IExcelAdapterCOMv1 excelFile, int index, string expectedText, string replaceValue)
        {
            var hyperLink = excelFile.GetHyperLink(index);
            if (hyperLink.Text != expectedText)
                throw new ApplicationException("Unexpected hyperlink text: " + hyperLink.Text);

            hyperLink.Text = replaceValue;
            excelFile.SetHyperLink(1, hyperLink);
        }

        private static Stream GetTemplateStream(EmployerReport report, bool includeCredits)
        {
            return report.ReportAsFile
                ? Assembly.GetExecutingAssembly().GetManifestResourceStream("LinkMe.Apps.Agents.Reports.Employers." + GetTemplateFileName(report, includeCredits))
                : null;
        }

        private static string GetTemplateFileName(EmployerReport report, bool includeCredits)
        {
            if (report is JobBoardActivityReport)
                return includeCredits ? "JobBoardActivity.xls" : "JobBoardActivityNoCredits.xls";
            if (report is ResumeSearchActivityReport)
                return includeCredits ? "ResumeSearchActivity.xls" : "ResumeSearchActivityNoCredits.xls";
            return null;
        }
    }
}
