using System;
using System.Linq;
using System.ServiceModel.Syndication;
using LinkMe.Apps.Services.External.PageUp;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Domain;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.External.PageUp
{
    [TestClass]
    public class JobFeedMapperTests
        : TestClass
    {
        private readonly string _feed = FileSystem.GetAbsolutePath(@"Apps\Services\Test\External\PageUp\SampleFeed.xml", RuntimeEnvironment.GetSourceFolder());
        private SyndicationItem[] _posts;
        private readonly IJobFeedMapper<SyndicationItem> _mapper = new JobFeedMapper(Resolve<IIndustriesQuery>(), Resolve<ILocationQuery>());

        [TestInitialize]
        public void TestInitialize()
        {
            IJobFeedReader<SyndicationItem> reader = new JobFeedReader(_feed);
            _posts = reader.GetPosts().ToArray();
        }

        [TestMethod]
        public void GetPostIdTest()
        {
            Assert.AreEqual("647438", _mapper.GetPostId(_posts[0]));
        }

        [TestMethod]
        public void ApplyPostDataTest()
        {
            var jobAds = _posts.Select(post =>
                                           {
                                               var jobAd = new JobAd();
                                               _mapper.ApplyPostData(post, jobAd);
                                               return jobAd;
                                           }).ToList();

            Assert.AreEqual("Call Centre - Workforce Analyst", jobAds[0].Title);
            Assert.AreEqual("647438", jobAds[0].Integration.ExternalReferenceId);
            Assert.AreEqual("https://secure.pageuppeople.com/apply/387/gateway/?c=apply&sJobIDs=647438&SourceTypeID=687&sLanguage=en", jobAds[0].Integration.ExternalApplyUrl);
            Assert.AreEqual("Imagine…. The joy you will get from securing tickets to a finals match at the MCG for a proud and passionate AFL fan.", jobAds[2].Description.Summary);
            StringAssert.StartsWith(jobAds[0].Description.Content, "\n        <P><B>Do you use words like, dedicated");
            Assert.AreEqual(JobTypes.FullTime, jobAds[0].Description.JobTypes);
            Assert.AreEqual("Call Centre & Customer Service", jobAds[0].Description.Industries[0].Name);
            Assert.AreEqual("Ultimo", jobAds[0].Description.Location.Suburb);
            Assert.AreEqual("2007", jobAds[0].Description.Location.Postcode);
            Assert.AreEqual("NSW", jobAds[0].Description.Location.CountrySubdivision.ShortName);
        }
    }
}