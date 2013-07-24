using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace LinkMe.Apps.Services.External.Disqus
{
    public class DisqusQuery
        : IDisqusQuery
    {
        private const string DisqusThreadApiUrl = "http://disqus.com/api/3.0/threads/details.json?thread:ident={0}&forum=linkme&api_secret=PnD2jKgWO6s7Z9JFE6Yjecgnjb7WpX3meyCbRf0VFgPHgknoQl4U6mCtkjwvcto5";

        /// <summary>
        /// Get the number of comments made in a particular thread
        /// </summary>
        /// <param name="threadIdentifier">The unique identifier for the thread, eg. the answeredQuestionId</param>
        /// <returns>The number of total comments (including comments on comments)</returns>
        int? IDisqusQuery.GetCommentCount(Guid threadIdentifier)
        {
            try
            {
                var request = WebRequest.Create(string.Format(DisqusThreadApiUrl, threadIdentifier));
                request.Timeout = 500;      //short timeout so as not to hold up execution
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();

                using (var reader = new StreamReader(responseStream))
                {
                    var contents = reader.ReadToEnd();

                    var serializer = new JavaScriptSerializer();
                    serializer.RegisterConverters(new[] { new DisqusThreadResponseJavaScriptConverter() });
                    var disqusThreadResponse = serializer.Deserialize<DisqusThreadJsonModel>(contents);

                    return disqusThreadResponse.Response.Posts;
                }

            }
            catch (WebException)
            {
                // Disqus will throw a 400 if there are no comments for a question
            }

            return null;
        }

    }
}
