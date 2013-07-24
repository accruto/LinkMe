using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Search.Criteria
{
    [TestClass]
    public class SalaryTests
        : SearchTests
    {
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
            var model = Search(criteria);
            AssertMembers(model, member0, member1, member2, member3, member4, member5);

            // Do bounded salary searches.

            criteria.Salary = new Salary { LowerBound = 10000, UpperBound = 20000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            model = Search(criteria);
            AssertMembers(model);

            criteria.Salary = new Salary { LowerBound = 10000, UpperBound = 200000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            model = Search(criteria);
            AssertMembers(model, member0, member1, member2, member3, member4, member5);

            criteria.Salary = new Salary { LowerBound = 10000, UpperBound = 60000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            model = Search(criteria);
            AssertMembers(model, member0, member2);

            criteria.Salary = new Salary { LowerBound = 60000, UpperBound = 200000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            model = Search(criteria);
            AssertMembers(model, member0, member1, member2, member3, member4, member5);

            criteria.Salary = new Salary { LowerBound = 120000, UpperBound = 200000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            model = Search(criteria);
            AssertMembers(model, member5);

            criteria.Salary = new Salary { LowerBound = 200000, UpperBound = 300000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            model = Search(criteria);
            AssertMembers(model);

            // Do un-lower-bounded salary searches.

            criteria.Salary = new Salary { LowerBound = null, UpperBound = 20000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            model = Search(criteria);
            AssertMembers(model);

            criteria.Salary = new Salary { LowerBound = null, UpperBound = 60000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            model = Search(criteria);
            AssertMembers(model, member0, member2);

            criteria.Salary = new Salary { LowerBound = null, UpperBound = 90000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            model = Search(criteria);
            AssertMembers(model, member0, member1, member2, member3, member4);

            criteria.Salary = new Salary { LowerBound = null, UpperBound = 120000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            model = Search(criteria);
            AssertMembers(model, member0, member1, member2, member3, member4, member5);

            criteria.Salary = new Salary { LowerBound = null, UpperBound = 200000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            model = Search(criteria);
            AssertMembers(model, member0, member1, member2, member3, member4, member5);

            // Do un-upper-bounded salary searches.

            criteria.Salary = new Salary { LowerBound = 20000, UpperBound = null, Currency = Currency.AUD, Rate = SalaryRate.Year };
            model = Search(criteria);
            AssertMembers(model, member0, member1, member2, member3, member4, member5);

            criteria.Salary = new Salary { LowerBound = 60000, UpperBound = null, Currency = Currency.AUD, Rate = SalaryRate.Year };
            model = Search(criteria);
            AssertMembers(model, member0, member1, member2, member3, member4, member5);

            criteria.Salary = new Salary { LowerBound = 90000, UpperBound = null, Currency = Currency.AUD, Rate = SalaryRate.Year };
            model = Search(criteria);
            AssertMembers(model, member1, member3, member4, member5);

            criteria.Salary = new Salary { LowerBound = 120000, UpperBound = null, Currency = Currency.AUD, Rate = SalaryRate.Year };
            model = Search(criteria);
            AssertMembers(model, member5);

            criteria.Salary = new Salary { LowerBound = 200000, UpperBound = null, Currency = Currency.AUD, Rate = SalaryRate.Year };
            model = Search(criteria);
            AssertMembers(model);
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
            var model = Search(criteria);
            AssertMembers(model, member);

            // Delete the salary, should still be included.

            candidate.DesiredSalary = null;
            _candidatesCommand.UpdateCandidate(candidate);

            criteria.Salary = new Salary { LowerBound = 10000, UpperBound = 200000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            model = Search(criteria);
            AssertMembers(model, member);
        }

        [TestMethod]
        public void TestUpdateSalary()
        {
            // Create member.

            var member = CreateMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.DesiredSalary = new Salary { LowerBound = 50000, UpperBound = 100000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            _candidatesCommand.UpdateCandidate(candidate);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            criteria.Salary = new Salary { LowerBound = 10000, UpperBound = 110000, Currency = Currency.AUD, Rate = SalaryRate.Year };

            // Do salary search.

            var model = Search(criteria);
            AssertMembers(model, member);
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

            var model = Search(criteria1);
            AssertMembers(model, member);
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

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            criteria.Salary = new Salary { LowerBound = 10000, UpperBound = 100000, Currency = Currency.AUD, Rate = SalaryRate.Year };

            var model = Search(criteria);
            AssertMembers(model, member);

            // Hide.

            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Salary);
            _memberAccountsCommand.UpdateMember(member);
            _memberSearchService.UpdateMember(member.Id);

            model = Search(criteria);
            AssertMembers(model, member);
        }

        private Member CreateMember(int index, int? lowerBound, int? upperBound)
        {
            var member = CreateMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.DesiredSalary = new Salary { LowerBound = lowerBound, UpperBound = upperBound, Currency = Currency.AUD, Rate = SalaryRate.Year };
            _candidatesCommand.UpdateCandidate(candidate);
            _memberSearchService.UpdateMember(member.Id);
            return member;
        }
    }
}