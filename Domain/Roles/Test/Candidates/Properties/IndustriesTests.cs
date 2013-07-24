using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Candidates.Properties
{
    [TestClass]
    public class IndustriesTests
        : PropertiesTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private Industry _engineering;
        private Industry _accounting;
        private Industry _construction;

        [TestInitialize]
        public void IndustriesTestsInitialize()
        {
            _engineering = _industriesQuery.GetIndustry("Engineering");
            _accounting = _industriesQuery.GetIndustry("Accounting");
            _construction = _industriesQuery.GetIndustry("Construction");
        }

        [TestMethod]
        public void TestCreateIndustries()
        {
            var candidate0 = new Candidate
            {
                Id = Guid.NewGuid(),
                Industries = null,
            };
            _candidatesCommand.CreateCandidate(candidate0);

            var candidate1 = new Candidate
            {
                Id = Guid.NewGuid(),
                Industries = new List<Industry>
                {
                    _engineering,
                    _accounting,
                },
            };
            _candidatesCommand.CreateCandidate(candidate1);

            var candidate2 = new Candidate
            {
                Id = Guid.NewGuid(),
                Industries = new List<Industry>
                {
                    _construction,
                }
            };
            _candidatesCommand.CreateCandidate(candidate2);

            AssertIndustries(candidate0.Id);
            AssertIndustries(candidate1.Id, _engineering, _accounting);
            AssertIndustries(candidate2.Id, _construction);
            AssertIndustries(candidate0.Id, new Industry[0], candidate1.Id, new[] { _engineering, _accounting });
            AssertIndustries(candidate0.Id, new Industry[0], candidate2.Id, new[] { _construction });
            AssertIndustries(candidate1.Id, new[] { _engineering, _accounting }, candidate2.Id, new[] { _construction });
        }

        [TestMethod]
        public void TestUpdateCreateIndustries()
        {
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            AssertIndustries(candidate.Id);

            candidate.Industries = new List<Industry>
            {
                _accounting,
                _engineering,
                _construction,
            };

            _candidatesCommand.UpdateCandidate(candidate);
            AssertIndustries(candidate.Id, _accounting, _engineering, _construction);
        }

        [TestMethod]
        public void TestUpdateDeleteIndustries()
        {
            var candidate = new Candidate
            {
                Id = Guid.NewGuid(),
                Industries = new List<Industry>
                {
                    _engineering,
                    _accounting,
                },
            };
            _candidatesCommand.CreateCandidate(candidate);
            AssertIndustries(candidate.Id, _engineering, _accounting);

            candidate.Industries = null;
            _candidatesCommand.UpdateCandidate(candidate);
            AssertIndustries(candidate.Id);
        }

        [TestMethod]
        public void TestUpdateIndustries()
        {
            var candidate = new Candidate
            {
                Id = Guid.NewGuid(),
                Industries = new List<Industry>
                {
                    _engineering,
                    _accounting,
                },
            };
            _candidatesCommand.CreateCandidate(candidate);
            AssertIndustries(candidate.Id, _engineering, _accounting);

            candidate.Industries = new List<Industry>
            {
                _construction,
            };
            _candidatesCommand.UpdateCandidate(candidate);
            AssertIndustries(candidate.Id, _construction);
        }

        private void AssertIndustries(Guid candidateId1, IEnumerable<Industry> expectedIndustries1, Guid candidateId2, IEnumerable<Industry> expectedIndustries2)
        {
            var candidates = _candidatesQuery.GetCandidates(new[] { candidateId1, candidateId2 });
            Assert.AreEqual(2, candidates.Count);
            Assert.IsTrue((from c in candidates where c.Id == candidateId1 select c).Single().Industries.NullableCollectionEqual(expectedIndustries1));
            Assert.IsTrue((from c in candidates where c.Id == candidateId2 select c).Single().Industries.NullableCollectionEqual(expectedIndustries2));
        }

        private void AssertIndustries(Guid candidateId, params Industry[] expectedIndustries)
        {
            var candidate = _candidatesQuery.GetCandidate(candidateId);
            Assert.IsTrue(candidate.Industries.NullableCollectionEqual(expectedIndustries));

            var candidates = _candidatesQuery.GetCandidates(new[] { candidateId });
            Assert.AreEqual(1, candidates.Count);
            Assert.IsTrue(candidates[0].Industries.NullableCollectionEqual(expectedIndustries));
        }
    }
}
