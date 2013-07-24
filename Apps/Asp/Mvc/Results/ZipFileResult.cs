using System.Web;
using System.Web.Mvc;
using Ionic.Zip;
using LinkMe.Framework.Utility.Files;

namespace LinkMe.Apps.Asp.Mvc.Results
{
    public class ZipFileResult
        : FileResult
    {
        private readonly ZipFile _zipFile;

        public ZipFileResult(ZipFile zipFile)
            : base(MediaType.Zip)
        {
            _zipFile = zipFile;
            FileDownloadName = zipFile.Name;
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            using (_zipFile)
            {
                _zipFile.Save(response.OutputStream);
            }
        }
    }
}