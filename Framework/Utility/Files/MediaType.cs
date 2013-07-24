using System;
using System.Collections.Generic;

namespace LinkMe.Framework.Utility.Files
{
    public static class MediaType
    {
        public const string Word = "application/msword";
        public const string RichText = "application/rtf";
        public const string Pdf = "application/pdf";
        public const string Excel = "application/vnd.ms-excel";
        public const string Text = "text/plain";
        public const string Html = "text/html";
        public const string Csv = "application/csv";
        public const string Zip = "application/zip";

        // Based on http://msdn2.microsoft.com/en-us/library/ms775147.aspx
        // and http://www.w3schools.com/media/media_mimeref.asp

        private static readonly IDictionary<string, string> ExtensionToMediaType = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase)
        {
            {".doc", Word},
            {".rtf", RichText},
            {".txt", Text},
            {".htm", Html},
            {".html", Html},
            {".jpe", "image/jpeg"},
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".gif", "image/gif"},
            {".bmp", "image/bmp"},
            {".wmf", "image/x-wmf"},
            {".emf", "image/x-emf"},
            {".png", "image/png"},
            {".pdf", Pdf},
            {".xls", Excel},
            {".csv", Csv},
            {".eot", "application/vnd.ms-fontobject"},
            {".woff", "application/x-woff"},
            {".ttf", "application/octet-stream"},
            {".svg", "image/svg+xml"},
            {".zip", Zip},
        };

        public static string GetMediaTypeFromExtension(string extension, string defaultValue)
        {
            // Look up the file extension in a list. The registry is unreliable for this, eg. Word
            // registers the MIME type for ".rtf" as "application/msword", which is incorrect.

            string value;
            return ExtensionToMediaType.TryGetValue(extension, out value)
                       ? value
                       : defaultValue;
        }
    }
}