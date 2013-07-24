using System;
using System.Collections.Specialized;
using System.Net;
using System.Web.Script.Serialization;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Api.Models.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Api.Resumes
{
    [TestClass]
    public abstract class ResumesApiTests
        : WebTestClass
    {
        protected ReadOnlyUrl _uploadUrl;
        private ReadOnlyUrl _parseUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _uploadUrl = new ReadOnlyApplicationUrl(true, "~/api/resumes/upload");
            _parseUrl = new ReadOnlyApplicationUrl(true, "~/api/resumes/parse");
        }

        protected JsonResumeModel Upload(string file)
        {
            var files = new NameValueCollection { { "file", file } };
            var response = Post(_uploadUrl, null, files);
            return new JavaScriptSerializer().Deserialize<JsonResumeModel>(response);
        }

        protected JsonParsedResumeModel Parse(Guid fileReferenceId)
        {
            return Parse(null, fileReferenceId);
        }

        protected JsonParsedResumeModel Parse(HttpStatusCode? expectedStatusCode, Guid fileReferenceId)
        {
            var response = expectedStatusCode == null
                ? Post(_parseUrl, new NameValueCollection { { "fileReferenceId", fileReferenceId.ToString() } })
                : Post(expectedStatusCode.Value, _parseUrl, new NameValueCollection { { "fileReferenceId", fileReferenceId.ToString() } });
            return new JavaScriptSerializer().Deserialize<JsonParsedResumeModel>(response);
        }
    }
}