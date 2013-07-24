using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using FlexCel.Report;
using FlexCel.XlsAdapter;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Reports.Credits.Queries;
using LinkMe.Query.Reports.Roles.JobAds.Queries;

namespace LinkMe.Apps.Agents.Reports.Employers.Commands
{
    internal class JobBoardActivityReportRunner
        : ReportRunner
    {
        private readonly ICreditReportsQuery _creditReportsQuery;
        private readonly IJobAdReportsQuery _jobAdReportsQuery;
        private readonly IRecruitersQuery _recruitersQuery;
        private readonly IOrganisationsQuery _organisationsQuery;
        private readonly JobBoardActivityReport _report;
        private readonly bool _includeCredits;

        public JobBoardActivityReportRunner(ICreditReportsQuery creditReportsQuery, IJobAdReportsQuery jobAdReportsQuery, IRecruitersQuery recruitersQuery, IOrganisationsQuery organisationsQuery, JobBoardActivityReport report, bool includeCredits, IOrganisation organisation, IAdministrator accountManager, DateRange dateRange)
            : base(organisation, accountManager, dateRange)
        {
            _creditReportsQuery = creditReportsQuery;
            _jobAdReportsQuery = jobAdReportsQuery;
            _recruitersQuery = recruitersQuery;
            _organisationsQuery = organisationsQuery;
            _report = report;
            _includeCredits = includeCredits;
        }

        public override ReportRunOutcome Run(IDbConnectionFactory connectionFactory, XlsFile excelFile, StringBuilder stringOutput)
        {
            if (excelFile == null)
                throw new ArgumentNullException("excelFile");
            using (var dataSet = GetDataSet(connectionFactory))
            {
                var orgUnit = dataSet.Tables["OrgUnit"];
                var jobAd = dataSet.Tables["JobAd"];

                if (jobAd.Rows.Count == 0)
                    return ReportRunOutcome.NoResults; // No point in generating a report when there are no job ads.

                using (var flexCelReport = new FlexCelReport(true))
                {
                    SetProperties(flexCelReport);
                    SetCreditReportProperties(flexCelReport);
                    SetJobAdReportProperties(flexCelReport);

                    flexCelReport.SetValue("ShowGrandTotal", orgUnit.Rows.Count > 1);
                    flexCelReport.AddTable("OrgUnit", orgUnit);
                    flexCelReport.AddTable("JobAd", jobAd);

                    flexCelReport.Run(excelFile);
                }

                return ReportRunOutcome.FileResult;
            }
        }

        private void SetJobAdReportProperties(FlexCelReport flexCelReport)
        {
            if (_includeCredits)
            {
                var posterIds = GetPosterIds();

                var jobAdReport = _jobAdReportsQuery.GetJobAdReport(posterIds, _dateRange);
                var jobAdTotalsReport = _jobAdReportsQuery.GetJobAdTotalsReport(jobAdReport.ClosedJobAds);

                flexCelReport.SetValue("OpenedJobAds", jobAdReport.OpenedJobAds.Count);
                flexCelReport.SetValue("ClosedJobAds", jobAdReport.ClosedJobAds.Count);
                flexCelReport.SetValue("TotalApplications", jobAdReport.Totals.Applications);
                flexCelReport.SetValue("TotalViews", jobAdReport.Totals.Views);

                flexCelReport.SetValue("TotalClosedJobAdApplications", jobAdTotalsReport.Applications);
                flexCelReport.SetValue("TotalClosedJobAdViews", jobAdTotalsReport.Views);
            }
        }

        private void SetCreditReportProperties(FlexCelReport flexCelReport)
        {
            if (_includeCredits)
            {
                var report = _creditReportsQuery.GetCreditReport<ApplicantCredit>(_organisation.Id, _dateRange);

                flexCelReport.SetValue("OpeningCredits", GetCreditValue(report.OpeningCredits) ?? "unlimited");
                flexCelReport.SetValue("ClosingCredits", GetCreditValue(report.ClosingCredits) ?? "unlimited");
                flexCelReport.SetValue("CreditsUsed", GetCreditValue(report.CreditsUsed) ?? string.Empty);
                flexCelReport.SetValue("CreditsAdded", GetCreditValue(report.CreditsAdded) ?? string.Empty);
                flexCelReport.SetValue("CreditsExpired", GetCreditValue(report.CreditsExpired) ?? string.Empty);

                if (report.ClosingCredits == null || report.CreditsUsed == null || report.CreditsUsed.Value == 0)
                    flexCelReport.SetValue("MonthsToTopUp", string.Empty);
                else
                    flexCelReport.SetValue("MonthsToTopUp", Math.Max(1, report.ClosingCredits.Value / report.CreditsUsed.Value));
            }
        }

        private static object GetCreditValue(int? credits)
        {
            return credits == null ? (object)null : credits.Value;
        }

        private IEnumerable<Guid> GetPosterIds()
        {
            return _report.IncludeChildOrganisations
                ? _recruitersQuery.GetRecruiters(_organisationsQuery.GetSubOrganisationHierarchy(_organisation.Id))
                : _recruitersQuery.GetRecruiters(_organisation.Id);
        }

        private DataSet GetDataSet(IDbConnectionFactory connectionFactory)
        {
            var sql = GetSql();
            var dataSet = new DataSet("Activity");
            var orgUnit = dataSet.Tables.Add("OrgUnit");
            var jobAd = dataSet.Tables.Add("JobAd");

            using (var connection = connectionFactory.CreateConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandTimeout = DbCommandTimeout;

                    DatabaseHelper.AddParameter(command, "@startTime", DbType.DateTime, _dateRange.Start.Value.Date);
                    DatabaseHelper.AddParameter(command, "@endTime", DbType.DateTime, _dateRange.End.Value.Date.AddDays(1));
                    DatabaseHelper.AddParameter(command, "@orgUnitId", DbType.Guid, _report.ClientId);
                    DatabaseHelper.InlineParameters(command);

                    using (var reader = command.ExecuteReader())
                    {
                        dataSet.Load(reader, LoadOption.OverwriteChanges, orgUnit, jobAd);
                    }
                }
            }

            dataSet.Relations.Add(orgUnit.Columns["id"], jobAd.Columns["organisationId"]);

            return dataSet;
        }

