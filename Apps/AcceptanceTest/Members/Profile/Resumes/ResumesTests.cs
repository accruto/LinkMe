using System;
using System.IO;
using System.Net;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.AcceptanceTest.Members.Profile.Resumes
{
    public abstract class ResumesTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        protected readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        protected readonly IResumesQuery _resumesQuery = Resolve<IResumesQuery>();
        private string _contentType;

        protected Member CreateMember(int index)
        {
            return _memberAccountsCommand.CreateTestMember(index);
        }

        protected Stream Download(ReadOnlyUrl url)
        {
            // Create the request, copying appropriate cookies.

            var webRequest = (HttpWebRequest)WebRequest.Create(url.AbsoluteUri);
            webRequest.Method = "GET";
            webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            webRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            webRequest.CookieContainer = new CookieContainer();
            webRequest.CookieContainer.Add(Browser.Cookies.GetCookies(new Uri(url.AbsoluteUri)));

            // Check the response.

            using (var webResponse = webRequest.GetResponse())
            {
                var responseStream = webResponse.GetResponseStream();
                var stream = new MemoryStream();
                var buffer = new byte[65536];
                int read;
                do
                {
                    read = responseStream.Read(buffer, 0, buffer.Length);
                    stream.Write(buffer, 0, read);
                } while (read != 0);
                stream.Seek(0, SeekOrigin.Begin);

                _contentType = webResponse.Headers["Content-Type"];
                return stream;
            }
        }

        protected string GetContentType()
        {
            return _contentType;
        }
    }
}
