using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test
{
    [TestClass]
    public class SalaryTests
    {
        [TestMethod]
        public void TestConversions()
        {
            // Hour to Year.

            var hourSalary = new Salary { LowerBound = 100, UpperBound = 200, Rate = SalaryRate.Hour, Currency = Currency.AUD };
            var yearSalary = hourSalary.ToRate(SalaryRate.Year);
            AssertSalary(200000, 400000, SalaryRate.Year, yearSalary);
            AssertSalary(hourSalary.LowerBound, hourSalary.UpperBound, hourSalary.Rate, yearSalary.ToRate(SalaryRate.Hour));

            // Hour to Year no upper bound.

            hourSalary = new Salary { LowerBound = 155, Rate = SalaryRate.Hour, Currency = Currency.AUD };
            yearSalary = hourSalary.ToRate(SalaryRate.Year);
            AssertSalary(310000, null, SalaryRate.Year, yearSalary);
            AssertSalary(hourSalary.LowerBound, hourSalary.UpperBound, hourSalary.Rate, yearSalary.ToRate(SalaryRate.Hour));

            // Year to hour.

            yearSalary = new Salary { LowerBound = 35000, UpperBound = 45000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            hourSalary = yearSalary.ToRate(SalaryRate.Hour);
            AssertSalary(17.5m, 22.5m, SalaryRate.Hour, hourSalary);
            AssertSalary(yearSalary.LowerBound, yearSalary.UpperBound, yearSalary.Rate, hourSalary.ToRate(SalaryRate.Year));

            // Hour to Year no upper bound.

            yearSalary = new Salary { LowerBound = 55000, Rate = SalaryRate.Year, Currency = Currency.AUD };
            hourSalary = yearSalary.ToRate(SalaryRate.Hour);
            AssertSalary(27.5m, null, SalaryRate.Hour, hourSalary);
            AssertSalary(yearSalary.LowerBound, yearSalary.UpperBound, yearSalary.Rate, hourSalary.ToRate(SalaryRate.Year));
        }

        [TestMethod]
        public void CanSerializeYearlySalary()
        {
            var stream = new MemoryStream();
            var serializer = new DataContractSerializer(typeof(Salary));

            // Serialize.

            var salaryIn = new Salary
            {
                Currency = Currency.AUD,
                LowerBound = 50000,
                UpperBound = 60000,
                Rate = SalaryRate.Year
            };

            var writer = XmlDictionaryWriter.CreateBinaryWriter(stream);
            serializer.WriteObject(writer, salaryIn);
            writer.Flush();

            // Deserialize.

            stream.Position = 0;
            var reader = XmlDictionaryReader.CreateBinaryReader(stream, new XmlDictionaryReaderQuotas());
            var salaryOut = (Salary)serializer.ReadObject(reader);

            Assert.AreEqual(salaryIn, salaryOut);
        }

        [TestMethod]
        public void CanSerializeEmptySalary()
        {
            var stream = new MemoryStream();
            var serializer = new DataContractSerializer(typeof(Salary));

            // Serialize.

            var salaryIn = new Salary
            {
                Currency = Currency.AUD,
                LowerBound = null,
                UpperBound = null,
                Rate = SalaryRate.None
            };

            var writer = XmlDictionaryWriter.CreateBinaryWriter(stream);
            serializer.WriteObject(writer, salaryIn);
            writer.Flush();

            // Deserialize.

            stream.Position = 0;
            var reader = XmlDictionaryReader.CreateBinaryReader(stream, new XmlDictionaryReaderQuotas());
            var salaryOut = (Salary)serializer.ReadObject(reader);

            Assert.AreEqual(salaryIn, salaryOut);
        }

        private static void AssertSalary(decimal? lowerBound, decimal? upperBound, SalaryRate rate, Salary salary)
        {
            Assert.AreEqual(lowerBound, salary.LowerBound);
            Assert.AreEqual(upperBound, salary.UpperBound);
            Assert.AreEqual(rate, salary.Rate);
        }
    }
}
