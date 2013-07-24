using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Framework.Utility;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Engine.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test.Members.Filters
{
    [TestClass]
    public class CommunityTests
        : FilterTests
    {
        [TestMethod]
        public void CommunityFilterTest()
        {
            var community = Guid.NewGuid();

            // Create content.

            var communal = Guid.NewGuid();
            var content = new MemberContent { Member = new Member { Id = communal, AffiliateId = community }, Candidate = new Candidate() };
            IndexContent(content);

            var independent = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = independent }, Candidate = new Candidate() };
            IndexContent(content);

            // Search without filter.

            var memberQuery = new MemberSearchQuery();
            var results = Search(memberQuery, 0, 10);
            Assert.AreEqual(2, results.MemberIds.Count);

            // Search for community.

            memberQuery = new MemberSearchQuery { CommunityId = community};
            results = Search(memberQuery, 0, 10);
            Assert.AreEqual(1, results.MemberIds.Count);
            Assert.IsTrue(new[] { communal }.CollectionEqual(results.MemberIds));
        }
    }
}