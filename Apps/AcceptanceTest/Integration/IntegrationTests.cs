using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Services.External.CareerOne.Queries;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.AcceptanceTest.Integration
{
    public abstract class IntegrationTests
        : WebTestClass
    {
        protected readonly ICareerOneQuery _careerOneQuery = Resolve<ICareerOneQuery>();

        protected const string Password = "password";
        private static readonly Encoding Encoding = Encoding.GetEncoding(28591); // Latin-1

        protected static string Get(ReadOnlyUrl url, IntegratorUser user, string password, bool useHeaders)
        {
            if (!useHeaders)
            {
                var newUrl = url.AsNonReadOnly();
                newUrl.QueryString["LinkMeUsername"] = user.LoginId;
                newUrl.QueryString["LinkMePassword"] = password;
                url = newUrl;
            }

            var request = (HttpWebRequest)WebRequest.Create(url.AbsoluteUri);
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            request.ContentLength = 0;
            request.Expect = "";
            request.Timeout = Debugger.IsAttached ? -1 : 300000;

            if (useHeaders)
            {
                request.Headers.Add("X-LinkMeUsername", user.LoginId);
                request.Headers.Add("X-LinkMePassword", password);
            }

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var encoding = string.IsNullOrEmpty(response.ContentEncoding) ? Encoding : Encoding.GetEncoding(response.ContentEncoding);
                    using (var reader = new StreamReader(response.GetResponseStream(), encoding))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Response == null)
                    throw;

                if (((HttpWebResponse)e.Response).StatusCode == HttpStatusCode.NotFound)
                    throw new NotFoundException(e.Response.ResponseUri);

                throw;
            }
        }

        protected static string Get(ReadOnlyUrl url, IntegratorUser user)
        {
            return Get(url, user, Password, true);
        }

        protected string Post(ReadOnlyUrl url, IntegratorUser user, string password, string requestXml)
        {
            Browser.RequestHeaders = new WebHeaderCollection
            {
                {"X-LinkMeUsername", user.LoginId},
                {"X-LinkMePassword", password}
            };

            return Post(url, "text/xml", requestXml);
        }
    }
}
