using System;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Display.Web
{
    [TestClass]
    public class SalaryTests
        : DisplayTests
    {
        [TestMethod]
        public void TestNoSalary()
        {
            var employer = CreateEmployer();
            TestSalary(employer, new Salary { LowerBound = 0, UpperBound = 0, Rate = SalaryRate.Year, Currency = Currency.AUD }, null);
            TestSalary(employer, new Salary { LowerBound = 0, UpperBound = null, Rate = SalaryRate.Year, Currency = Currency.AUD }, null);
            TestSalary(employer, new Salary { LowerBound = null, UpperBound = 0, Rate = SalaryRate.Year, Currency = Currency.AUD }, null);
            TestSalary(employer, new Salary { LowerBound = null, UpperBound = null, Rate = SalaryRate.Year, Currency = Currency.AUD }, null);
        }

        [TestMethod]
        public void TestLowerBound()
        {
            var employer = CreateEmployer();
            TestSalary(employer, new Salary { LowerBound = 50000, UpperBound = null, Rate = SalaryRate.Year, Currency = Currency.AUD });
        }

        [TestMethod]
        public void TestUpperBound()
        {
            var employer = CreateEmployer();
            TestSalary(employer, new Salary { LowerBound = null, UpperBound = 50000, Rate = SalaryRate.Year, Currency = Currency.AUD });
        }

        [TestMethod]
        public void TestLowerUpperBound()
        {
            var employer = CreateEmployer();
            TestSalary(employer, new Salary { LowerBound = 30000, UpperBound = 50000, Rate = SalaryRate.Year, Currency = Currency.AUD });
        }

        private void TestSalary(IEmployer employer, Salary salary)
        {
            TestSalary(employer, salary, salary);
        }

        private void TestSalary(IEmployer employer, Salary salary, Salary expectedSalary)
        {
            var jobAd = PostJobAd(employer, j => { j.Description.Salary = salary; });

            Get(GetJobUrl(jobAd.Id));
            if (expectedSalary != null && !expectedSalary.IsEmpty && !expectedSalary.IsZero)
                Assert.AreEqual(GetDisplayText(expectedSalary) + " p.a.", GetSalary());
            else
                Assert.AreEqual("Not specified", GetSalary());
        }

        private string GetSalary()
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='summary']//div[@class='title' and text()='Salary']/../div[@class='desc']");
            return nodes.Count > 0 ? nodes[0].InnerText : null;
        }

        private static string GetDisplayText(Salary salary)
        {
            if (salary.LowerBound != null)
            {
                if (salary.UpperBound != null)
                {
                    return salary.UpperBound == salary.LowerBound
                        ? string.Format("{0}", GetDisplayText(salary, salary.UpperBound.Value))
                        : string.Format("{0} to {1}", GetDisplayText(salary, salary.LowerBound.Value), GetDisplayText(salary, salary.UpperBound.Value));
                }

                return string.Format("{0}+", GetDisplayText(salary, salary.LowerBound.Value));
            }

            return salary.UpperBound != null
                ? string.Format("{0}", GetDisplayText(salary, salary.UpperBound.Value))
                : string.Empty;
        }

        private static string GetDisplayText(Salary salary, decimal value)
        {
            return Math.Round(value, salary.Currency.CultureInfo.NumberFormat.CurrencyDecimalDigits).ToString("C0", salary.Currency.CultureInfo);
        }
    }
}