using System;
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
    public class EthnicStatusTests
        : FilterTests
    {
        [TestMethod]
        public void EthnicStatusFilterTest()
        {
            // Create content.

            var aboriginal = Guid.NewGuid();
            var content = new MemberContent { Member = new Member { Id = aboriginal, EthnicStatus = EthnicStatus.Aboriginal }, Candidate = new Candidate() };
            IndexContent(content);

            var islander = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = islander, EthnicStatus = EthnicStatus.TorresIslander }, Candidate = new Candidate() };
            IndexContent(content);

            var both = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = both, EthnicStatus = EthnicStatus.Aboriginal | EthnicStatus.TorresIslander }, Candidate = new Candidate() };
            IndexContent(content);

            var none = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = none }, Candidate = new Candidate() };
            IndexContent(content);

            // Search without filter.

            var memberQuery = new MemberSearchQuery();
            var results = Search(memberQuery, 0, 10);
            Assert.AreEqual(4, results.MemberIds.Count);

            memberQuery = new MemberSearchQuery { EthnicStatus = default(EthnicStatus) };
            results = Search(memberQuery, 0, 10);
            Assert.AreEqual(4, results.MemberIds.Count);

            // Search for Aboriginal.

            memberQuery = new MemberSearchQuery { EthnicStatus = EthnicStatus.Aboriginal };
            results = Search(memberQuery, 0, 10);
            Assert.AreEqual(2, results.MemberIds.Count);
            Assert.IsTrue(new[] { aboriginal, both }.CollectionEqual(results.MemberIds));

            // Search for Both.

            memberQuery = new MemberSearchQuery { EthnicStatus = EthnicStatus.Aboriginal | EthnicStatus.TorresIslander };
            results = Search(memberQuery, 0, 10);
            Assert.AreEqual(3, results.MemberIds.Count);
            Assert.IsTrue(new[] { aboriginal, islander, both }.CollectionEqual(results.MemberIds));
        }
    }
}