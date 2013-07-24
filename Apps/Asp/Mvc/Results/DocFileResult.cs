using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Framework.Utility.Files;

namespace LinkMe.Apps.Asp.Mvc.Results
{
    public class DocFileResult
        : FileResult
    {
        private readonly DocFile _docFile;

        public DocFileResult(DocFile docFile, string mediaType)
            : base(mediaType)
        {
            _docFile = docFile;
            FileDownloadName = docFile.Name;
        }

        public DocFileResult(DocFile docFile)
            : this(docFile, MediaType.Word)
        {
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            response.OutputStream.Write(_docFile.Contents, 0, _docFile.Contents.Length);
        }
    }
}