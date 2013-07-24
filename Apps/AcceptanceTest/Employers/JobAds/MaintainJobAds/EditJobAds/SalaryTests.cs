using System;
using System.Globalization;
using LinkMe.Domain;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class SalaryTests
        : EditJobAdTests
    {
        private const int LowerBound = 50000;
        private const int UpperBound = 75000;
        private const int ChangedLowerBound = 150000;
        private const int ChangedUpperBound = 175000;

        protected override void SetValue(JobAd jobAd)
        {
            jobAd.Description.Salary = new Salary
            {
                Currency = Currency.AUD,
                Rate = SalaryRate.Year,
                LowerBound = LowerBound,
                UpperBound = UpperBound,
            };
        }

        protected override void SetDisplayValue()
        {
            _salaryLowerBoundTextBox.Text = ChangedLowerBound.ToString(CultureInfo.InvariantCulture);
            _salaryUpperBoundTextBox.Text = ChangedUpperBound.ToString(CultureInfo.InvariantCulture);
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(new Salary { LowerBound = ChangedLowerBound, UpperBound = ChangedUpperBound, Currency = Currency.AUD, Rate = SalaryRate.Year }, jobAd.Description.Salary);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
        }
    }
}
