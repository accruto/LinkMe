using LinkMe.Apps.Services.External.Monster;
using LinkMe.Apps.Services.External.Monster.Schema;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.External.Monster
{
    [TestClass]
    public class JobAdMapperCompanyTests
        : MonsterFeedTests
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        [TestMethod]
        public void TestCompanyFeed()
        {
            var fileName = FileSystem.GetAbsolutePath(@"Apps\Services\Test\External\Monster\CompanyFeed.xml", RuntimeEnvironment.GetSourceFolder());
            const string postId = "123456";

            var jobs = GetJobFeed(fileName);
            IJobFeedMapper<Job> mapper = new JobAdMapper(_locationQuery, _industriesQuery, null);

            Assert.AreEqual(1, jobs.Count);

            var jobAd = new JobAd
            {
                Integration =
                {
                    IntegratorReferenceId = postId,
                    ExternalApplyUrl = string.Format("http://jobview.careerone.com.au/GetJob.aspx?JobID={0}", postId),
                },
            };
            mapper.ApplyPostData(jobs[0], jobAd);

            Assert.IsTrue(jobAd.Visibility.HideCompany);
            Assert.AreEqual("MOKA FOODS", jobAd.Description.CompanyName);
            Assert.IsTrue(jobAd.Visibility.HideContactDetails);
            Assert.AreEqual("MOKA FOODS", jobAd.ContactDetails.CompanyName);
        }
    }
}