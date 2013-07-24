using System;
using System.Web;

namespace LinkMe.Web.Helper
{
    public static class HttpHelper
    {
        public static void SetResponseToFileDownload(HttpResponse response, string filename,
            string contentType, long? fileSize)
        {
            if (response == null)
                throw new ArgumentNullException("response");
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentException("The contentType must be specified.", "contentType");
            if (filename != null && filename.IndexOf('\"') != -1)
                throw new ArgumentException("The filename must not contain a quote character.", "filename");

            response.ClearHeaders();

            if (!string.IsNullOrEmpty(filename))
            {
                response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
            }

            response.ContentType = contentType;

            if (fileSize.HasValue)
            {
                response.AddHeader("Content-Length", fileSize.Value.ToString());
            }
        }

        public static void WriteFileDownloadToResponse(HttpResponse response, string filename,
            string contentType, string fileContent)
        {
            if (fileContent == null)
                throw new ArgumentNullException("fileContent");

            SetResponseToFileDownload(response, filename, contentType, fileContent.Length);

            response.ClearContent();
            response.Write(fileContent);
        }

        public static bool IsBotUserAgent(string userAgent)
        {
            if (userAgent == null)
                return false;

            return (userAgent.IndexOf("+http://www.google.com/bot.html") != -1
                || userAgent == "Jyxobot/1"
                || userAgent.IndexOf("http://www.cuil.com/twiceler/robot.html") != -1);
        }
    }
}
