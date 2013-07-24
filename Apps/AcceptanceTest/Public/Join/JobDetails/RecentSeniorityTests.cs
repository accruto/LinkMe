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
    public class RecentSeniorityTests
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
            Assert.AreNotEqual(candidate.RecentSeniority, Seniority.Internship);
            candidate.RecentSeniority = Seniority.Internship;
        }

        private void AssertDefault()
        {
            AssertValues();
            Assert.AreEqual(string.Empty, _recentSeniorityDropDownList.SelectedItem.Value);
        }

        private void AssertValues()
        {
            var validValues = (from v in Enum.GetValues(typeof(Seniority)).Cast<Seniority>()
                               select v.ToString()).Concat(new[] { "" }).ToArray();
            var values = (from i in _recentSeniorityDropDownList.Items
                          select i.Value).ToArray();

            var validTexts = (from v in validValues select GetText(v)).ToArray();
            var texts = (from i in _recentSeniorityDropDownList.Items
                         select i.Text).ToArray();

            Assert.AreEqual(validValues.Length, values.Length);
            Assert.IsTrue(validValues.All(values.Contains));
            Assert.IsTrue(values.All(validValues.Contains));
            Assert.IsTrue(validTexts.All(texts.Contains));
            Assert.IsTrue(texts.All(validTexts.Contains));
        }

        private static string GetText(string value)
        {
            switch (value)
            {
                case "MidSenior":
                    return "Mid-senior level";

                case "EntryLevel":
                    return "Entry level";

                case "NotApplicable":
                    return "Not applicable";

                default:
                    return value;
            }
        }
    }
}
