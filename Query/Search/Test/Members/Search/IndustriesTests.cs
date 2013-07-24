using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Search
{
    [TestClass]
    public class IndustriesTests
        : ExecuteMemberSearchTests
    {
        private static readonly IIndustriesQuery IndustriesQuery = Resolve<IIndustriesQuery>();

        private static Industry _accounting;
        private static Industry _engineering;
        private static Industry _other;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _accounting = IndustriesQuery.GetIndustry("Accounting");
            _engineering = IndustriesQuery.GetIndustry("Engineering");
            _other = IndustriesQuery.GetIndustry("Other");
        }

        [TestMethod]
        public void TestNoIndustries()
        {
            // Various industries.

            var memberNone = CreateMember(0, null);
            var memberOne = CreateMember(1, _accounting);
            var memberMultiple = CreateMember(2, _accounting, _engineering, _other);
            var memberAll = CreateMember(3, IndustriesQuery.GetIndustries().ToArray());

            // Search.

            var criteria = new MemberSearchCriteria { IndustryIds = null };
            var execution = _executeMemberSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(4, execution.Results.TotalMatches);
            Assert.IsTrue(execution.Results.MemberIds.CollectionEqual(new[] { memberNone.Id, memberOne.Id, memberMultiple.Id, memberAll.Id }));
            AssertHits(execution.Results.IndustryHits, new[] { _accounting, _engineering, _other }, new[] { 3, 2, 3 }, 1);
        }

        [TestMethod]
        public void TestAllIndustries()
        {
            // Various industries.

            var memberNone = CreateMember(0, null);
            var memberOne = CreateMember(1, _accounting);
            var memberMultiple = CreateMember(2, _accounting, _engineering, _other);
            var memberAll = CreateMember(3, IndustriesQuery.GetIndustries().ToArray());

            // Search.

            var criteria = new MemberSearchCriteria { IndustryIds = IndustriesQuery.GetIndustries().Select(i => i.Id).ToList() };
            var execution = _executeMemberSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(4, execution.Results.TotalMatches);
            Assert.IsTrue(execution.Results.MemberIds.CollectionEqual(new[] { memberNone.Id, memberOne.Id, memberMultiple.Id, memberAll.Id }));
            AssertHits(execution.Results.IndustryHits, new[] { _accounting, _engineering, _other }, new[] { 3, 2, 3 }, 1);
        }

        [TestMethod]
        public void TestFirstIndustry()
        {
            // Various industries.

            /*var memberNone =*/ CreateMember(0, null);
            var memberOne = CreateMember(1, _accounting);
            var memberMultiple = CreateMember(2, _accounting, _engineering, _other);
            var memberAll = CreateMember(3, IndustriesQuery.GetIndustries().ToArray());

            // Search.

            var criteria = new MemberSearchCriteria { IndustryIds = new[] { _accounting.Id } };
            var execution = _executeMemberSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(3, execution.Results.TotalMatches);
            Assert.IsTrue(execution.Results.MemberIds.CollectionEqual(new[] { memberOne.Id, memberMultiple.Id, memberAll.Id }));
            AssertHits(execution.Results.IndustryHits, new[] { _accounting, _engineering, _other }, new[] { 3, 2, 3 }, 1);
        }

        [TestMethod]
        public void TestSecondIndustry()
        {
            // Various industries.

            /*var memberNone =*/ CreateMember(0, null);
            /*var memberOne =*/ CreateMember(1, _accounting);
            var memberMultiple = CreateMember(2, _accounting, _engineering, _other);
            var memberAll = CreateMember(3, IndustriesQuery.GetIndustries().ToArray());

            // Search.

            var criteria = new MemberSearchCriteria { IndustryIds = new[] { _engineering.Id } };
            var execution = _executeMemberSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(2, execution.Results.TotalMatches);
            Assert.IsTrue(execution.Results.MemberIds.CollectionEqual(new[] { memberMultiple.Id, memberAll.Id }));
            AssertHits(execution.Results.IndustryHits, new[] { _accounting, _engineering, _other }, new[] { 3, 2, 3 }, 1);
        }

        [TestMethod]
        public void TestMultipleIndustries()
        {
            // Various industries.

            /*var memberNone =*/ CreateMember(0, null);
            var memberOne = CreateMember(1, _accounting);
            var memberMultiple = CreateMember(2, _accounting, _engineering, _other);
            var memberAll = CreateMember(3, IndustriesQuery.GetIndustries().ToArray());

            // Search.

            var criteria = new MemberSearchCriteria { IndustryIds = new[] { _accounting.Id, _engineering.Id } };
            var execution = _executeMemberSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(3, execution.Results.TotalMatches);
            Assert.IsTrue(execution.Results.MemberIds.CollectionEqual(new[] { memberOne.Id, memberMultiple.Id, memberAll.Id }));
            AssertHits(execution.Results.IndustryHits, new[] { _accounting, _engineering, _other }, new[] { 3, 2, 3 }, 1);
        }

        [TestMethod]
        public void TestOtherIndustry()
        {
            // Various industries.

            var memberNone = CreateMember(0, null);
            /*var memberOne =*/ CreateMember(1, _accounting);
            var memberMultiple = CreateMember(2, _accounting, _engineering, _other);
            var memberAll = CreateMember(3, IndustriesQuery.GetIndustries().ToArray());

            // Search.

            var criteria = new MemberSearchCriteria { IndustryIds = new[] { _other.Id } };
            var execution = _executeMemberSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(3, execution.Results.TotalMatches);
            Assert.IsTrue(execution.Results.MemberIds.CollectionEqual(new[] { memberNone.Id, memberMultiple.Id, memberAll.Id }));
            AssertHits(execution.Results.IndustryHits, new[] { _accounting, _engineering, _other }, new[] { 3, 2, 3 }, 1);
        }

        private static void AssertHits(IEnumerable<KeyValuePair<Guid, int>> hits, IList<Industry> industries, IList<int> expectedHits, int exceptExpectedHits)
        {
            // Check specific hits.

            for (var index = 0; index < industries.Count;  ++index)
                AssertHits(hits, industries[index].Id, expectedHits[index]);

            // Check all industries not passed in.

            foreach (var industry in (from i in IndustriesQuery.GetIndustries() select i.Id).Except(from i in industries select i.Id))
                AssertHits(hits, industry, exceptExpectedHits);
        }

        private static void AssertHits(IEnumerable<KeyValuePair<Guid, int>> hits, Guid industryId, int expectedHits)
        {
            Assert.AreEqual(expectedHits, (from h in hits where h.Key == industryId select h.Value).Single());
        }

        private Member CreateMember(int index, params Industry[] industries)
        {
            return CreateMember(index, (m, c, r) => c.Industries = industries);
        }
    }
}