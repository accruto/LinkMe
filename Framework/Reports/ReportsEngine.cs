using System;
using System.Collections.Generic;
using System.IO;
using LinkMe.Framework.Reports.ReportExecution2005;

namespace LinkMe.Framework.Reports
{
    public class ReportsEngine
        : IReportsEngine
    {
        private readonly string _reportExecutionUrl;

        public ReportsEngine(string reportExecutionUrl)
        {
            _reportExecutionUrl = reportExecutionUrl;
        }

        byte[] IReportsEngine.RunReport(string reportPath, ReportFormat format, IDictionary<string, object> parameters)
        {
            return RunReport(reportPath, format, parameters);
        }

        string IReportsEngine.RunHtmlReport(string reportPath, IDictionary<string, object> parameters)
        {
            return RunStringReport(reportPath, ReportFormat.Html, parameters);
        }

        string IReportsEngine.RunXmlReport(string reportPath, IDictionary<string, object> parameters)
        {
            return RunStringReport(reportPath, ReportFormat.Xml, parameters);
        }

        private byte[] RunReport(string reportPath, ReportFormat format, ICollection<KeyValuePair<string, object>> parameters)
        {
            var service = new ReportExecutionService
            {
                Credentials = System.Net.CredentialCache.DefaultCredentials,
                Url = _reportExecutionUrl,
                ExecutionHeaderValue = new ExecutionHeader()
            };

            service.LoadReport(reportPath, null);

            // Prepare report parameters.

            if (parameters != null && parameters.Count > 0)
            {
                var parameterValues = new ParameterValue[parameters.Count];
                var index = 0;
                foreach (var parameterPair in parameters)
                {
                    parameterValues[index] = new ParameterValue
                    {
                        Name = parameterPair.Key,
                        Value = parameterPair.Value == null ? null : parameterPair.Value.ToString()
                    };
                    ++index;
                }

                service.SetExecutionParameters(parameterValues, "en-us");
            }

            const string deviceInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";
            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings;
            string[] streamIds;

            return service.Render(GetFormat(format), deviceInfo, out extension, out encoding, out mimeType, out warnings, out streamIds);
        }

        private string RunStringReport(string reportPath, ReportFormat format, ICollection<KeyValuePair<string, object>> parameters)
        {
            var report = RunReport(reportPath, format, parameters);
            using (var stream = new MemoryStream(report))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static string GetFormat(ReportFormat format)
        {
            switch (format)
            {
                case ReportFormat.Csv:
                    return "CSV";

                case ReportFormat.Pdf:
                    return "PDF";

                case ReportFormat.Html:
                    return "HTML4.0";

                case ReportFormat.Excel:
                    return "EXCEL";
                
                default:
                    return "XML";
            }
        }
    }
}
