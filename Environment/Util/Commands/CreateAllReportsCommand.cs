using System;
using System.Collections.Generic;
using System.IO;

namespace LinkMe.Environment.Util.Commands
{
    public class CreateAllReportsCommand
        : ReportsCommand
    {
        public override void Execute()
        {
            var url = Options["ru"].Values[0];
            var folder = Options["rf"].Values[0];
            var dataSources = Options["rds"] != null ? GetDataSources(Options["rds"].Values.ToArray()) : null;

            var reportFiles = GetReportFiles(folder);

            foreach (var reportFile in reportFiles)
            {
                var reportPath = FilePath.GetRelativePath(reportFile, folder);

                // Convert it into a reporting services path.

                reportPath = "/" + reportPath.Replace("\\", "/").Replace(".rdl", "");

                string name;
                string parent;
                ReportServiceUtil.SplitPath(reportPath, out name, out parent);

                ReportServiceUtil.CreateFolder(url, parent);
                ReportServiceUtil.CreateReport(url, name, parent, dataSources, reportFile);

                Console.WriteLine("The '{0}' report has been created from the '{1}' definition file.", reportPath, reportFile);
            }
        }

        private static string[] GetReportFiles(string folder)
        {
            var reportFiles = new List<string>();
            GetReportFiles(folder, reportFiles);
            return reportFiles.ToArray();
        }

        private static void GetReportFiles(string folder, List<string> files)
        {
            foreach (var file in Directory.GetFiles(folder))
            {
                if (Path.GetExtension(file) == ".rdl")
                    files.Add(file);
            }

            // Iterate.

            foreach (var directory in Directory.GetDirectories(folder))
                GetReportFiles(directory, files);
        }
    }
}
