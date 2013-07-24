using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks.Reports
{
    public class DeployReports
        : Task
    {
        private ITaskItem[] _folders;
        private ITaskItem[] _dataSources;
        private ReportOptions _options = new ReportOptions();

        public ITaskItem[] Folders
        {
            get { return _folders; }
            set { _folders = value; }
        }

        public ITaskItem[] DataSources
        {
            get { return _dataSources; }
            set { _dataSources = value; }
        }

        [Required]
        public string ReportServerUrl
        {
            get { return _options.ReportServerUrl; }
            set { _options.ReportServerUrl = value; }
        }

        public override bool Execute()
        {
            // Iterate over all folders.

            var dataSources = _dataSources == null ? null : (from ds in _dataSources select ds.ItemSpec).ToArray();

            foreach (var folderItem in _folders)
            {
                // Get  the set of report files.

                var folder = folderItem.ItemSpec;
                var reportFiles = GetReportFiles(folder);

                foreach (var reportFile in reportFiles)
                {
                    var reportPath = FilePath.GetRelativePath(reportFile, folder);

                    // Convert it into a reporting services path.

                    reportPath = "/" + reportPath.Replace("\\", "/").Replace(".rdl", "");

                    string name;
                    string parent;
                    ReportServiceUtil.SplitPath(reportPath, out name, out parent);

                    ReportServiceUtil.CreateFolder(_options.ReportServerUrl, parent);
                    ReportServiceUtil.CreateReport(_options.ReportServerUrl, name, parent, dataSources, reportFile);

                    Log.LogMessage("The '{0}' report has been created from the '{1}' definition file.", reportPath, reportFile);
                }
            }

            return true;
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
