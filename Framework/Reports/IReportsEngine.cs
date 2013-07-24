using System.Collections.Generic;

namespace LinkMe.Framework.Reports
{
    public enum ReportFormat
    {
        Xml,
        Csv,
        Pdf,
        Html,
        Excel
    };

    public interface IReportsEngine
    {
        byte[] RunReport(string reportPath, ReportFormat format, IDictionary<string, object> parameters);
        string RunHtmlReport(string reportPath, IDictionary<string, object> parameters);
        string RunXmlReport(string reportPath, IDictionary<string, object> parameters);
    }
}
