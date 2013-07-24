using System.Data;
using System.Text;
using FlexCel.XlsAdapter;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Apps.Agents.Reports.Employers.Commands
{
    internal class CandidateCareReportRunner
        : ReportRunner
    {
        private readonly CandidateCareReport _report;

        public CandidateCareReportRunner(CandidateCareReport report, IOrganisation organisation, IAdministrator accountManager, DateRange dateRange)
            : base(organisation, accountManager, dateRange)
        {
            _report = report;
        }

        public override ReportRunOutcome Run(IDbConnectionFactory connectionFactory, XlsFile excelFile, StringBuilder stringOutput)
        {
            if (string.IsNullOrEmpty(_report.PromoCode))
                return ReportRunOutcome.InvalidParameters;

            var sql = string.Format(@"
SELECT COUNT(1)
FROM dbo.RegisteredUser ru 
INNER JOIN dbo.Member m
ON m.id = ru.id 
INNER JOIN dbo.JoinReferral jr
ON jr.userId = ru.id
WHERE (jr.promotionCode = '{0}' OR jr.referralCode = '{0}')
AND ru.createdTime BETWEEN @timeStart AND @timeEnd", TextUtil.EscapeSqlText(_report.PromoCode));

            using (var connection = connectionFactory.CreateConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandTimeout = DbCommandTimeout;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;

                    DatabaseHelper.AddParameter(command, "@timeStart", DbType.DateTime, _dateRange.Start.Value.Date);
                    DatabaseHelper.AddParameter(command, "@timeEnd", DbType.DateTime, _dateRange.End.Value.AddDays(1));

                    DatabaseHelper.InlineParameters(command); // This avoids sp_executesql and make it much faster

                    var joins = (int)DatabaseHelper.TimeExecuteScalar(command);
                    if (joins == 0)
                        return ReportRunOutcome.NoResults;
                    stringOutput.Append(joins.ToString());
                    return ReportRunOutcome.TextResultOnly;
                }
            }
        }
    }
}