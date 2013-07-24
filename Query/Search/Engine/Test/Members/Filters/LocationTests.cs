using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Engine.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test.Members.Filters
{
    [TestClass]
    public class LocationTests
        : FilterTests
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();

        [TestMethod]
        public void TestRelocationCountry()
        {
            // Member is located in an unstructured location in Australia, and is willing to relocate to Australia.

            var content = new MemberContent
            {
                Member = new Member
                {
                    Id = Guid.NewGuid(),
                    Address = new Address { Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "Winnipeg MB r3e 1w1") },
                },
                Candidate = new Candidate
                {
                    RelocationPreference = RelocationPreference.Yes,
                    RelocationLocations = new List<LocationReference>
                    {
                        _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), null),
                    },
                }
            };
            IndexContent(content);

            // Search in Mackay.

            var memberQuery = new MemberSearchQuery
            {
                Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "Mackay QLD 4740"),
                Distance = 5,
                IncludeRelocating = false,
            };
            var results = Search(memberQuery, 0, 10);
            Assert.AreEqual(0, results.MemberIds.Count);

            memberQuery = new MemberSearchQuery
            {
                Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "Mackay QLD 4740"),
                Distance = 5,
                IncludeRelocating = true,
            };
            results = Search(memberQuery, 0, 10);
            Assert.AreEqual(1, results.MemberIds.Count);
        }
    }
}
