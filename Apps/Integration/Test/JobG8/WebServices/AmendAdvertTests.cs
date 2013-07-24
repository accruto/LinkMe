using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Integration.Test.SoapJobG8;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Integration.Test.JobG8.WebServices
{
    [TestClass]
    public class AmendAdvertTests
        : WebServiceTests
    {
        private const string Position = "Archeologist";
        private const string NewPosition = "Librarian";

        [TestMethod]
        public void TestAmendAdvert()
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

            // Amend it.

            var amendRequest = CreateRequest(employer, request, NewPosition);
            var amendResponse = CreateService().AmendAdvert(amendRequest);
            Assert.AreEqual("", amendResponse.Success);

            // Find the job ad.

            request.Adverts.PostAdvert[0].Position = NewPosition;

            jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Open));
            AssertJobAds(request, jobAds);

            jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Closed));
            Assert.AreEqual(0, jobAds.Count);

            jobAds = Search(Position);
            Assert.AreEqual(0, jobAds.Count);

            jobAds = Search(NewPosition);
            AssertJobAds(request, jobAds);
        }

        protected AmendAdvertRequest CreateRequest(Employer employer, PostAdvertRequest request, string position)
        {
            return new AmendAdvertRequest
            {
                Adverts = new AmendAdvertRequestAdverts
                {
                    AccountNumber = employer.GetLoginId(),
                    AmendAdvert = new[]
                    {
                        new AmendAdvertRequestAdvertsAmendAdvert
                        {
                            JobReference = request.Adverts.PostAdvert[0].JobReference,
                            Position = position,
                            Description = request.Adverts.PostAdvert[0].Description,
                            Location = request.Adverts.PostAdvert[0].Location,
                            Area = request.Adverts.PostAdvert[0].Area,
                            PostCode = request.Adverts.PostAdvert[0].PostCode,
                            Country = request.Adverts.PostAdvert[0].Country,
                            VisaRequired = (AmendAdvertRequestAdvertsAmendAdvertVisaRequired) request.Adverts.PostAdvert[0].VisaRequired,
                            PayPeriod = (AmendAdvertRequestAdvertsAmendAdvertPayPeriod) request.Adverts.PostAdvert[0].PayPeriod,
                            PayAmount = request.Adverts.PostAdvert[0].PayAmount,
                            PayAmountSpecified = request.Adverts.PostAdvert[0].PayAmountSpecified,
                            Currency = request.Adverts.PostAdvert[0].Currency,
                            Contact = request.Adverts.PostAdvert[0].Contact,
                            EmploymentType = (AmendAdvertRequestAdvertsAmendAdvertEmploymentType) request.Adverts.PostAdvert[0].EmploymentType,
                            WorkHoursSpecified = request.Adverts.PostAdvert[0].WorkHoursSpecified,
                            WorkHours = (AmendAdvertRequestAdvertsAmendAdvertWorkHours) request.Adverts.PostAdvert[0].WorkHours,
                        }
                    }
                }
            };
        }
    }
}