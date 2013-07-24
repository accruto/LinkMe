using System.Text;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Query.Search.Members;
using LinkMe.Web.Areas.Employers.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Results
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

            Get(GetPartialSearchUrl(criteria, DetailLevel.Expanded));
            AssertExpandedSalary(null);
            Get(GetPartialSearchUrl(criteria, DetailLevel.Compact));
            AssertCompactSalary(null);
        }

        [TestMethod]
        public void TestYearlyLowerOnly()
        {
            var salary = new Salary { LowerBound = 70000, UpperBound = null, Rate = SalaryRate.Year, Currency = Currency.AUD };
            CreateMember(salary);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            Get(GetPartialSearchUrl(criteria, DetailLevel.Expanded));
            AssertExpandedSalary(salary);
            Get(GetPartialSearchUrl(criteria, DetailLevel.Compact));
            AssertCompactSalary(salary);
        }

        [TestMethod]
        public void TestYearlyUpperOnly()
        {
            var salary = new Salary { LowerBound = null, UpperBound = 70000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            CreateMember(salary);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            Get(GetPartialSearchUrl(criteria, DetailLevel.Expanded));
            AssertExpandedSalary(salary);
            Get(GetPartialSearchUrl(criteria, DetailLevel.Compact));
            AssertCompactSalary(salary);
        }

        [TestMethod]
        public void TestYearly()
        {
            var salary = new Salary { LowerBound = 70000, UpperBound = 90000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            CreateMember(salary);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            Get(GetPartialSearchUrl(criteria, DetailLevel.Expanded));
            AssertExpandedSalary(salary);
            Get(GetPartialSearchUrl(criteria, DetailLevel.Compact));
            AssertCompactSalary(salary);
        }

        [TestMethod]
        public void TestHourlyLowerOnly()
        {
            var salary = new Salary { LowerBound = 55, UpperBound = null, Rate = SalaryRate.Hour, Currency = Currency.AUD };
            CreateMember(salary);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            Get(GetPartialSearchUrl(criteria, DetailLevel.Expanded));
            AssertExpandedSalary(salary);
            Get(GetPartialSearchUrl(criteria, DetailLevel.Compact));
            AssertCompactSalary(salary);
        }

        [TestMethod]
        public void TestHourlyUpperOnly()
        {
            var salary = new Salary { LowerBound = null, UpperBound = 95, Rate = SalaryRate.Hour, Currency = Currency.AUD };
            CreateMember(salary);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            Get(GetPartialSearchUrl(criteria, DetailLevel.Expanded));
            AssertExpandedSalary(salary);
            Get(GetPartialSearchUrl(criteria, DetailLevel.Compact));
            AssertCompactSalary(salary);
        }

        [TestMethod]
        public void TestHourly()
        {
            var salary = new Salary { LowerBound = 55, UpperBound = 95, Rate = SalaryRate.Hour, Currency = Currency.AUD };
            CreateMember(salary);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            Get(GetPartialSearchUrl(criteria, DetailLevel.Expanded));
            AssertExpandedSalary(salary);
            Get(GetPartialSearchUrl(criteria, DetailLevel.Compact));
            AssertCompactSalary(salary);
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

            Get(GetPartialSearchUrl(criteria, DetailLevel.Expanded));
            AssertExpandedSalary(null);
            Get(GetPartialSearchUrl(criteria, DetailLevel.Compact));
            AssertCompactSalary(null);
        }

        private Member CreateMember(Salary salary)
        {
            var member = CreateMember(0);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            candidate.DesiredSalary = salary;
            _candidatesCommand.UpdateCandidate(candidate);
            return member;
        }

        private void AssertExpandedSalary(Salary salary)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='desired-salary']");
            Assert.IsNotNull(node);
            var spanNodes = node.SelectNodes("span");
            Assert.IsNotNull(spanNodes);
            Assert.AreEqual(1, spanNodes.Count);

            if (salary == null || salary.LowerBound == null)
                Assert.AreEqual("None specified", spanNodes[0].InnerText);
            else
                Assert.AreEqual(GetDisplayText(salary), spanNodes[0].InnerText);
        }

        private void AssertCompactSalary(Salary salary)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='desired-salary_column column']");
            Assert.IsNotNull(node);

            if (salary == null || salary.LowerBound == null)
                Assert.AreEqual("None specified", node.InnerText.Trim());
            else
                Assert.AreEqual(GetDisplayText(salary), node.InnerText.Trim());
        }

        private static string GetDisplayText(Salary salary)
        {
            salary = salary.ToRate(SalaryRate.Year);
            var sb = new StringBuilder();
            sb.Append(salary.LowerBound.Value.ToString("C0", salary.Currency.CultureInfo));
            if (salary.UpperBound == null)
                sb.Append("+");
            else
                sb.Append(" to " + salary.UpperBound.Value.ToString("C0", salary.Currency.CultureInfo));
            return sb.ToString();
        }
    }
}
