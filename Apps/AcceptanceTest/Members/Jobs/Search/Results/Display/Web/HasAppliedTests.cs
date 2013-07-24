using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Web
{
    [TestClass]
    public class HasAppliedTests
        : DisplayTests
    {
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();

        [TestMethod]
        public void TestHasApplied()
        {
            var member = CreateMember(0);
            var jobAd = CreateJobAd();

            var application = new InternalApplication { ApplicantId = member.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);

            LogIn(member);
            Search(Keywords);
            var node = GetResult(jobAd.Id);

            Assert.IsNotNull(node.SelectSingleNode(".//div[@class='icon applied active']"));
            Assert.IsNull(node.SelectSingleNode(".//div[@class='icon applied ']"));

            var model = ApiSearch(Keywords);
            Assert.AreEqual(1, model.JobAds.Count);
            Assert.IsTrue(model.JobAds[0].HasApplied);
        }

        [TestMethod]
        public void TestHasNotApplied()
        {
            var member = CreateMember(0);
            var jobAd = CreateJobAd();

            LogIn(member);
            Search(Keywords);
            var node = GetResult(jobAd.Id);

            Assert.IsNull(node.SelectSingleNode(".//div[@class='icon applied active']"));
            Assert.IsNotNull(node.SelectSingleNode(".//div[@class='icon applied ']"));

            var model = ApiSearch(Keywords);
            Assert.AreEqual(1, model.JobAds.Count);
            Assert.IsFalse(model.JobAds[0].HasApplied);
        }

        private JobAd CreateJobAd()
        {
            var employer = CreateEmployer(0);
            var jobAd = employer.CreateTestJobAd(Keywords);
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            return jobAd;
        }
    }
}
