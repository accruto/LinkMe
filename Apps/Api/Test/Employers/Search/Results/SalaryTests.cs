using LinkMe.Apps.Api.Areas.Employers.Models.Candidates;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Search.Results
{
    [TestClass]
    public class SalaryTests
        : SearchTests
    {
        [TestMethod]
        public void TestNone()
        {
            CreateMember(null);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            var model = Search(criteria);
            AssertSalary(model, null);
        }

        [TestMethod]
        public void TestYearlyLowerOnly()
        {
            var salary = new Salary { LowerBound = 70000, UpperBound = null, Rate = SalaryRate.Year, Currency = Currency.AUD };
            CreateMember(salary);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            var model = Search(criteria);
            AssertSalary(model, salary);
        }

        [TestMethod]
        public void TestYearlyUpperOnly()
        {
            var salary = new Salary { LowerBound = null, UpperBound = 70000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            CreateMember(salary);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            var model = Search(criteria);
            AssertSalary(model, salary);
        }

        [TestMethod]
        public void TestYearly()
        {
            var salary = new Salary { LowerBound = 70000, UpperBound = 90000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            CreateMember(salary);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            var model = Search(criteria);
            AssertSalary(model, salary);
        }

        [TestMethod]
        public void TestHourlyLowerOnly()
        {
            var salary = new Salary { LowerBound = 55, UpperBound = null, Rate = SalaryRate.Hour, Currency = Currency.AUD };
            CreateMember(salary);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            var model = Search(criteria);
            AssertSalary(model, salary);
        }

        [TestMethod]
        public void TestHourlyUpperOnly()
        {
            var salary = new Salary { LowerBound = null, UpperBound = 95, Rate = SalaryRate.Hour, Currency = Currency.AUD };
            CreateMember(salary);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            var model = Search(criteria);
            AssertSalary(model, salary);
        }

        [TestMethod]
        public void TestHourly()
        {
            var salary = new Salary { LowerBound = 55, UpperBound = 95, Rate = SalaryRate.Hour, Currency = Currency.AUD };
            CreateMember(salary);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            var model = Search(criteria);
            AssertSalary(model, salary);
        }

        [TestMethod]
        public void TestHidden()
        {
            var salary = new Salary { LowerBound = 55, UpperBound = 95, Rate = SalaryRate.Hour, Currency = Currency.AUD };
            var member = CreateMember(salary);
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Salary);
            _memberAccountsCommand.UpdateMember(member);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            var model = Search(criteria);
            AssertSalary(model, null);
        }

        private Member CreateMember(Salary salary)
        {
            var member = CreateMember(0);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            candidate.DesiredSalary = salary;
            _candidatesCommand.UpdateCandidate(candidate);
            return member;
        }

        private static void AssertSalary(CandidatesResponseModel model, Salary expectedSalary)
        {
            Assert.AreEqual(1, model.Candidates.Count);

            if (expectedSalary == null)
            {
                Assert.IsNull(model.Candidates[0].DesiredSalary);
            }
            else
            {
                Assert.IsNotNull(model.Candidates[0].DesiredSalary);
                expectedSalary = expectedSalary.ToRate(SalaryRate.Year);
                Assert.AreEqual(expectedSalary.LowerBound, model.Candidates[0].DesiredSalary.LowerBound);
                Assert.AreEqual(expectedSalary.UpperBound, model.Candidates[0].DesiredSalary.UpperBound);
            }
        }
    }
}