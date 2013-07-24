using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.Xml;
using LinkMe.Apps.Services.External.Monster.Schema;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Wcf;

namespace LinkMe.Apps.Services.External.Monster
{
    public class JobFeedReader
        : IJobFeedReader<Job>
    {
        private readonly EventSource _logger;

        private readonly string _remoteBaseUrl;
        private readonly string _remoteUsername;
        private readonly string _remotePassword;
        private IList<string> _files;

        public JobFeedReader(string remoteBaseUrl, string remoteUsername, string remotePassword, IList<string> files, EventSource logger)
        {
            _remoteBaseUrl = remoteBaseUrl;
            _remotePassword = remotePassword;
            _remoteUsername = remoteUsername;
            _files = files;

            _logger = logger;
        }

        #region Implementation of IJobFeedReader.

        IEnumerable<Job> IJobFeedReader<Job>.GetPosts()
        {
            if (_files == null || _files.Count == 0)
                _files = GetJobFeedFiles();

            var postSegments = new Job[_files.Count][];

            // TODO: run in parallel
            for (int i = 0; i < _files.Count; i++)
                postSegments[i] = GetJobFeed(_files[i]);

            IEnumerable<Job> posts = postSegments.SelectMany(segment => segment);
            return posts;
        }

        #endregion

        #region Private Methods

        private IList<string> GetJobFeedFiles()
        {
            #region Log
            const string method = "GetJobFeedFiles";
            _logger.Raise(Event.Information, method, "Getting the list of files...");
            #endregion

            var request = WebRequest.Create(_remoteBaseUrl) as FtpWebRequest;
            if (request == null)
                throw new InvalidOperationException("Unable to automatically generate file list for protocols other than FTP.");

            request.Method = WebRequestMethods.Ftp.ListDirectory;
            if (!string.IsNullOrEmpty(_remoteUsername) || !string.IsNullOrEmpty(_remotePassword))
                request.Credentials = new NetworkCredential(_remoteUsername, _remotePassword);

            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            var reader = new StreamReader(responseStream);

            string yesterday = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
            var lines = ReadAllLines(reader).Where(line => line.StartsWith("C1Alliance_Active_" + yesterday));
            return lines.ToList();
        }

        private static IEnumerable<string> ReadAllLines(TextReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
                yield return line;
        }

        private Job[] GetJobFeed(string file)
        {
            const string method = "DownloadJobPosts";

            // Make a web request.

            _logger.Raise(Event.Information, method, string.Format("Downloading the job feed ({0})...", file));

            var request = WebRequest.Create(_remoteBaseUrl + "/" + file);
            if (!string.IsNullOrEmpty(_remoteUsername) || !string.IsNullOrEmpty(_remotePassword))
                request.Credentials = new NetworkCredential(_remoteUsername, _remotePassword);

            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            // Parse the response.

            var responseReader = XmlReader.Create(responseStream);
            var responseMessage = Message.CreateMessage(responseReader, 1024, MessageVersion.Soap11);
            var responseBody = responseMessage.GetBody<Body>(new XmlSerializerObjectSerializer(typeof(Body)));
            _logger.Raise(Event.Information, method, string.Format("Downloaded {0} job ads.", responseBody.Query.JobCollection.Jobs.Length));

            return responseBody.Query.JobCollection.Jobs;
        }

        #endregion
    }
}
