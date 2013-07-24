using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Candidates.Properties
{
    [TestClass]
    public class RelocationTests
        : PropertiesTests
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private LocationReference _armadale;
        private LocationReference _malvern;
        private LocationReference _tasmania;

        [TestInitialize]
        public void RelocationTestsInitialize()
        {
            var australia = _locationQuery.GetCountry("Australia");
            _tasmania = new LocationReference(_locationQuery.GetCountrySubdivision(australia, "TAS"));
            _armadale = new LocationReference(_locationQuery.GetPostalCode(australia, "3143"));
            _malvern = new LocationReference(_locationQuery.GetPostalSuburb(_locationQuery.GetPostalCode(australia, "3144"), "Malvern"));
        }

        [TestMethod]
        public void TestCreateRelocations()
        {
            var candidate0 = new Candidate
            {
                Id = Guid.NewGuid(),
                RelocationLocations = null,
            };
            _candidatesCommand.CreateCandidate(candidate0);

            var candidate1 = new Candidate
            {
                Id = Guid.NewGuid(),
                RelocationLocations = new List<LocationReference>
                {
                    _armadale.Clone(),
                    _malvern.Clone(),
                }
            };
            _candidatesCommand.CreateCandidate(candidate1);

            var candidate2 = new Candidate
            {
                Id = Guid.NewGuid(),
                RelocationLocations = new List<LocationReference>
                {
                    _tasmania.Clone()
                }
            };
            _candidatesCommand.CreateCandidate(candidate2);

            AssertRelocationLocations(candidate0.Id);
            AssertRelocationLocations(candidate1.Id, _armadale, _malvern);
            AssertRelocationLocations(candidate2.Id, _tasmania);
            AssertRelocationLocations(candidate0.Id, new LocationReference[0], candidate1.Id, new[] { _armadale, _malvern });
            AssertRelocationLocations(candidate0.Id, new LocationReference[0], candidate2.Id, new[] { _tasmania });
            AssertRelocationLocations(candidate1.Id, new[] { _armadale, _malvern }, candidate2.Id, new[] { _tasmania });
        }

        [TestMethod]
        public void TestUpdateCreateRelocations()
        {
            var candidate = new Candidate { Id = Guid.NewGuid() };
            _candidatesCommand.CreateCandidate(candidate);
            AssertRelocationLocations(candidate.Id);

            candidate.RelocationLocations = new List<LocationReference>
            {
                _armadale.Clone(),
                _malvern.Clone(),
                _tasmania.Clone()
            };

            _candidatesCommand.UpdateCandidate(candidate);
            AssertRelocationLocations(candidate.Id, _armadale, _malvern, _tasmania);
        }

        [TestMethod]
        public void TestUpdateDeleteRelocations()
        {
            var candidate = new Candidate
            {
                Id = Guid.NewGuid(),
                RelocationLocations = new List<LocationReference>
                {
                    _armadale.Clone(),
                    _malvern.Clone(),
                    _tasmania.Clone()
                },
            };
            _candidatesCommand.CreateCandidate(candidate);
            AssertRelocationLocations(candidate.Id, _armadale, _malvern, _tasmania);

            candidate.RelocationLocations = null;
            _candidatesCommand.UpdateCandidate(candidate);
            AssertRelocationLocations(candidate.Id);
        }

        [TestMethod]
        public void TestUpdateRelocations()
        {
            var candidate = new Candidate
            {
                Id = Guid.NewGuid(),
                RelocationLocations = new List<LocationReference>
                {
                    _armadale.Clone(),
                    _malvern.Clone(),
                },
            };
            _candidatesCommand.CreateCandidate(candidate);
            AssertRelocationLocations(candidate.Id, _armadale, _malvern);

            candidate.RelocationLocations = new List<LocationReference>
            {
                _tasmania.Clone()
            };
            _candidatesCommand.UpdateCandidate(candidate);
            AssertRelocationLocations(candidate.Id, _tasmania);
        }

        private void AssertRelocationLocations(Guid candidateId, params LocationReference[] expectedLocations)
        {
            var candidate = _candidatesQuery.GetCandidate(candidateId);
            Assert.IsTrue(candidate.RelocationLocations.NullableCollectionEqual(expectedLocations));

            var candidates = _candidatesQuery.GetCandidates(new[] { candidateId });
            Assert.AreEqual(1, candidates.Count);
            Assert.IsTrue(candidates[0].RelocationLocations.NullableCollectionEqual(expectedLocations));
        }

        private void AssertRelocationLocations(Guid candidateId1, IEnumerable<LocationReference> expectedLocations1, Guid candidateId2, IEnumerable<LocationReference> expectedLocations2)
        {
            var candidates = _candidatesQuery.GetCandidates(new[] { candidateId1, candidateId2 });
            Assert.AreEqual(2, candidates.Count);
            Assert.IsTrue((from c in candidates where c.Id == candidateId1 select c).Single().RelocationLocations.NullableCollectionEqual(expectedLocations1));
            Assert.IsTrue((from c in candidates where c.Id == candidateId2 select c).Single().RelocationLocations.NullableCollectionEqual(expectedLocations2));
        }
    }
}
