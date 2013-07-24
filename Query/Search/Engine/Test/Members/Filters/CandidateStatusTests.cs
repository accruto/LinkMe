using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Framework.Utility;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Engine.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test.Members.Filters
{
    [TestClass]
    public class CandidateStatusTests
        : FilterTests
    {
        [TestMethod]
        public void CandidateStatusFilterTest()
        {
            // Create content.

            var activelyLooking = Guid.NewGuid();
            var content = new MemberContent { Member = new Member { Id = activelyLooking }, Candidate = new Candidate { Status = CandidateStatus.ActivelyLooking } };
            IndexContent(content);

            var availableNow = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = availableNow }, Candidate = new Candidate { Status = CandidateStatus.AvailableNow } };
            IndexContent(content);

            var unspecified = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = unspecified }, Candidate = new Candidate { Status = CandidateStatus.Unspecified } };
            IndexContent(content);

            // Search without filter.

            var memberQuery = new MemberSearchQuery();
            var results = Search(memberQuery, 0, 10);
            Assert.AreEqual(3, results.MemberIds.Count);

            // Search for all.

            memberQuery = new MemberSearchQuery { CandidateStatusList = new List<CandidateStatus>{ CandidateStatus.AvailableNow, CandidateStatus.ActivelyLooking, CandidateStatus.OpenToOffers, CandidateStatus.NotLooking, CandidateStatus.Unspecified } };
            results = Search(memberQuery, 0, 10);
            Assert.AreEqual(3, results.MemberIds.Count);

            // Search for ActivelyLooking.

            memberQuery = new MemberSearchQuery { CandidateStatusList = new List<CandidateStatus> { CandidateStatus.ActivelyLooking } };
            results = Search(memberQuery, 0, 10);
            Assert.AreEqual(1, results.MemberIds.Count);
            Assert.IsTrue(new[] { activelyLooking }.CollectionEqual(results.MemberIds));

            // Search for ActivelyLooking or Unspecified.

            memberQuery = new MemberSearchQuery { CandidateStatusList = new List<CandidateStatus>{ CandidateStatus.ActivelyLooking, CandidateStatus.Unspecified} };
            results = Search(memberQuery, 0, 10);
            Assert.AreEqual(2, results.MemberIds.Count);
            Assert.IsTrue(new[] { activelyLooking, unspecified }.CollectionEqual(results.MemberIds));
        }
    }
}