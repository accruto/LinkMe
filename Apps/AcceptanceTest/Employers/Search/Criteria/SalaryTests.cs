using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Criteria
{
    [TestClass]
    public class SalaryTests
        : CriteriaTests
    {
        protected override void TestDisplay()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.Salary = null;
            TestDisplay(criteria);

            criteria.Salary = new Salary { LowerBound = 10000, UpperBound = 20000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            TestDisplay(criteria);

            criteria.Salary = new Salary { LowerBound = null, UpperBound = 20000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            TestDisplay(criteria);

            criteria.Salary = new Salary { LowerBound = 20000, UpperBound = null, Currency = Currency.AUD, Rate = SalaryRate.Year };
            TestDisplay(criteria);
        }

        [TestMethod]
        public void TestSalary()
        {
            // Create members.

            var member0 = CreateMember(0, 50000, null);     // Effectively $50000 - $62500
            var member1 = CreateMember(1, 80000, null);     // Effectively $80000 - $100000
            var member2 = CreateMember(2, 50000, 100000);   // Effectively $50000 - $62500
            var member3 = CreateMember(3, 80000, 150000);   // Effectively $80000 - $100000
            var member4 = CreateMember(4, null, 100000);    // Effectively $75000 - $100000
            var member5 = CreateMember(5, null, 150000);    // Effectively $112500 - $150000

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Do unbounded salary searches.

            criteria.Salary = null;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member3, member4, member5);

            // Do bounded salary searches.

            criteria.Salary = new Salary { LowerBound = 10000, UpperBound = 20000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            Get(GetPartialSearchUrl(criteria));
            AssertMembers();

            criteria.Salary = new Salary { LowerBound = 10000, UpperBound = 200000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member3, member4, member5);

            criteria.Salary = new Salary { LowerBound = 10000, UpperBound = 60000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member2);

            criteria.Salary = new Salary { LowerBound = 60000, UpperBound = 200000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member3, member4, member5);

            criteria.Salary = new Salary { LowerBound = 120000, UpperBound = 200000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member5);

            criteria.Salary = new Salary { LowerBound = 200000, UpperBound = 300000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            Get(GetPartialSearchUrl(criteria));
            AssertMembers();

            // Do un-lower-bounded salary searches.

            criteria.Salary = new Salary { LowerBound = null, UpperBound = 20000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            Get(GetPartialSearchUrl(criteria));
            AssertMembers();

            criteria.Salary = new Salary { LowerBound = null, UpperBound = 60000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member2);

            criteria.Salary = new Salary { LowerBound = null, UpperBound = 90000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member3, member4);

            criteria.Salary = new Salary { LowerBound = null, UpperBound = 120000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member3, member4, member5);

            criteria.Salary = new Salary { LowerBound = null, UpperBound = 200000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member3, member4, member5);

            // Do un-upper-bounded salary searches.

            criteria.Salary = new Salary { LowerBound = 20000, UpperBound = null, Currency = Currency.AUD, Rate = SalaryRate.Year };
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member3, member4, member5);

            criteria.Salary = new Salary { LowerBound = 60000, UpperBound = null, Currency = Currency.AUD, Rate = SalaryRate.Year };
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member3, member4, member5);

            criteria.Salary = new Salary { LowerBound = 90000, UpperBound = null, Currency = Currency.AUD, Rate = SalaryRate.Year };
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member1, member3, member4, member5);

            criteria.Salary = new Salary { LowerBound = 120000, UpperBound = null, Currency = Currency.AUD, Rate = SalaryRate.Year };
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member5);

            criteria.Salary = new Salary { LowerBound = 200000, UpperBound = null, Currency = Currency.AUD, Rate = SalaryRate.Year };
            Get(GetPartialSearchUrl(criteria));
            AssertMembers();
        }

        [TestMethod]
        public void TestExcludeNoSalaryWithSalary()
        {
            // With salary.

            var member0 = CreateMember(0, 50000, 100000);

            // With no salary.

            var member1 = CreateMember(1);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Specify salary in criteria.

            Assert.IsFalse(criteria.ExcludeNoSalary);
            criteria.Salary = new Salary { LowerBound = 10000, UpperBound = 200000, Currency = Currency.AUD, Rate = SalaryRate.Year };

            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1);

            // Exclude no salary.

            criteria.ExcludeNoSalary = true;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0);
        }

        [TestMethod]
        public void TestExcludeNoSalaryWithoutSalary()
        {
            // With salary.

            var member0 = CreateMember(0, 50000, 100000);

            // With no salary.

            var member1 = CreateMember(1);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            Assert.IsFalse(criteria.ExcludeNoSalary);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1);

            // Exclude no salary.

            criteria.ExcludeNoSalary = true;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0);
        }

        [TestMethod]
        public void TestDeleteSalary()
        {
            // Create member.

            var member = CreateMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.DesiredSalary = new Salary { LowerBound = 50000, UpperBound = 100000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            _candidatesCommand.UpdateCandidate(candidate);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Do salary search.

            criteria.Salary = new Salary { LowerBound = 10000, UpperBound = 200000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            criteria.ExcludeNoSalary = true;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);

            // Delete the salary.

            candidate.DesiredSalary = null;
            _candidatesCommand.UpdateCandidate(candidate);

            criteria.Salary = new Salary { LowerBound = 10000, UpperBound = 200000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            criteria.ExcludeNoSalary = true;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers();
        }

        [TestMethod]
        public void TestUpdateSalary()
        {
            // Create member.

            var member = CreateMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.DesiredSalary = new Salary { LowerBound = 50000, UpperBound = 100000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            _candidatesCommand.UpdateCandidate(candidate);

            var criteria1 = new MemberSearchCriteria();
            criteria1.SetKeywords(BusinessAnalyst);
            criteria1.Salary = new Salary { LowerBound = 10000, UpperBound = 110000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            criteria1.ExcludeNoSalary = true;

            var criteria2 = new MemberSearchCriteria();
            criteria2.SetKeywords(BusinessAnalyst);
            criteria2.Salary = new Salary { LowerBound = 120000, UpperBound = 200000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            criteria2.ExcludeNoSalary = true;

            // Do salary search.

            Get(GetPartialSearchUrl(criteria1));
            AssertMembers(member);
            Get(GetPartialSearchUrl(criteria2));
            AssertMembers();

            // Update the salary.

            candidate.DesiredSalary = new Salary { LowerBound = 140000, UpperBound = 150000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            _candidatesCommand.UpdateCandidate(candidate);

            Get(GetPartialSearchUrl(criteria1));
            AssertMembers();
            Get(GetPartialSearchUrl(criteria2));
            AssertMembers(member);
        }

        [TestMethod]
        public void TestHourlySalary()
        {
            // Create member, equivalent to $50000 - $100000.

            var member = CreateMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.DesiredSalary = new Salary { LowerBound = 25, UpperBound = 50, Currency = Currency.AUD, Rate = SalaryRate.Hour };
            _candidatesCommand.UpdateCandidate(candidate);

            // Salary searches.

            var criteria1 = new MemberSearchCriteria();
            criteria1.SetKeywords(BusinessAnalyst);
            criteria1.Salary = new Salary { LowerBound = 10000, UpperBound = 100000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            criteria1.ExcludeNoSalary = true;

            Get(GetPartialSearchUrl(criteria1));
            AssertMembers(member);

            var criteria2 = new MemberSearchCriteria();
            criteria2.SetKeywords(BusinessAnalyst);
            criteria2.Salary = new Salary { LowerBound = 110000, UpperBound = 200000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            criteria2.ExcludeNoSalary = true;

            Get(GetPartialSearchUrl(criteria2));
            AssertMembers();
        }

        [TestMethod]
        public void TestHiddenSalary()
        {
            // Create member.

            var member = CreateMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.DesiredSalary = new Salary { LowerBound = 50000, UpperBound = 100000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            _candidatesCommand.UpdateCandidate(candidate);

            // Salary searches.

            var criteria1 = new MemberSearchCriteria();
            criteria1.SetKeywords(BusinessAnalyst);
            criteria1.Salary = new Salary { LowerBound = 10000, UpperBound = 100000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            criteria1.ExcludeNoSalary = true;

            Get(GetPartialSearchUrl(criteria1));
            AssertMembers(member);

            var criteria2 = new MemberSearchCriteria();
            criteria2.SetKeywords(BusinessAnalyst);
            criteria2.Salary = new Salary { LowerBound = 10000, UpperBound = 100000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            criteria2.ExcludeNoSalary = false;

            Get(GetPartialSearchUrl(criteria2));
            AssertMembers(member);

            // Hide.

            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Salary);
            _memberAccountsCommand.UpdateMember(member);

            Get(GetPartialSearchUrl(criteria1));
            AssertMembers();

            Get(GetPartialSearchUrl(criteria2));
            AssertMembers(member);
        }

        private Member CreateMember(int index, int? lowerBound, int? upperBound)
        {
            var member = CreateMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.DesiredSalary = new Salary { LowerBound = lowerBound, UpperBound = upperBound, Currency = Currency.AUD, Rate = SalaryRate.Year };
            _candidatesCommand.UpdateCandidate(candidate);
            return member;
        }
    }
}