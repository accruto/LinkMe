using System;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.JobDetails
{
    [TestClass]
    public class RecentProfessionTests
        : FieldsTests
    {
        protected override void AssertManualDefault()
        {
            AssertDefault();
        }

        protected override void AssertUploadDefault(IMember member, IResume resume)
        {
            AssertDefault();
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, Resume resume)
        {
        }

        protected override void Update(Member member, Candidate candidate, Resume resume, ref bool sendSuggestedJobs, ref int? referralSourceId, bool resumeUploaded)
        {
            Assert.AreNotEqual(candidate.RecentProfession, Profession.Strategy);
            candidate.RecentProfession = Profession.Strategy;
        }

        private void AssertDefault()
        {
            AssertValues();
            Assert.AreEqual(string.Empty, _recentProfessionDropDownList.SelectedItem.Value);
        }

        private void AssertValues()
        {
            var validValues = (from v in Enum.GetValues(typeof(Profession)).Cast<Profession>()
                               select v.ToString()).Concat(new[] { "" }).ToArray();
            var values = (from i in _recentProfessionDropDownList.Items
                          select i.Value).ToArray();

            Assert.AreEqual(validValues.Length, values.Length);
            Assert.IsTrue(validValues.All(values.Contains));
            Assert.IsTrue(values.All(validValues.Contains));
        }
    }
}
