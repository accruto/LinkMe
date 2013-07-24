using System;
using System.Linq;
using LinkMe.Domain.Roles.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Candidates.Properties
{
    [TestClass]
    public class VisaStatusTests
        : PropertiesTests
    {
        [TestMethod]
        public void TestCreateVisaStatus()
        {
            var candidate1 = new Candidate
            {
                Id = Guid.NewGuid(),
                VisaStatus = VisaStatus.Citizen,
            };
            _candidatesCommand.CreateCandidate(candidate1);

            var candidate2 = new Candidate
            {
                Id = Guid.NewGuid(),
                VisaStatus = VisaStatus.RestrictedWorkVisa,
            };
            _candidatesCommand.CreateCandidate(candidate2);

            AssertVisaStatus(candidate1.Id, candidate1.VisaStatus);
            AssertVisaStatus(candidate2.Id, candidate2.VisaStatus);
            AssertVisaStatus(candidate1.Id, candidate1.VisaStatus, candidate2.Id, candidate2.VisaStatus);
        }

        [TestMethod]
        public void TestUpdateVisaStatus()
        {
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            AssertVisaStatus(candidate.Id, null);

            candidate.VisaStatus = VisaStatus.UnrestrictedWorkVisa;
            _candidatesCommand.UpdateCandidate(candidate);
            AssertVisaStatus(candidate.Id, candidate.VisaStatus);

            candidate.VisaStatus = VisaStatus.NotApplicable;
            _candidatesCommand.UpdateCandidate(candidate);
            AssertVisaStatus(candidate.Id, candidate.VisaStatus);
        }

        private void AssertVisaStatus(Guid candidateId, VisaStatus? expectedVisaStatus)
        {
            var candidate = _candidatesQuery.GetCandidate(candidateId);
            Assert.AreEqual(expectedVisaStatus, candidate.VisaStatus);

            var candidates = _candidatesQuery.GetCandidates(new[] { candidateId });
            Assert.AreEqual(1, candidates.Count);
            Assert.AreEqual(expectedVisaStatus, candidates[0].VisaStatus);
        }

        private void AssertVisaStatus(Guid candidateId1, VisaStatus? expectedVisaStatus1, Guid candidateId2, VisaStatus? expectedVisaStatus2)
        {
            var candidates = _candidatesQuery.GetCandidates(new[] { candidateId1, candidateId2 });
            Assert.AreEqual(2, candidates.Count);
            Assert.AreEqual(expectedVisaStatus1, (from c in candidates where c.Id == candidateId1 select c).Single().VisaStatus);
            Assert.AreEqual(expectedVisaStatus2, (from c in candidates where c.Id == candidateId2 select c).Single().VisaStatus);
        }
    }
}
