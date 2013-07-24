using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Web
{
    [TestClass]
    public class FeatureBoostTests
        : DisplayTests
    {
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IEmployerJobAdsCommand _employerJobAdsCommand = Resolve<IEmployerJobAdsCommand>();

        [TestMethod]
        public void TestIsHighlighted()
        {
            var jobAd = CreateJobAd(true);
            Assert.AreEqual(JobAdFeatures.Highlight, jobAd.Features);

            Search(Keywords);
            var node = GetResult(jobAd.Id);

            Assert.AreEqual("jobad-list-view row anonymous featured", node.Attributes["class"].Value);

            var model = ApiSearch(Keywords);
            Assert.AreEqual(1, model.JobAds.Count);
            Assert.IsTrue(model.JobAds[0].IsHighlighted);
        }

        [TestMethod]
        public void TestIsNotHighlighted()
        {
            var jobAd = CreateJobAd(false);
            Assert.AreEqual(JobAdFeatures.None, jobAd.Features);

            Search(Keywords);
            var node = GetResult(jobAd.Id);

            Assert.AreEqual("jobad-list-view row anonymous ", node.Attributes["class"].Value);

            var model = ApiSearch(Keywords);
            Assert.AreEqual(1, model.JobAds.Count);
            Assert.IsFalse(model.JobAds[0].IsHighlighted);
        }

        private JobAd CreateJobAd(bool isHighlighted)
        {
            var employer = CreateEmployer(0);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, OwnerId = employer.Id, InitialQuantity = null });
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, OwnerId = employer.Id, InitialQuantity = null });

            var jobAd = employer.CreateTestJobAd(Keywords);
            jobAd.Features = isHighlighted ? JobAdFeatures.Highlight : JobAdFeatures.None;
            _employerJobAdsCommand.CreateJobAd(employer, jobAd);
            _employerJobAdsCommand.OpenJobAd(employer, jobAd, false);
            return jobAd;
        }
    }
}
