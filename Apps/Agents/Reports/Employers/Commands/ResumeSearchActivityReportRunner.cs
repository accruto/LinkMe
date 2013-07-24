using System;
using System.Data;
using System.Text;
using FlexCel.Report;
using FlexCel.XlsAdapter;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Reports.Credits.Queries;

namespace LinkMe.Apps.Agents.Reports.Employers.Commands
{
    internal class ResumeSearchActivityReportRunner
        : ReportRunner
    {
        // For the purposes of checking whether there's any activity in the organisation ignore alerts,
        // but list a user in the "active" part of the result set if they are enabled and have alerts.
        private const string HaveActivityCondition = "(searches > 0 OR views > 0 OR contacts > 0)";
        private const string ActiveUserCondition = "(searches > 0 OR views > 0 OR contacts > 0 OR (isEnabled = 1 AND alerts > 0))";
        private const string SortUsersBy = "loginId";

        private readonly ICreditReportsQuery _creditReportsQuery;
        private readonly ResumeSearchActivityReport _report;
        private readonly bool _includeCredits;

        public ResumeSearchActivityReportRunner(ICreditReportsQuery creditReportsQuery, ResumeSearchActivityReport report, bool includeCredits, IOrganisation organisation, IAdministrator accountManager, DateRange dateRange)
            : base(organisation, accountManager, dateRange)
        {
            _creditReportsQuery = creditReportsQuery;
            _report = report;
            _includeCredits = includeCredits;
        }

        public override ReportRunOutcome Run(IDbConnectionFactory connectionFactory, XlsFile excelFile, StringBuilder stringOutput)
        {
            using (var dataSet = GetDataSet(connectionFactory))
            {
                var orgUnit = dataSet.Tables["OrgUnit"];
                var user = dataSet.Tables["User"];
                var activeUser = new DataView(user, ActiveUserCondition, SortUsersBy, DataViewRowState.CurrentRows);
                var inactiveUser = new DataView(user, "NOT " + ActiveUserCondition, SortUsersBy, DataViewRowState.CurrentRows);

                var haveActivity = (user.Select(HaveActivityCondition).Length > 0);

                using (var flexCelReport = new FlexCelReport(true))
                {
                    SetProperties(flexCelReport);
                    SetCreditReportProperties(flexCelReport);

                    flexCelReport.SetValue("ShowGrandTotal", orgUnit.Rows.Count > 1);
                    flexCelReport.AddTable("OrgUnit", orgUnit);
                    flexCelReport.AddTable("ActiveUser", activeUser);
                    flexCelReport.AddTable("InactiveUser", inactiveUser);

                    flexCelReport.Run(excelFile);
                }

                return (haveActivity ? ReportRunOutcome.FileResult : ReportRunOutcome.NoResults);
            }
        }

        private void SetCreditReportProperties(FlexCelReport flexCelReport)
        {
            if (_includeCredits)
            {
                var report = _creditReportsQuery.GetCreditReport<ContactCredit>(_organisation.Id, _dateRange);

                flexCelReport.SetValue("OpeningCredits", GetCreditValue(report.OpeningCredits) ?? "unlimited");
                flexCelReport.SetValue("ClosingCredits", GetCreditValue(report.ClosingCredits) ?? "unlimited");
                flexCelReport.SetValue("CreditsUsed", GetCreditValue(report.CreditsUsed) ?? string.Empty);
                flexCelReport.SetValue("CreditsAdded", GetCreditValue(report.CreditsAdded) ?? string.Empty);
                flexCelReport.SetValue("CreditsExpired", GetCreditValue(report.CreditsExpired) ?? string.Empty);

                if (report.ClosingCredits == null || report.CreditsUsed == null || report.CreditsUsed.Value == 0)
                    flexCelReport.SetValue("MonthsToTopUp", string.Empty);
                else
                    flexCelReport.SetValue("MonthsToTopUp", "Approx " + Math.Max(1, report.ClosingCredits.Value/report.CreditsUsed.Value));
            }
        }

        private static object GetCreditValue(int? credits)
        {
            return credits == null ? (object)null : credits.Value;
        }

        private DataSet GetDataSet(IDbConnectionFactory connectionFactory)
        {
            var sql = GetSql();
            var dataSet = new DataSet("Activity");
            var orgUnit = dataSet.Tables.Add("OrgUnit");
            var user = dataSet.Tables.Add("User");

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
                        dataSet.Load(reader, LoadOption.OverwriteChanges, orgUnit, user);
                    }
                }
            }

            dataSet.Relations.Add(orgUnit.Columns["id"], user.Columns["organisationId"]);

            return dataSet;
        }

        private string GetSql()
        {
            const string excludeDisabledUsersClause = "AND ru.flags & 4 = 0";
            const string mainReportSqlFormat = @"

SELECT *
FROM @orgs

SELECT emp.organisationId, ru.loginId, ru.firstName + ' ' + ru.lastName AS [name],
	CASE WHEN ru.flags & 4 = 4 THEN 0 ELSE 1 END AS isEnabled,
	ISNULL(se.searches, 0) AS searches, ISNULL(rv.views, 0) AS [views],
	ISNULL(cn.contacts, 0) AS contacts,	ISNULL(al.alerts, 0) AS alerts
FROM dbo.RegisteredUser AS ru
INNER JOIN dbo.Employer AS emp ON emp.id = ru.[id]
INNER JOIN @orgs AS o ON emp.organisationId = o.[id]
LEFT JOIN
(
	SELECT rs.searcherId, COUNT(1) AS searches
	FROM dbo.ResumeSearch AS rs
	WHERE rs.startTime BETWEEN @startTime AND @endTime 
	GROUP BY rs.searcherId
)
AS se ON se.searcherId = ru.id
LEFT JOIN
(
	SELECT mv.employerId, COUNT(1) AS [views]
	FROM dbo.MemberViewing AS mv
	WHERE mv.time BETWEEN @startTime AND @endTime
	GROUP BY mv.employerId
) AS rv ON rv.employerId = ru.id
LEFT JOIN
(
	SELECT mc.employerId, COUNT(1) AS contacts
	FROM dbo.MemberContact AS mc
	WHERE mc.time BETWEEN @startTime AND @endTime
	GROUP BY mc.employerId
) AS cn ON cn.employerId = ru.id
LEFT JOIN
(
	SELECT rs.ownerId AS id, COUNT(1) AS alerts
	FROM dbo.SavedResumeSearch AS rs
	INNER JOIN dbo.SavedResumeSearchAlert AS al	ON al.savedResumeSearchId = rs.id
	GROUP BY rs.ownerId
) AS al ON al.id = ru.id
WHERE ru.createdTime < @endTime {0}
ORDER BY o.fullName, o.[id], loginId
";

            return GetOrganisationsSql(_report.IncludeChildOrganisations) + string.Format(mainReportSqlFormat, _report.IncludeDisabledUsers ? "" : excludeDisabledUsersClause);
        }
    }
}