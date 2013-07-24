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
    public class DesiredJobTypesTests
        : FilterTests
    {
        [TestMethod]
        public void DesiredJobTypesFilterTest()
        {
            // Create content.

            var fullTime = Guid.NewGuid();
            var content = new MemberContent { Member = new Member { Id = fullTime }, Candidate = new Candidate { DesiredJobTypes = JobTypes.FullTime} };
            IndexContent(content);

            var partTime = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = partTime }, Candidate = new Candidate { DesiredJobTypes = JobTypes.PartTime } };
            IndexContent(content);

            var contract = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = contract }, Candidate = new Candidate { DesiredJobTypes = JobTypes.Contract } };
            IndexContent(content);

            var fullOrPartTime = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = fullOrPartTime }, Candidate = new Candidate { DesiredJobTypes = JobTypes.FullTime | JobTypes.PartTime } };
            IndexContent(content);

            var all = Guid.NewGuid();
            content = new MemberContent { Member = new Member { Id = all }, Candidate = new Candidate { DesiredJobTypes = JobTypes.All } };
            IndexContent(content);

            // Search without filter.

            var memberQuery = new MemberSearchQuery();
            var results = Search(memberQuery, 0, 10);
            Assert.AreEqual(5, results.MemberIds.Count);

            // Search for FullTime.

            memberQuery = new MemberSearchQuery { DesiredJobTypes = JobTypes.FullTime };
            results = Search(memberQuery, 0, 10);
            Assert.AreEqual(3, results.MemberIds.Count);
            Assert.IsTrue(new[] { fullTime, fullOrPartTime, all }.CollectionEqual(results.MemberIds));

            // Search for Contract.

            memberQuery = new MemberSearchQuery { DesiredJobTypes = JobTypes.Contract };
            results = Search(memberQuery, 0, 10);
            Assert.AreEqual(2, results.MemberIds.Count);
            Assert.IsTrue(new[] { contract, all }.CollectionEqual(results.MemberIds));

            // Search for FullTime or Contract.

            memberQuery = new MemberSearchQuery { DesiredJobTypes = JobTypes.FullTime | JobTypes.Contract };
            results = Search(memberQuery, 0, 10);
            Assert.AreEqual(4, results.MemberIds.Count);
            Assert.IsTrue(new[] { fullTime, contract, fullOrPartTime, all }.CollectionEqual(results.MemberIds));
        }
    }
}