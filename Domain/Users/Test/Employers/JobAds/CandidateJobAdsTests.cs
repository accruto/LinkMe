using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Test.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds
{
    [TestClass]
    public class CandidateJobAdsTests
        : JobAdsTests
    {
        private readonly ICandidateBlockListsQuery _candidateBlockListsQuery = Resolve<ICandidateBlockListsQuery>();
        private readonly ICandidateListsCommand _candidateListsCommand = Resolve<ICandidateListsCommand>();

        [TestMethod]
        public void TestCandidateCount()
        {
            const int count = 5;

            var employer = CreateEmployer(1);

            var jobAd = CreateJobAd(employer);
            var list = _jobAdApplicantsQuery.GetApplicantList(employer, jobAd);
            var member1 = _membersCommand.CreateTestMember(1);
            _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, new [] {member1.Id});

            for (var i = 1; i < count; i++)
            {
                var member = CreateMember(i+1);
                _jobAdApplicantsCommand.ShortlistApplicants(employer, jobAd, new[] {member.Id});
            }

            var appListCount = _jobAdApplicantsQuery.GetApplicantCount(employer, list);
            Assert.AreEqual(count, appListCount);

            AssertCounts(employer, list, 0, count, 0);

            //Now block a member
            var tempBlockList = _candidateBlockListsQuery.GetTemporaryBlockList(employer);

            // Add candidate to Temporary Blocklist - should have no effect on folder count
            var tempBlockListCount = _candidateListsCommand.AddCandidateToBlockList(employer, tempBlockList, member1.Id);

            Assert.AreEqual(1, tempBlockListCount);
            AssertCounts(employer, list, 0, count, 0);

            //Now block the member permanently
            var permBlockList = _candidateBlockListsQuery.GetPermanentBlockList(employer);

            // Add candidate to Permanent Blocklist - should have effect on folder count
            var permBlockListCount = _candidateListsCommand.AddCandidateToBlockList(employer, permBlockList, member1.Id);
            var tempBlockListCounts = _candidateBlockListsQuery.GetBlockedCounts(employer);

            //if the tempBlockListId isn't in the list there are zero entries
            Assert.AreEqual(false, tempBlockListCounts.ContainsKey(tempBlockList.Id));
            Assert.AreEqual(1, permBlockListCount);
            AssertCounts(employer, list, 0, count - 1, 0);
        }

        protected Employer CreateEmployer(int index)
        {
            var employer = _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestVerifiedOrganisation(index));
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = employer.Id });
            return employer;
        }

        protected Member CreateMember(int index)
        {
            return _membersCommand.CreateTestMember(index);
        }

        protected JobAd CreateJobAd(IEmployer employer)
        {
            return _jobAdsCommand.PostTestJobAd(employer);
        }
    }
}
