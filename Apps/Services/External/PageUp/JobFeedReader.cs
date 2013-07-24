using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;
using LinkMe.Apps.Services.JobAds;

namespace LinkMe.Apps.Services.External.PageUp
{
    public class JobFeedReader
        : IJobFeedReader<SyndicationItem>
    {
        private readonly string _remoteUrl;

        public JobFeedReader(string remoteUrl)
        {
            _remoteUrl = remoteUrl;
        }

        #region Implementation of IJobFeedReader

        IEnumerable<SyndicationItem> IJobFeedReader<SyndicationItem>.GetPosts()
        {
            var feedReader = XmlReader.Create(_remoteUrl);
            var feed = SyndicationFeed.Load(feedReader);
            return feed.Items;
        }

        #endregion
    }
}
