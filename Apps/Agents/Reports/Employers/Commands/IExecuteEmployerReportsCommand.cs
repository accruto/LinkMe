using System.IO;
using System.Text;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Reports.Employers.Commands
{
    public interface IExecuteEmployerReportsCommand
    {
        ReportRunOutcome RunReport(EmployerReport report, bool includeCredits, IOrganisation organisation, IAdministrator accountManager, DateRange dateRange, Stream xlsOutput, Stream pdfOutput, StringBuilder stringOutput);
    }
}
