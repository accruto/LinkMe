using System.IO;
using Microsoft.Build.Framework;

namespace LinkMe.Environment.Build.Tasks.StyleSheets
{
    public class MakeUrlsAbsolute
        : StyleSheetsTask
    {
        [Required]
        public ITaskItem WebRootDirectory { get; set; }

        public override bool Execute()
        {
            return Execute(MakeAbsolute);
        }

        private string MakeAbsolute(string url, string cssSourceFilePath)
        {
            if (string.IsNullOrEmpty(url))
                return "url(\"" + url + "\")";
            if (url.StartsWith("data:"))
                return "url(" + url + ")";
            if (url.StartsWith("/"))
                return "url(\"" + url + "\")";

            var filePath = FilePath.GetAbsolutePath(url.Replace("/", "\\"), Path.GetDirectoryName(cssSourceFilePath));
            if (!File.Exists(filePath))
            {
                Log.LogWarning("\t'{0}' not found for url '{1}' in css file '{2}'.", filePath, url, cssSourceFilePath);
                return "url(\"" + url + "\")";
            }

            var absolutePath = FilePath.GetRelativePath(filePath, WebRootDirectory.ItemSpec);
            absolutePath = "/" + absolutePath.Replace("\\", "/");

            Log.LogMessage("\tFile '{0}' replaced with '{1}'.", url, absolutePath);
            return "url(\"" + absolutePath + "\")";
        }
    }
}
