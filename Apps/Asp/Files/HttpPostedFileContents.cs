using System.IO;
using System.Web;
using LinkMe.Domain.Files;

namespace LinkMe.Apps.Asp.Files
{
    public class HttpPostedFileContents
        : FileContents
    {
        private readonly HttpPostedFileBase _httpPostedFile;

        public HttpPostedFileContents(HttpPostedFile httpPostedFile)
        {
            _httpPostedFile = new HttpPostedFileWrapper(httpPostedFile);
        }

        public HttpPostedFileContents(HttpPostedFileBase httpPostedFile)
        {
            _httpPostedFile = httpPostedFile;
        }

        public override void Save(FilePath filePath)
        {
            _httpPostedFile.SaveAs(filePath.FullPath);
        }

        public override Stream GetStream()
        {
            return _httpPostedFile.InputStream;
        }

        public override string Type
        {
            get { return _httpPostedFile.ContentType; }
        }

        public override int Length
        {
            get { return _httpPostedFile.ContentLength; }
        }

        public override byte[] Hash
        {
            get { return GetHash(_httpPostedFile.InputStream); }
        }
    }
}
