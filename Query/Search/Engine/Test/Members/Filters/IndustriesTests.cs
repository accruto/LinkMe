using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Framework.Utility;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Engine.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test.Members.Filters
{
    [TestClass]
    public class IndustriesTests
        : FilterTests
    {
        [TestMethod]
        public void IndustryFilterTest()
        {
            var bankingIndustry = Guid.NewGuid();
            var accountingIndustry = Guid.NewGuid();

            // Create content.

            var banking = Guid.NewGuid();
            var content = new MemberContent { Member = new Member { Id = banking }, Candidate = new Candidate { Industries = new[] { new Industry { Id = bankingIndustry } } } };
            IndexContent(content);

            var accounting = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = accounting }, Candidate = new Candidate { Industries = new[] { new Industry { Id = accountingIndustry } } } };
            IndexContent(content);

            var both = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = both }, Candidate = new Candidate { Industries = new[] { new Industry { Id = bankingIndustry }, new Industry { Id = accountingIndustry } } } };
            IndexContent(content);

            var none = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = none }, Candidate = new Candidate() };
            IndexContent(content);

            // Search without filter.

            var memberQuery = new MemberSearchQuery();
            var results = Search(memberQuery, 0, 10);
            Assert.AreEqual(4, results.MemberIds.Count);

            memberQuery = new MemberSearchQuery { IndustryIds = new Guid[0] };
            results = Search(memberQuery, 0, 10);
            Assert.AreEqual(4, results.MemberIds.Count);

            // Search for banking.

            memberQuery = new MemberSearchQuery { IndustryIds = new[] { bankingIndustry }};
            results = Search(memberQuery, 0, 10);
            Assert.AreEqual(2, results.MemberIds.Count);
            Assert.IsTrue(new[] { banking, both }.CollectionEqual(results.MemberIds));

            // Search for banking or accounting.

            memberQuery = new MemberSearchQuery { IndustryIds = new[] { bankingIndustry, accountingIndustry } };
            results = Search(memberQuery, 0, 10);
            Assert.AreEqual(3, results.MemberIds.Count);
            Assert.IsTrue(new[] { banking, accounting, both }.CollectionEqual(results.MemberIds));
        }
    }
}