using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Web
{
    [TestClass]
    public class IsFlaggedTests
        : DisplayTests
    {
        private readonly IJobAdFlagListsQuery _jobAdFlagListsQuery = Resolve<IJobAdFlagListsQuery>();
        private readonly IMemberJobAdListsCommand _memberJobAdListsCommand = Resolve<IMemberJobAdListsCommand>();

        [TestMethod]
        public void TestIsFlagged()
        {
            var member = CreateMember(0);
            var jobAd = CreateJobAd();
            var flagList = _jobAdFlagListsQuery.GetFlagList(member);
            _memberJobAdListsCommand.AddJobAdToFlagList(member, flagList, jobAd.Id);

            LogIn(member);
            Search(Keywords);
            var node = GetResult(jobAd.Id);

            Assert.IsNotNull(node.SelectSingleNode(".//div[@class='flag flagged']"));
            Assert.IsNull(node.SelectSingleNode(".//div[@class='flag ']"));

            var model = ApiSearch(Keywords);
            Assert.AreEqual(1, model.JobAds.Count);
            Assert.IsTrue(model.JobAds[0].IsFlagged);
        }

        [TestMethod]
        public void TestIsNotFlagged()
        {
            var member = CreateMember(0);
            var jobAd = CreateJobAd();

            LogIn(member);
            Search(Keywords);
            var node = GetResult(jobAd.Id);

            Assert.IsNull(node.SelectSingleNode(".//div[@class='flag flagged']"));
            Assert.IsNotNull(node.SelectSingleNode(".//div[@class='flag ']"));

            var model = ApiSearch(Keywords);
            Assert.AreEqual(1, model.JobAds.Count);
            Assert.IsFalse(model.JobAds[0].IsFlagged);
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
