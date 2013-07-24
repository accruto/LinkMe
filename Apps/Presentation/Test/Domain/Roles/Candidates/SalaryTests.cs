using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Presentation.Test.Domain.Roles.Candidates
{
    [TestClass]
    public class SalaryTests
    {
        [TestMethod]
        public void TestNull()
        {
            const Salary salary = null;
            Assert.IsNull(salary.GetEffectiveLowerBound());
            Assert.IsNull(new Salary { Rate = SalaryRate.None }.GetEffectiveLowerBound());
            Assert.IsNull(new Salary { Rate = SalaryRate.Year }.GetEffectiveLowerBound());
        }

        [TestMethod]
        public void TestYearlyLowerBound()
        {
            Assert.AreEqual(0, new Salary { LowerBound = 0, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(5000, new Salary { LowerBound = 2000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());

            Assert.AreEqual(5000, new Salary { LowerBound = 5000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(5000, new Salary { LowerBound = 7000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(5000, new Salary { LowerBound = 9999, Rate = SalaryRate.Year }.GetEffectiveLowerBound());

            Assert.AreEqual(55000, new Salary { LowerBound = 55000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(55000, new Salary { LowerBound = 57000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(55000, new Salary { LowerBound = 59999, Rate = SalaryRate.Year }.GetEffectiveLowerBound());

            Assert.AreEqual(250000, new Salary { LowerBound = 250000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(250000, new Salary { LowerBound = 257000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(250000, new Salary { LowerBound = 259999, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(250000, new Salary { LowerBound = 300000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(250000, new Salary { LowerBound = 432120, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(250000, new Salary { LowerBound = 8998098, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
        }

        [TestMethod]
        public void TestYearlyLowerAndUpperBounds()
        {
            Assert.AreEqual(5000, new Salary { LowerBound = 5000, UpperBound = 10000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(5000, new Salary { LowerBound = 7000, UpperBound = 10000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(5000, new Salary { LowerBound = 9999, UpperBound = 10000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());

            Assert.AreEqual(55000, new Salary { LowerBound = 55000, UpperBound = 10000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(55000, new Salary { LowerBound = 57000, UpperBound = 10000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(55000, new Salary { LowerBound = 59999, UpperBound = 10000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());

            Assert.AreEqual(250000, new Salary { LowerBound = 250000, UpperBound = 1000000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(250000, new Salary { LowerBound = 257000, UpperBound = 1000000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(250000, new Salary { LowerBound = 259999, UpperBound = 1000000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(250000, new Salary { LowerBound = 300000, UpperBound = 1000000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(250000, new Salary { LowerBound = 432120, UpperBound = 1000000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(250000, new Salary { LowerBound = 8998098, UpperBound = 10000000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
        }

        [TestMethod]
        public void TestYearlyUpperBound()
        {
            Assert.AreEqual(5000, new Salary { UpperBound = 5000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(5000, new Salary { UpperBound = 7000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(5000, new Salary { UpperBound = 9999, Rate = SalaryRate.Year }.GetEffectiveLowerBound());

            Assert.AreEqual(40000, new Salary { UpperBound = 55000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(45000, new Salary { UpperBound = 57000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(45000, new Salary { UpperBound = 59999, Rate = SalaryRate.Year }.GetEffectiveLowerBound());

            Assert.AreEqual(200000, new Salary { UpperBound = 250000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(205000, new Salary { UpperBound = 257000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(205000, new Salary { UpperBound = 259999, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(240000, new Salary { UpperBound = 300000, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(250000, new Salary { UpperBound = 432120, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
            Assert.AreEqual(250000, new Salary { UpperBound = 8998098, Rate = SalaryRate.Year }.GetEffectiveLowerBound());
        }

        [TestMethod]
        public void TestMonthlyLowerBound()
        {
            Assert.AreEqual(0, new Salary { LowerBound = 0, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(416, new Salary { LowerBound = 167, Rate = SalaryRate.Month }.GetEffectiveLowerBound());

            Assert.AreEqual(416, new Salary { LowerBound = 416, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(416, new Salary { LowerBound = 584, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(416, new Salary { LowerBound = 833, Rate = SalaryRate.Month }.GetEffectiveLowerBound());

            Assert.AreEqual(4166, new Salary { LowerBound = 4583, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(4583, new Salary { LowerBound = 4750, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(4999, new Salary { LowerBound = 5000, Rate = SalaryRate.Month }.GetEffectiveLowerBound());

            Assert.AreEqual(20416, new Salary { LowerBound = 20833, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(20833, new Salary { LowerBound = 21416, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(20833, new Salary { LowerBound = 21666, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(20833, new Salary { LowerBound = 25000, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(20833, new Salary { LowerBound = 36010, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(20833, new Salary { LowerBound = 749841, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
        }

        [TestMethod]
        public void TestMonthlyLowerAndUpperBounds()
        {
            Assert.AreEqual(416, new Salary { LowerBound = 416, UpperBound = 10000, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(416, new Salary { LowerBound = 584, UpperBound = 10000, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(416, new Salary { LowerBound = 833, UpperBound = 10000, Rate = SalaryRate.Month }.GetEffectiveLowerBound());

            Assert.AreEqual(4166, new Salary { LowerBound = 4583, UpperBound = 10000, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(4583, new Salary { LowerBound = 4750, UpperBound = 10000, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(4999, new Salary { LowerBound = 5000, UpperBound = 10000, Rate = SalaryRate.Month }.GetEffectiveLowerBound());

            Assert.AreEqual(20416, new Salary { LowerBound = 20833, UpperBound = 1000000, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(20833, new Salary { LowerBound = 21416, UpperBound = 1000000, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(20833, new Salary { LowerBound = 21666, UpperBound = 1000000, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(20833, new Salary { LowerBound = 25000, UpperBound = 1000000, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(20833, new Salary { LowerBound = 36010, UpperBound = 1000000, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(20833, new Salary { LowerBound = 749841, UpperBound = 10000000, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
        }

        [TestMethod]
        public void TestMonthlyUpperBound()
        {
            Assert.AreEqual(416, new Salary { UpperBound = 416, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(416, new Salary { UpperBound = 584, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(416, new Salary { UpperBound = 833, Rate = SalaryRate.Month }.GetEffectiveLowerBound());

            Assert.AreEqual(3333, new Salary { UpperBound = 4583, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(3749, new Salary { UpperBound = 4750, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(3749, new Salary { UpperBound = 5000, Rate = SalaryRate.Month }.GetEffectiveLowerBound());

            Assert.AreEqual(16249, new Salary { UpperBound = 20833, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(17083, new Salary { UpperBound = 21416, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(17083, new Salary { UpperBound = 21666, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(19999, new Salary { UpperBound = 25000, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(20833, new Salary { UpperBound = 36010, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
            Assert.AreEqual(20833, new Salary { UpperBound = 749841, Rate = SalaryRate.Month }.GetEffectiveLowerBound());
        }

        [TestMethod]
        public void TestHourlyLowerBound()
        {
            Assert.AreEqual(0, new Salary { LowerBound = 0, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(5, new Salary { LowerBound = 1, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());

            Assert.AreEqual(5, new Salary { LowerBound = 5, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(5, new Salary { LowerBound = 7, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(5, new Salary { LowerBound = 9, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());

            Assert.AreEqual(25, new Salary { LowerBound = 28, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(30, new Salary { LowerBound = 31, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(30, new Salary { LowerBound = 34, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());

            Assert.AreEqual(125, new Salary { LowerBound = 125, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(125, new Salary { LowerBound = 130, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(125, new Salary { LowerBound = 200, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
        }

        [TestMethod]
        public void TestHourlyLowerAndUpperBounds()
        {
            Assert.AreEqual(0, new Salary { LowerBound = 0, UpperBound = 40, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(5, new Salary { LowerBound = 1, UpperBound = 40, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());

            Assert.AreEqual(5, new Salary { LowerBound = 5, UpperBound = 40, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(5, new Salary { LowerBound = 7, UpperBound = 40, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(5, new Salary { LowerBound = 9, UpperBound = 40, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());

            Assert.AreEqual(25, new Salary { LowerBound = 28, UpperBound = 40, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(30, new Salary { LowerBound = 31, UpperBound = 40, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(30, new Salary { LowerBound = 34, UpperBound = 40, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());

            Assert.AreEqual(125, new Salary { LowerBound = 125, UpperBound = 400, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(125, new Salary { LowerBound = 130, UpperBound = 400, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(125, new Salary { LowerBound = 200, UpperBound = 400, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
        }

        [TestMethod]
        public void TestHourlyUpperBound()
        {
            Assert.AreEqual(0, new Salary { UpperBound = 0, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(5, new Salary { UpperBound = 1, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());

            Assert.AreEqual(5, new Salary { UpperBound = 5, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(5, new Salary { UpperBound = 7, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(5, new Salary { UpperBound = 9, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());

            Assert.AreEqual(20, new Salary { UpperBound = 28, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(20, new Salary { UpperBound = 31, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(25, new Salary { UpperBound = 34, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());

            Assert.AreEqual(100, new Salary { UpperBound = 125, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(100, new Salary { UpperBound = 130, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
            Assert.AreEqual(125, new Salary { UpperBound = 200, Rate = SalaryRate.Hour }.GetEffectiveLowerBound());
        }
    }
}