        private string GetSql()
        {
            const string mainSql = @"
SELECT *
FROM @orgs

SELECT
    organisationId,
    j.id,
    j.title AS Title,
    j.externalReferenceId AS ExternalReferenceId,
    j.createdTime AS CreatedTime,
    u.loginId AS LoginId,
    CASE j.status WHEN 2 THEN 'Open' ELSE 'Closed' END AS Status,
	COALESCE(dbo.GetJobAdLastClosedTime(j.id), j.expiryTime) AS CloseDate,
    (SELECT COUNT(*) FROM dbo.JobAdViewing WHERE jobAdId = j.id AND time < @endTime) AS ViewsToDate,
	(SELECT COUNT(DISTINCT applicantId) FROM dbo.JobApplication WHERE jobAdId = j.id AND createdTime < @endTime) AS ApplicationsToDate,
	(SELECT COUNT(DISTINCT applicantId) FROM dbo.JobApplication WHERE jobAdId = j.id AND createdTime >= @startTime AND createdTime < @endTime) AS ApplicationsInPeriod,
    (SELECT COUNT(*) FROM dbo.CandidateAccessPurchase AS p INNER JOIN dbo.JobApplication AS a ON a.id = p.referenceId WHERE a.jobAdId = j.id AND p.purchaseTime >= @startTime AND p.purchaseTime < @endTime AND p.adjustedAllocation = 1) AS CreditsUsedInPeriod
FROM
    dbo.JobAd AS j
INNER JOIN
    dbo.Employer AS e ON e.id = j.jobPosterId
INNER JOIN
    dbo.RegisteredUser AS u ON u.id = e.id
INNER JOIN
    @orgs AS o ON e.organisationId = o.id
WHERE
    (j.status = 2 OR j.status = 3)
    AND
    (
        (dbo.GetJobAdLastClosedTime(j.id) >= @startTime AND dbo.GetJobAdLastClosedTime(j.id) < @endTime)    -- Last closed between the dates.
        OR
        (dbo.GetJobAdFirstOpenedTime(j.id) >= @startTime AND dbo.GetJobAdFirstOpenedTime(j.id) < @endTime)  -- First opened between the dates.
        OR
        (dbo.GetJobAdFirstOpenedTime(j.id) < @startTime AND dbo.GetJobAdLastClosedTime(j.id) >= @endTime)   -- Opened before and closed after.
        OR
        (dbo.GetJobAdFirstOpenedTime(j.id) < @endTime AND j.status = 2)                                     -- Opened before the end date and still open.
    )
ORDER BY
    o.fullName, j.createdTime, j.externalReferenceId, j.title
";

            return GetOrganisationsSql(_report.IncludeChildOrganisations) + mainSql;
        }
    }
}