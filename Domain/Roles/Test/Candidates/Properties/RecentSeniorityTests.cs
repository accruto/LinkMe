using System;
using System.Linq;
using LinkMe.Domain.Roles.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Candidates.Properties
{
    [TestClass]
    public class RecentSeniorityTests
        : PropertiesTests
    {
        [TestMethod]
        public void TestCreateRecentSeniority()
        {
            var candidate1 = new Candidate
            {
                Id = Guid.NewGuid(),
                RecentSeniority = Seniority.Student,
            };
            _candidatesCommand.CreateCandidate(candidate1);

            var candidate2 = new Candidate
            {
                Id = Guid.NewGuid(),
                RecentSeniority = Seniority.Internship,
            };
            _candidatesCommand.CreateCandidate(candidate2);

            AssertRecentSeniority(candidate1.Id, candidate1.RecentSeniority);
            AssertRecentSeniority(candidate2.Id, candidate2.RecentSeniority);
            AssertRecentSeniority(candidate1.Id, candidate1.RecentSeniority, candidate2.Id, candidate2.RecentSeniority);
        }

        [TestMethod]
        public void TestUpdateRecentSeniority()
        {
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            AssertRecentSeniority(candidate.Id, null);

            candidate.RecentSeniority = Seniority.Executive;
            _candidatesCommand.UpdateCandidate(candidate);
            AssertRecentSeniority(candidate.Id, candidate.RecentSeniority);

            candidate.RecentSeniority = null;
            _candidatesCommand.UpdateCandidate(candidate);
            AssertRecentSeniority(candidate.Id, candidate.RecentSeniority);
        }

        private void AssertRecentSeniority(Guid candidateId, Seniority? expectedSeniority)
        {
            var candidate = _candidatesQuery.GetCandidate(candidateId);
            Assert.AreEqual(expectedSeniority, candidate.RecentSeniority);

            var candidates = _candidatesQuery.GetCandidates(new[] { candidateId });
            Assert.AreEqual(1, candidates.Count);
            Assert.AreEqual(expectedSeniority, candidates[0].RecentSeniority);
        }

        private void AssertRecentSeniority(Guid candidateId1, Seniority? expectedRecentSeniority1, Guid candidateId2, Seniority? expectedRecentSeniority2)
        {
            var candidates = _candidatesQuery.GetCandidates(new[] { candidateId1, candidateId2 });
            Assert.AreEqual(2, candidates.Count);
            Assert.AreEqual(expectedRecentSeniority1, (from c in candidates where c.Id == candidateId1 select c).Single().RecentSeniority);
            Assert.AreEqual(expectedRecentSeniority2, (from c in candidates where c.Id == candidateId2 select c).Single().RecentSeniority);
        }
    }
}
