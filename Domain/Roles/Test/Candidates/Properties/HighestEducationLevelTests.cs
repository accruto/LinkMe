using System;
using System.Linq;
using LinkMe.Domain.Roles.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Candidates.Properties
{
    [TestClass]
    public class HighestEducationLevelTests
        : PropertiesTests
    {
        [TestMethod]
        public void TestCreateHighestEducationLevel()
        {
            var candidate1 = new Candidate
            {
                Id = Guid.NewGuid(),
                HighestEducationLevel = EducationLevel.Doctoral,
            };
            _candidatesCommand.CreateCandidate(candidate1);

            var candidate2 = new Candidate
            {
                Id = Guid.NewGuid(),
                HighestEducationLevel = EducationLevel.HighSchool,
            };
            _candidatesCommand.CreateCandidate(candidate2);

            AssertHighestEducationLevel(candidate1.Id, candidate1.HighestEducationLevel);
            AssertHighestEducationLevel(candidate2.Id, candidate2.HighestEducationLevel);
            AssertHighestEducationLevel(candidate1.Id, candidate1.HighestEducationLevel, candidate2.Id, candidate2.HighestEducationLevel);
        }

        [TestMethod]
        public void TestUpdateHighestEducationLevel()
        {
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            AssertHighestEducationLevel(candidate.Id, null);

            candidate.HighestEducationLevel = EducationLevel.TradeCertificate;
            _candidatesCommand.UpdateCandidate(candidate);
            AssertHighestEducationLevel(candidate.Id, candidate.HighestEducationLevel);

            candidate.HighestEducationLevel = null;
            _candidatesCommand.UpdateCandidate(candidate);
            AssertHighestEducationLevel(candidate.Id, candidate.HighestEducationLevel);
        }

        private void AssertHighestEducationLevel(Guid candidateId, EducationLevel? expectedEducationLevel)
        {
            var candidate = _candidatesQuery.GetCandidate(candidateId);
            Assert.AreEqual(expectedEducationLevel, candidate.HighestEducationLevel);

            var candidates = _candidatesQuery.GetCandidates(new[] { candidateId });
            Assert.AreEqual(1, candidates.Count);
            Assert.AreEqual(expectedEducationLevel, candidates[0].HighestEducationLevel);
        }

        private void AssertHighestEducationLevel(Guid candidateId1, EducationLevel? expectedEducationLevel1, Guid candidateId2, EducationLevel? expectedEducationLevel2)
        {
            var candidates = _candidatesQuery.GetCandidates(new[] { candidateId1, candidateId2 });
            Assert.AreEqual(2, candidates.Count);
            Assert.AreEqual(expectedEducationLevel1, (from c in candidates where c.Id == candidateId1 select c).Single().HighestEducationLevel);
            Assert.AreEqual(expectedEducationLevel2, (from c in candidates where c.Id == candidateId2 select c).Single().HighestEducationLevel);
        }
    }
}
