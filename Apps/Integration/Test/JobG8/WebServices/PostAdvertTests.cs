using System.Web.Services.Protocols;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Integration.Test.JobG8.WebServices
{
    [TestClass]
    public class PostAdvertTests
        : WebServiceTests
    {
        private const string Position = "Archeologist";

        [TestMethod]
        public void TestPostAdvert()
        {
            var employer = CreateEmployer();

            // Check does not exist.

            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Open));
            Assert.AreEqual(0, jobAds.Count);

            jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Closed));
            Assert.AreEqual(0, jobAds.Count);

            jobAds = Search(Position);
            Assert.AreEqual(0, jobAds.Count);

            // Post.

            var request = CreateRequest(employer, Position);
            var response = CreateService().PostAdvert(request);
            Assert.AreEqual("", response.Success);

            // Find the job ad.

            jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Open));
            AssertJobAds(request, jobAds);

            jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Closed));
            Assert.AreEqual(0, jobAds.Count);

            jobAds = Search(Position);
            AssertJobAds(request, jobAds);
        }

        [TestMethod]
        public void TestPost2Adverts()
        {
            var employer = CreateEmployer();

            // Check does not exist.

            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Open));
            Assert.AreEqual(0, jobAds.Count);

            jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Closed));
            Assert.AreEqual(0, jobAds.Count);

            jobAds = Search(Position);
            Assert.AreEqual(0, jobAds.Count);

            // Post.

            var request = CreateRequest(employer, Position + "1", Position + "2");
            var response = CreateService().PostAdvert(request);
            Assert.AreEqual("", response.Success);

            // Find the job ad.

            jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Open));
            AssertJobAds(request, jobAds);

            jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Closed));
            Assert.AreEqual(0, jobAds.Count);

            jobAds = Search(Position);
            AssertJobAds(request, jobAds);
        }

        [TestMethod]
        [ExpectedException(typeof(SoapHeaderException), "Web service authorization failed: unknown user 'Bad user'.")]
        public void TestBadUsername()
        {
            var employer = CreateEmployer();
            var request = CreateRequest(employer, Position);
            var service = CreateService();
            service.UserCredentials.Username = "Bad user";
            service.PostAdvert(request);
        }

        [TestMethod]
        [ExpectedException(typeof(SoapHeaderException), "Web service authorization failed: the password for user 'JobG8' is incorrect.")]
        public void TestBadPassword()
        {
            var employer = CreateEmployer();
            var request = CreateRequest(employer, Position);
            var service = CreateService();
            service.UserCredentials.Password = "Bad password";
            service.PostAdvert(request);
        }
    }
}