using System.Text;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Views
{
    [TestClass]
    public class SalaryTests
        : ViewsTests
    {
        [TestMethod]
        public void TestNone()
        {
            var member = CreateMember(null);
            TestCandidateUrls(member, () => AssertSalary(null));
        }

        [TestMethod]
        public void TestYearlyLowerOnly()
        {
            var salary = new Salary { LowerBound = 70000, UpperBound = null, Rate = SalaryRate.Year, Currency = Currency.AUD };
            var member = CreateMember(salary);
            TestCandidateUrls(member, () => AssertSalary(salary));
        }

        [TestMethod]
        public void TestYearlyUpperOnly()
        {
            var salary = new Salary { LowerBound = null, UpperBound = 70000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            var member = CreateMember(salary);
            TestCandidateUrls(member, () => AssertSalary(salary));
        }

        [TestMethod]
        public void TestYearly()
        {
            var salary = new Salary { LowerBound = 70000, UpperBound = 90000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            var member = CreateMember(salary);
            TestCandidateUrls(member, () => AssertSalary(salary));
        }

        [TestMethod]
        public void TestHourlyLowerOnly()
        {
            var salary = new Salary { LowerBound = 55, UpperBound = null, Rate = SalaryRate.Hour, Currency = Currency.AUD };
            var member = CreateMember(salary);
            TestCandidateUrls(member, () => AssertSalary(salary));
        }

        [TestMethod]
        public void TestHourlyUpperOnly()
        {
            var salary = new Salary { LowerBound = null, UpperBound = 95, Rate = SalaryRate.Hour, Currency = Currency.AUD };
            var member = CreateMember(salary);
            TestCandidateUrls(member, () => AssertSalary(salary));
        }

        [TestMethod]
        public void TestHourly()
        {
            var salary = new Salary { LowerBound = 55, UpperBound = 95, Rate = SalaryRate.Hour, Currency = Currency.AUD };
            var member = CreateMember(salary);
            TestCandidateUrls(member, () => AssertSalary(salary));
        }

        [TestMethod]
        public void TestHidden()
        {
            var salary = new Salary { LowerBound = 55, UpperBound = 95, Rate = SalaryRate.Hour, Currency = Currency.AUD };
            var member = CreateMember(salary);
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Salary);
            _memberAccountsCommand.UpdateMember(member);
            TestCandidateUrls(member, () => AssertSalary(null));
        }

        private Member CreateMember(Salary salary)
        {
            var member = CreateMember(0);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            candidate.DesiredSalary = salary;
            _candidatesCommand.UpdateCandidate(candidate);
            return member;
        }

        private void AssertSalary(Salary salary)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='professional_section resume_section']//table[@class='professional']//tr");
            Assert.IsNotNull(node);
            var spanNodes = node[4].SelectNodes("//td//span[@class='detail-content']//span");
            Assert.IsNotNull(spanNodes);

            if (salary == null || salary.LowerBound == null)
            {
                Assert.AreEqual(1, spanNodes.Count);
                Assert.AreEqual("", spanNodes[0].InnerText);
            }
            else
            {
                if (salary.Rate == SalaryRate.Hour)
                {
                    Assert.AreEqual(2, spanNodes.Count);
                    Assert.AreEqual(GetDisplayText(salary), spanNodes[0].InnerText);
                    Assert.AreEqual(GetEquivalentDisplayText(salary.ToRate(SalaryRate.Year)), spanNodes[1].InnerText);
                }
                else
                {
                    Assert.AreEqual(1, spanNodes.Count);
                    Assert.AreEqual(GetDisplayText(salary), spanNodes[0].InnerText);
                }
            }
        }

        private static string GetDisplayText(Salary salary)
        {
            var sb = new StringBuilder();
            GetDisplayText(sb, salary);

            sb.Append(" (total remuneration per ");
            if (salary.Rate == SalaryRate.Hour)
                sb.Append("hour");
            else
                sb.Append("annum");
            sb.Append(")");

            return sb.ToString();
        }

        private static string GetEquivalentDisplayText(Salary salary)
        {
            var sb = new StringBuilder();
            sb.Append("Equivalent to approximately ");
            GetDisplayText(sb, salary);
            sb.Append(" per annum");
            return sb.ToString();
        }

        private static void GetDisplayText(StringBuilder sb, Salary salary)
        {
            sb.Append(salary.LowerBound.Value.ToString("C0", salary.Currency.CultureInfo));
            if (salary.UpperBound == null)
                sb.Append("+");
            else
                sb.Append(" to " + salary.UpperBound.Value.ToString("C0", salary.Currency.CultureInfo));
        }
    }
}
