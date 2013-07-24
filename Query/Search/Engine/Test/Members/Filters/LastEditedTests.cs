using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Engine.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test.Members.Filters
{
    [TestClass]
    public class LastEditedTests
        : FilterTests
    {
        [TestMethod]
        public void LastEditedFilterTest()
        {
            // Create content.

            var now = DateTime.Now;

            var resumeOneHourAgo = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = resumeOneHourAgo }, Resume = new Resume { LastUpdatedTime = now.AddHours(-1) }, Candidate = new Candidate() });

            var resumeTwoDaysAgo = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = resumeTwoDaysAgo }, Resume = new Resume { LastUpdatedTime = now.AddDays(-2) }, Candidate = new Candidate() });

            var candidateOneHourAgo = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = candidateOneHourAgo }, Candidate = new Candidate { LastUpdatedTime = now.AddHours(-1) } });

            var candidateTwoDaysAgo = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = candidateTwoDaysAgo }, Candidate = new Candidate { LastUpdatedTime = now.AddDays(-2) } });

            var memberOneHourAgo = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = memberOneHourAgo, CreatedTime = now.AddHours(-1), LastUpdatedTime = now.AddHours(-1) }, Candidate = new Candidate() });

            var memberTwoDaysAgo = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = memberTwoDaysAgo, CreatedTime = now.AddDays(-2), LastUpdatedTime = now.AddDays(-2) }, Candidate = new Candidate() });

            var memberUpdatedOneHourAgo = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = memberUpdatedOneHourAgo, LastUpdatedTime = now.AddHours(-1) }, Candidate = new Candidate() });

            var memberUpdatedTwoDaysAgo = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = memberUpdatedTwoDaysAgo, LastUpdatedTime = now.AddDays(-2) }, Candidate = new Candidate() });

            var never = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = never }, Candidate = new Candidate() });

            // Search without filter.

            var memberQuery = new MemberSearchQuery();
            var results = Search(memberQuery, 0, 10);
            Assert.AreEqual(9, results.MemberIds.Count);

            // Search for modified during the last day.

            memberQuery = new MemberSearchQuery { Recency = new TimeSpan(1, 0, 0, 0) };
            results = Search(memberQuery, 0, 10);
            Assert.AreEqual(4, results.MemberIds.Count);
            Assert.IsTrue(new[] { resumeOneHourAgo, candidateOneHourAgo, memberOneHourAgo, memberUpdatedOneHourAgo }.CollectionEqual(results.MemberIds));
        }
    }
}