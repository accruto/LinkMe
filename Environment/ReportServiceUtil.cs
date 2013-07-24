using System.IO;
using System.Linq;
using System.Web.Services.Protocols;
using LinkMe.Environment.ReportService2005;

namespace LinkMe.Environment
{
    public static class ReportServiceUtil
    {
        public static void CreateFolder(string reportingServiceUrl, string folder)
        {
            var pos = folder.LastIndexOf("/");
            if (pos == 0)
            {
                // Top level folder.

                var name = folder.Substring(1);
                CreateFolder(reportingServiceUrl, name, "/");
            }
            else
            {
                var parent = folder.Substring(0, pos);
                var name = folder.Substring(pos + 1);

                // Need to make sure the parent is created.

                CreateFolder(reportingServiceUrl, parent);

                // Create this folder.

                CreateFolder(reportingServiceUrl, name, parent);
            }
        }

        public static void CreateReport(string reportingServiceUrl, string name, string parent, string[] dataSourcePaths, string definitionFile)
        {
            // Open the definition.

            byte[] definition;
            using (var stream = File.OpenRead(definitionFile))
            {
                definition = new byte[stream.Length];
                stream.Read(definition, 0, (int)stream.Length);
                stream.Close();
            }

            var service = new ReportingService2005
            {
                Url = reportingServiceUrl,
                Credentials = System.Net.CredentialCache.DefaultCredentials
            };

            var warnings = service.CreateReport(name, parent, true, definition, null);

            // If there are data sources then associate them with the report.

            if (dataSourcePaths != null && dataSourcePaths.Length > 0)
            {
                var dataSources = (from dsp in dataSourcePaths
                                   select new DataSource
                                   {
                                       Item = new DataSourceReference { Reference = dsp },
                                       Name = GetName(dsp),
                                   }).ToArray();
                service.SetItemDataSources(GetPath(name, parent), dataSources);
            }
        }

        public static void SplitPath(string path, out string name, out string parent)
        {
            var pos = path.LastIndexOf("/");
            name = path.Substring(pos + 1);
            parent = path.Substring(0, pos);
        }

        private static void CreateFolder(string reportingServiceUrl, string folder, string parent)
        {
            try
            {
                var service = new ReportingService2005
                {
                    Url = reportingServiceUrl,
                    Credentials = System.Net.CredentialCache.DefaultCredentials
                };

                service.CreateFolder(folder, parent, null);
            }
            catch (SoapException ex)
            {
                // Check whether the folder already exists.

                var errorCodeNode = ex.Detail["ErrorCode"];
                if (errorCodeNode == null)
                    throw;
                var errorCode = errorCodeNode.InnerText;
                if (errorCode != "rsItemAlreadyExists")
                    throw;
            }
        }

        private static string GetPath(string name, string parent)
        {
            return parent + "/" + name;
        }

        private static string GetName(string path)
        {
            var pos = path.LastIndexOf("/");
            return path.Substring(pos + 1);
        }
    }
}
