using System.Text;
using FlexCel.Report;
using FlexCel.XlsAdapter;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Apps.Agents.Reports.Employers.Commands
{
    internal abstract class ReportRunner
    {
        protected const int DbCommandTimeout = 600;
        private const string DateFormat = "dd-MMM-yy";

        protected readonly IOrganisation _organisation;
        protected IAdministrator _accountManager;
        protected readonly DateRange _dateRange;

        protected ReportRunner(IOrganisation organisation, IAdministrator accountManager, DateRange dateRange)
        {
            _organisation = organisation;
            _dateRange = dateRange;
            _accountManager = accountManager;
        }

        public abstract ReportRunOutcome Run(IDbConnectionFactory connectionFactory, XlsFile excelFile, StringBuilder stringOutput);

        protected void SetProperties(FlexCelReport flexCelReport)
        {
            flexCelReport.SetValue("ReportedOrgName", _organisation.FullName);
            flexCelReport.SetValue("FromDate", _dateRange.Start.Value.ToString(DateFormat));
            flexCelReport.SetValue("ToDate", _dateRange.End.Value.ToString(DateFormat));
            flexCelReport.SetValue("AccountManagerName", _accountManager.FullName);
            flexCelReport.SetValue("AccountManagerPhone", Constants.PhoneNumbers.FreecallText);
            flexCelReport.SetValue("AccountManagerEmail", _accountManager.EmailAddress.Address);
        }

        protected static string GetOrganisationsSql(bool includeChildOrganisations)
        {
            const string orgUnitSelect = @"
DECLARE @orgs TABLE
(
	[id] UNIQUEIDENTIFIER NOT NULL,
	fullName CompanyName NOT NULL
)

INSERT INTO @orgs
SELECT ou.[id], dbo.GetOrganisationFullName(ou.[id], ' ')";

            const string getOrgUnitAndChildren = orgUnitSelect + @"
FROM dbo.OrganisationalUnit ou
WHERE dbo.IsOrganisationDescendent(@orgUnitId, ou.[id]) = 1
ORDER BY dbo.GetOrganisationFullName(ou.[id], NULL)";

            const string getOrgUnitOnly = orgUnitSelect + @"
FROM dbo.OrganisationalUnit ou
WHERE ou.[id] = @orgUnitId";

            return includeChildOrganisations ? getOrgUnitAndChildren : getOrgUnitOnly;
        }
    }
}