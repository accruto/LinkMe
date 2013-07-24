using LinkMe.Common;
using LinkMe.Domain;
using LinkMe.TaskRunner.Tasks.MyCareer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.MyCareer
{
    [TestClass]
    public class SalaryParserTest
    {
        private readonly SalaryParser _parser = new SalaryParser();

        [TestMethod]
        public void Can_parse_empty_string()
        {
            Salary salary;
            bool done = _parser.TryParse(string.Empty, out salary);
            Assert.IsTrue(done);
            Assert.IsTrue(salary.IsEmpty);
        }

        [TestMethod]
        public void Can_parse_range()
        {
            Salary salary;
            bool done = _parser.TryParse("$100,000 - $120,000", out salary);
            Assert.IsTrue(done);
            Assert.AreEqual(100000, salary.LowerBound);
            Assert.AreEqual(120000, salary.UpperBound);
            Assert.AreEqual(SalaryRate.Year, salary.Rate);
        }

        [TestMethod]
        public void Can_parse_range_plus()
        {
            Salary salary;
            bool done = _parser.TryParse("$100,000 - $120,000+", out salary);
            Assert.IsTrue(done);
            Assert.AreEqual(100000, salary.LowerBound);
            Assert.AreEqual(120000, salary.UpperBound);
            Assert.AreEqual(SalaryRate.Year, salary.Rate);
        }

        [TestMethod]
        public void Can_parse_range_plus_OTE()
        {
            Salary salary;
            bool done = _parser.TryParse("$100,000 - $120,000+ OTE", out salary);
            Assert.IsTrue(done);
            Assert.AreEqual(100000, salary.LowerBound);
            Assert.AreEqual(120000, salary.UpperBound);
            Assert.AreEqual(SalaryRate.Year, salary.Rate);
        }

        [TestMethod]
        public void Can_parse_range_pkg()
        {
            Salary salary;
            bool done = _parser.TryParse("$100,000 - $120,000 pkg", out salary);
            Assert.IsTrue(done);
            Assert.AreEqual(100000, salary.LowerBound);
            Assert.AreEqual(120000, salary.UpperBound);
            Assert.AreEqual(SalaryRate.Year, salary.Rate);
        }

        [TestMethod]
        public void Can_parse_value()
        {
            Salary salary;
            bool done = _parser.TryParse("$50,000", out salary);
            Assert.IsTrue(done);
            Assert.AreEqual(50000, salary.LowerBound);
            Assert.IsFalse(salary.HasUpperBound);
            Assert.AreEqual(SalaryRate.Year, salary.Rate);
        }

        [TestMethod]
        public void Can_parse_value_plus()
        {
            Salary salary;
            bool done = _parser.TryParse("$50,000+", out salary);
            Assert.IsTrue(done);
            Assert.AreEqual(50000, salary.LowerBound);
            Assert.IsFalse(salary.HasUpperBound);
            Assert.AreEqual(SalaryRate.Year, salary.Rate);
        }

        [TestMethod]
        public void Can_parse_value_pkg()
        {
            Salary salary;
            bool done = _parser.TryParse("$50,000 pkg", out salary);
            Assert.IsTrue(done);
            Assert.AreEqual(50000, salary.LowerBound);
            Assert.IsFalse(salary.HasUpperBound);
            Assert.AreEqual(SalaryRate.Year, salary.Rate);
        }

        [TestMethod]
        public void Can_parse_rate_range()
        {
            Salary salary;
            bool done = _parser.TryParse("$20 - $30/hr", out salary);
            Assert.IsTrue(done);
            Assert.AreEqual(20, salary.LowerBound);
            Assert.AreEqual(30, salary.UpperBound);
            Assert.AreEqual(SalaryRate.Hour, salary.Rate);
        }

        [TestMethod]
        public void Can_parse_rate_range_plus()
        {
            Salary salary;
            bool done = _parser.TryParse("$20 - $30/hr+", out salary);
            Assert.IsTrue(done);
            Assert.AreEqual(20, salary.LowerBound);
            Assert.AreEqual(30, salary.UpperBound);
            Assert.AreEqual(SalaryRate.Hour, salary.Rate);
        }

        [TestMethod]
        public void Can_parse_rate_range_neg()
        {
            Salary salary;
            bool done = _parser.TryParse("$20 - $30/hr neg.", out salary);
            Assert.IsTrue(done);
            Assert.AreEqual(20, salary.LowerBound);
            Assert.AreEqual(30, salary.UpperBound);
            Assert.AreEqual(SalaryRate.Hour, salary.Rate);
        }

        [TestMethod]
        public void Can_parse_rate_value()
        {
            Salary salary;
            bool done = _parser.TryParse("$20/hr", out salary);
            Assert.IsTrue(done);
            Assert.AreEqual(20, salary.LowerBound);
            Assert.IsFalse(salary.HasUpperBound);
            Assert.AreEqual(SalaryRate.Hour, salary.Rate);
        }

        [TestMethod]
        public void Can_parse_rate_value_plus()
        {
            Salary salary;
            bool done = _parser.TryParse("$20/hr+", out salary);
            Assert.IsTrue(done);
            Assert.AreEqual(20, salary.LowerBound);
            Assert.IsFalse(salary.HasUpperBound);
            Assert.AreEqual(SalaryRate.Hour, salary.Rate);
        }

        [TestMethod]
        public void Can_parse_rate_value_neg()
        {
            Salary salary;
            bool done = _parser.TryParse("$20/hr neg.", out salary);
            Assert.IsTrue(done);
            Assert.AreEqual(20, salary.LowerBound);
            Assert.IsFalse(salary.HasUpperBound);
            Assert.AreEqual(SalaryRate.Hour, salary.Rate);
        }
    }
}
