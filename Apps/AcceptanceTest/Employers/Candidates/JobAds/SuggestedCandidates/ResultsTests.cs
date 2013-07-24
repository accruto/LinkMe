using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.JobAds.SuggestedCandidates
{
    [TestClass]
    public class ResultsTests
        : SuggestedCandidatesTests
    {
        [TestMethod]
        public void TestNoResults()
        {
            var employer = CreateEmployer(0, null);
            var jobAd = CreateJobAd(employer, 0);

            LogIn(employer);

            var url = GetSuggestedCandidatesUrl(jobAd.Id);
            Get(url);
            AssertUrl(url);
            AssertNoCandidates();
            AssertMembers();
        }

        [TestMethod]
        public void TestSomeResults()
        {
            var employer = CreateEmployer(0, null);
            var jobAd = CreateJobAd(employer, 0);

            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index, jobAd.Title);

            LogIn(employer);

            var url = GetSuggestedCandidatesUrl(jobAd.Id);
            Get(url);
            AssertUrl(url);
            AssertMembers(members);
        }

        private Member CreateMember(int index, string title)
        {
            var member = CreateMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = _resumesQuery.GetResume(candidate.ResumeId.Value);
            resume.Jobs[0].Title = title;
            _candidateResumesCommand.UpdateResume(candidate, resume);
            return member;
        }
    }
}
