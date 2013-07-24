using LinkMe.Apps.Agents.Test.Users;
using LinkMe.Apps.Integration.Test.SoapJobG8;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Integration.Test.JobG8.WebServices
{
    [TestClass]
    public class DeleteAdvertTests
        : WebServiceTests
    {
        private const string Position = "Archeologist";

        [TestMethod]
        public void TestDeleteAdvert()
        {
            var employer = CreateEmployer();

            // Post.

            var request = CreateRequest(employer, Position);
            var response = CreateService().PostAdvert(request);
            Assert.AreEqual("", response.Success);

            // Find the job ad.

            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Open));
            AssertJobAds(request, jobAds);

            jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Closed));
            Assert.AreEqual(0, jobAds.Count);

            jobAds = Search(Position);
            AssertJobAds(request, jobAds);

            // Delete it.

            var deleteRequest = CreateRequest(employer, request);
            var deleteResponse = CreateService().DeleteAdvert(deleteRequest);
            Assert.AreEqual("", deleteResponse.Success);

            // Find the job ad.

            jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Open));
            Assert.AreEqual(0, jobAds.Count);

            jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Closed));
            AssertJobAds(request, jobAds);

            jobAds = Search(Position);
            Assert.AreEqual(0, jobAds.Count);
        }

        private static DeleteAdvertRequest CreateRequest(IUser employer, PostAdvertRequest request)
        {
            return new DeleteAdvertRequest
            {
                Adverts = new DeleteAdvertRequestAdverts
                {
                    AccountNumber = employer.GetLoginId(),
                    DeleteAdvert = new[]
                    {
                        new DeleteAdvertRequestAdvertsDeleteAdvert
                        {
                            JobReference   = request.Adverts.PostAdvert[0].JobReference,
                        }
                    }
                }
            };
        }
    }
}