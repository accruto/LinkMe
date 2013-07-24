using System;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.PersonalDetails
{
    [TestClass]
    public class DesiredSalaryTests
        : FieldsTests
    {
        private static readonly decimal? NewLowerBound = 100;
        private const SalaryRate NewSalaryRate = SalaryRate.Hour;

        protected override void AssertManualDefault()
        {
            AssertDefaults();
        }

        protected override void AssertManualJoinedDefault(IMember member)
        {
            AssertDefaults();
        }

        protected override void AssertUploadDefault(IMember member)
        {
            AssertDefaults();
        }

        protected override void AssertUploadJoinedDefault(IMember member)
        {
            AssertDefaults();
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, bool alreadyLoggedIn)
        {
            candidate.DesiredSalary = null;
            TestErrors(instanceId, member, candidate, alreadyLoggedIn, "Please specify a minimum expected salary.");
            candidate.DesiredSalary = new Salary { LowerBound = null, Rate = NewSalaryRate, Currency = Currency.AUD };
            TestErrors(instanceId, member, candidate, alreadyLoggedIn, "Please specify a minimum expected salary.");
        }

        protected override void Update(Member member, Candidate candidate, ref string password, bool resumeUploaded)
        {
            if (candidate.DesiredSalary != null)
                Assert.AreNotEqual(candidate.DesiredSalary.LowerBound, NewLowerBound);
            candidate.DesiredSalary = new Salary { LowerBound = NewLowerBound, Rate = NewSalaryRate, Currency = Currency.AUD };
        }

        private void AssertDefaults()
        {
            Assert.AreEqual("", _salaryLowerBoundTextBox.Text);
            Assert.IsTrue(_salaryRateYearRadioBox.IsChecked);
            Assert.IsFalse(_salaryRateHourRadioBox.IsChecked);
        }
    }
}
