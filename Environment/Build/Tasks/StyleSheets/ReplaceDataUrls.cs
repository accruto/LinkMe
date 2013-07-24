using System;
using System.IO;

namespace LinkMe.Environment.Build.Tasks.StyleSheets
{
    public class ReplaceDataUrls
        : StyleSheetsTask
    {
        public ReplaceDataUrls()
        {
            ImageFileSize = 50;
        }

        public int ImageFileSize { get; set; }

        public override bool Execute()
        {
            return Execute(ReplaceUrl);
        }

        private string ReplaceUrl(string url, string cssSourceFilePath)
        {
            if (url.StartsWith("data:"))
                return "url(" + url + ")";
            if (!url.EndsWith(".png") && !url.EndsWith(".gif"))
                return "url(\"" + url + "\")";

            var filePath = FilePath.GetAbsolutePath(url.Replace("/", "\\"), Path.GetDirectoryName(cssSourceFilePath));
            if (!File.Exists(filePath))
            {
                Log.LogWarning("\t'{0}' not found for url '{1}' in css file '{2}'.", filePath, url, cssSourceFilePath);
                return "url(\"" + url + "\")";
            }

            if (new FileInfo(filePath).Length > ImageFileSize * 1024)
            {
                Log.LogMessage("\tFile '{0}' to large to replace.", filePath);
                return "url(\"" + url + "\")";
            }

            var extension = Path.GetExtension(filePath);
            if (extension == null)
            {
                Log.LogWarning("\t'{0}' for url '{1}' in css file '{2}' does not have an extension.", filePath, url, cssSourceFilePath);
                return "url(\"" + url + "\")";
            }

            byte[] buffer;
            var dataUri = "url(data:image/" + extension.Substring(1) + ";base64,";
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var fileLength = Convert.ToInt32(fs.Length);
                buffer = new byte[fileLength];
                fs.Read(buffer, 0, fileLength);
            }

            dataUri += Convert.ToBase64String(buffer) + ")";

            Log.LogMessage("\tFile '{0}' replaced.", filePath);
            return dataUri;
        }
    }
}
