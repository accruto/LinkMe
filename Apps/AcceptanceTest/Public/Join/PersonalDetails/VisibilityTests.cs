using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.PersonalDetails
{
    [TestClass]
    public class VisibilityTests
        : FieldsTests
    {
        protected override void AssertManualDefault()
        {
            AssertDefault();
        }

        protected override void AssertManualJoinedDefault(IMember member)
        {
            AssertDefault();
        }

        protected override void AssertUploadDefault(IMember member)
        {
            AssertDefault();
        }

        protected override void AssertUploadJoinedDefault(IMember member)
        {
            AssertDefault();
        }

        protected override void TestErrors(Guid instanceId, Member member, Candidate candidate, bool alreadyLoggedIn)
        {
        }

        protected override void Update(Member member, Candidate candidate, ref string password, bool resumeUploaded)
        {
            Assert.IsTrue(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.PhoneNumbers));
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.PhoneNumbers);
        }

        private void AssertDefault()
        {
            Assert.IsTrue(_resumeVisibilityCheckBox.IsChecked);
            Assert.IsTrue(_nameVisibilityCheckBox.IsChecked);
            Assert.IsTrue(_phoneNumbersVisibilityCheckBox.IsChecked);
            Assert.IsTrue(_recentEmployersVisibilityCheckBox.IsChecked);
        }
    }
}
