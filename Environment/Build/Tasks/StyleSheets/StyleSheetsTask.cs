using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks.StyleSheets
{
    public abstract class StyleSheetsTask
        : Task
    {
        [Required]
        public ITaskItem[] CssSourceFiles { get; set; }

        protected bool Execute(Func<string, string, string> processUrl)
        {
            foreach (var file in CssSourceFiles)
            {
                var cssSourceFilePath = file.ItemSpec;

                Log.LogMessage("Processing '{0}'", cssSourceFilePath);

                var fileContent = File.ReadAllText(file.ItemSpec);
                var fileContentReplaced = Regex.Replace(fileContent, @"url\((?<fileURI>.*?)\)", m => processUrl(m.Groups["fileURI"].Value.Trim('\"', '\''), cssSourceFilePath), RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.ExplicitCapture);
                if (!fileContentReplaced.Equals(fileContent))
                    File.WriteAllText(file.ItemSpec, fileContentReplaced);
            }

            return true;
        }
    }
}
