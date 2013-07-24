using System;
using System.Linq;
using LinkMe.Domain.Roles.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Candidates.Properties
{
    [TestClass]
    public class RecentProfessionTests
        : PropertiesTests
    {
        [TestMethod]
        public void TestCreateRecentProfession()
        {
            var candidate1 = new Candidate
            {
                Id = Guid.NewGuid(),
                RecentProfession = Profession.QualityAssurance,
            };
            _candidatesCommand.CreateCandidate(candidate1);

            var candidate2 = new Candidate
            {
                Id = Guid.NewGuid(),
                RecentProfession = Profession.Administration,
            };
            _candidatesCommand.CreateCandidate(candidate2);

            AssertRecentProfession(candidate1.Id, candidate1.RecentProfession);
            AssertRecentProfession(candidate2.Id, candidate2.RecentProfession);
            AssertRecentProfession(candidate1.Id, candidate1.RecentProfession, candidate2.Id, candidate2.RecentProfession);
        }

        [TestMethod]
        public void TestUpdateRecentProfession()
        {
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            AssertRecentProfession(candidate.Id, null);

            candidate.RecentProfession = Profession.Training;
            _candidatesCommand.UpdateCandidate(candidate);
            AssertRecentProfession(candidate.Id, candidate.RecentProfession);

            candidate.RecentProfession = null;
            _candidatesCommand.UpdateCandidate(candidate);
            AssertRecentProfession(candidate.Id, candidate.RecentProfession);
        }

        private void AssertRecentProfession(Guid candidateId, Profession? expectedProfession)
        {
            var candidate = _candidatesQuery.GetCandidate(candidateId);
            Assert.AreEqual(expectedProfession, candidate.RecentProfession);

            var candidates = _candidatesQuery.GetCandidates(new[] { candidateId });
            Assert.AreEqual(1, candidates.Count);
            Assert.AreEqual(expectedProfession, candidates[0].RecentProfession);
        }

        private void AssertRecentProfession(Guid candidateId1, Profession? expectedRecentProfession1, Guid candidateId2, Profession? expectedRecentProfession2)
        {
            var candidates = _candidatesQuery.GetCandidates(new[] { candidateId1, candidateId2 });
            Assert.AreEqual(2, candidates.Count);
            Assert.AreEqual(expectedRecentProfession1, (from c in candidates where c.Id == candidateId1 select c).Single().RecentProfession);
            Assert.AreEqual(expectedRecentProfession2, (from c in candidates where c.Id == candidateId2 select c).Single().RecentProfession);
        }
    }
}
