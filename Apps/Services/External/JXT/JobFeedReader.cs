using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using LinkMe.Apps.Services.External.Jxt.Schema;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Apps.Services.External.Jxt
{
    public class JobFeedReader
        : IJobFeedReader<Job>
    {
        private readonly EventSource _logger;

        private readonly string _remoteBaseUrl;
        private string _filename;
        private const string DefaultFilename = "jobsjobsjobsjobs.xml";

        public JobFeedReader(string remoteBaseUrl, string filename, EventSource logger)
        {
            _remoteBaseUrl = remoteBaseUrl;
            _filename = filename;

            _logger = logger;
        }

        #region Implementation of IJobFeedReader.

        IEnumerable<Job> IJobFeedReader<Job>.GetPosts()
        {
            if (string.IsNullOrEmpty(_filename))
                _filename = DefaultFilename;

            var posts = GetJobFeed(_filename);

            return posts;
        }

        #endregion

        #region Private Methods

        private Job[] GetJobFeed(string file)
        {
            const string method = "DownloadJobPosts";

            // Make a web request.

            _logger.Raise(Event.Information, method, string.Format("Downloading the job feed ({0})...", file));

            var request = WebRequest.Create(Path.Combine(_remoteBaseUrl, file));

            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            // Parse the response.

            var serializer = new XmlSerializer(typeof(Jobs));

            var responseBody = (Jobs)serializer.Deserialize(responseStream);
            _logger.Raise(Event.Information, method, string.Format("Downloaded {0} job ads.", responseBody.Client.Job.Length));

            return responseBody.Client.Job;

        }

        #endregion
    }
}
