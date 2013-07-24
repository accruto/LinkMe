using System;
using System.Globalization;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.JobDetails
{
    [TestClass]
    public class DateOfBirthTests
        : FieldsTests
    {
        protected override void AssertManualDefault()
        {
            AssertValues();
            Assert.AreEqual("", _dateOfBirthMonthDropDownList.SelectedItem.Value);
            Assert.AreEqual("", _dateOfBirthYearDropDownList.SelectedItem.Value);
        }

        protected override void AssertUploadDefault(IMember member, IResume resume)
        {
            Assert.AreEqual(member.DateOfBirth, GetSelectedDateOfBirth());
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, Resume resume)
        {
            member.DateOfBirth = null;
            TestErrors(instanceId, member, candidate, resume, "The date of birth is required.");
        }

        protected override void Update(Member member, Candidate candidate, Resume resume, ref bool sendSuggestedJobs, ref int? referralSourceId, bool resumeUploaded)
        {
            var newDate = new PartialDate(1972, 8);
            Assert.AreNotEqual(member.DateOfBirth, newDate);
            member.DateOfBirth = newDate;
        }

        private void AssertValues()
        {
            var validMonths = new[] {""}.Concat(from i in Enumerable.Range(1, 12) select i.ToString(CultureInfo.InvariantCulture)).ToArray();
            var months = (from i in _dateOfBirthMonthDropDownList.Items
                          select i.Value).ToArray();

            Assert.AreEqual(validMonths.Length, months.Length);
            Assert.AreEqual(12, months.Max(v => v == "" ? 0 : int.Parse(v)));
            Assert.IsTrue(validMonths.All(months.Contains));

            var validYears = new[] { "" }.Concat(from i in Enumerable.Range(1900, DateTime.Now.Year - 1900 + 1) select i.ToString(CultureInfo.InvariantCulture)).ToArray();
            var years = (from i in _dateOfBirthYearDropDownList.Items
                         select i.Value).ToArray();

            Assert.AreEqual(validYears.Length, years.Length);
            Assert.AreEqual(DateTime.Now.Year, years.Max(v => v == "" ? 0 : int.Parse(v)));
            Assert.IsTrue(validYears.All(years.Contains));
        }
    }
}
