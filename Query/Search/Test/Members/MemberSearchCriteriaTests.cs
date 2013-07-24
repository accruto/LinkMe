using LinkMe.Domain;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members
{
    [TestClass]
    public class MemberSearchCriteriaTests
        : TestClass
    {
        [TestMethod]
        public void TestSortCriteria()
        {
            var criteria = new MemberSearchCriteria();
            Assert.AreEqual(MemberSortOrder.Relevance, criteria.SortCriteria.SortOrder);
            Assert.AreEqual(false, criteria.SortCriteria.ReverseSortOrder);

            criteria.SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.Availability };
            Assert.AreEqual(MemberSortOrder.Availability, criteria.SortCriteria.SortOrder);
            Assert.AreEqual(false, criteria.SortCriteria.ReverseSortOrder);

            criteria.SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.Relevance, ReverseSortOrder = true };
            Assert.AreEqual(MemberSortOrder.Relevance, criteria.SortCriteria.SortOrder);
            Assert.AreEqual(true, criteria.SortCriteria.ReverseSortOrder);

            criteria.SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.Availability, ReverseSortOrder = true };
            Assert.AreEqual(MemberSortOrder.Availability, criteria.SortCriteria.SortOrder);
            Assert.AreEqual(true, criteria.SortCriteria.ReverseSortOrder);
        }

        [TestMethod]
        public void TestCandidateStatus()
        {
            var criteria = new MemberSearchCriteria { CandidateStatusFlags = null };
            Assert.IsNull(criteria.CandidateStatusFlags);

            criteria.CandidateStatusFlags = CandidateStatusFlags.OpenToOffers;
            Assert.IsNotNull(criteria.CandidateStatusFlags);
            Assert.AreEqual(CandidateStatusFlags.OpenToOffers, criteria.CandidateStatusFlags);

            criteria.CandidateStatusFlags = CandidateStatusFlags.OpenToOffers | CandidateStatusFlags.AvailableNow;
            Assert.IsNotNull(criteria.CandidateStatusFlags);
            Assert.AreEqual(CandidateStatusFlags.OpenToOffers | CandidateStatusFlags.AvailableNow, criteria.CandidateStatusFlags);

            criteria.CandidateStatusFlags = CandidateStatusFlags.All;
            Assert.AreNotEqual(CandidateStatusFlags.All, criteria.CandidateStatusFlags);
            Assert.IsNull(criteria.CandidateStatusFlags);
        }

        [TestMethod]
        public void TestCandidateStatusEquality()
        {
            var criteria1 = new MemberSearchCriteria { CandidateStatusFlags = null };
            var criteria2 = new MemberSearchCriteria { CandidateStatusFlags = null };
            Assert.AreEqual(criteria1, criteria2);

            criteria1.CandidateStatusFlags = CandidateStatusFlags.OpenToOffers;
            criteria2.CandidateStatusFlags = null;
            Assert.AreNotEqual(criteria1, criteria2);

            criteria1.CandidateStatusFlags = CandidateStatusFlags.OpenToOffers;
            criteria2.CandidateStatusFlags = CandidateStatusFlags.OpenToOffers;
            Assert.AreEqual(criteria1, criteria2);

            criteria1.CandidateStatusFlags = CandidateStatusFlags.OpenToOffers;
            criteria2.CandidateStatusFlags = CandidateStatusFlags.AvailableNow;
            Assert.AreNotEqual(criteria1, criteria2);

            criteria1.CandidateStatusFlags = CandidateStatusFlags.OpenToOffers | CandidateStatusFlags.ActivelyLooking;
            criteria2.CandidateStatusFlags = CandidateStatusFlags.ActivelyLooking;
            Assert.AreNotEqual(criteria1, criteria2);

            criteria1.CandidateStatusFlags = CandidateStatusFlags.OpenToOffers | CandidateStatusFlags.ActivelyLooking;
            criteria2.CandidateStatusFlags = CandidateStatusFlags.OpenToOffers | CandidateStatusFlags.ActivelyLooking;
            Assert.AreEqual(criteria1, criteria2);

            criteria1.CandidateStatusFlags = CandidateStatusFlags.OpenToOffers | CandidateStatusFlags.ActivelyLooking;
            criteria2.CandidateStatusFlags = CandidateStatusFlags.All;
            Assert.AreNotEqual(criteria1, criteria2);

            criteria1.CandidateStatusFlags = CandidateStatusFlags.All;
            criteria2.CandidateStatusFlags = CandidateStatusFlags.All;
            Assert.AreEqual(criteria1, criteria2);

            // These 2 are considered equal as they both mean include everyone.

            criteria1.CandidateStatusFlags = null;
            criteria2.CandidateStatusFlags = CandidateStatusFlags.All;
            Assert.AreEqual(criteria1, criteria2);
        }
    }
}
