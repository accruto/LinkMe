using System.Globalization;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class SalaryTests
        : CreateJobAdTests
    {
        private const int LowerBound = 50000;
        private const int UpperBound = 75000;

        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual("", _salaryLowerBoundTextBox.Text);
            Assert.AreEqual("", _salaryUpperBoundTextBox.Text);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.IsTrue(jobAd.Description.Salary.IsEmpty);
        }

        protected override void SetDisplayValue()
        {
            _salaryLowerBoundTextBox.Text = LowerBound.ToString(CultureInfo.InvariantCulture);
            _salaryUpperBoundTextBox.Text = UpperBound.ToString(CultureInfo.InvariantCulture);
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(new Salary{ LowerBound = LowerBound, UpperBound = UpperBound, Currency = Currency.AUD, Rate = SalaryRate.Year }, jobAd.Description.Salary);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
        }
    }
}
