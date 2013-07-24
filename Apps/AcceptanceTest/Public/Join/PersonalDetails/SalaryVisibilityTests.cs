using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.PersonalDetails
{
    [TestClass]
    public class SalaryVisibilityTests
        : FieldsTests
    {
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
        }

        protected override void Update(Member member, Candidate candidate, ref string password, bool resumeUploaded)
        {
            Assert.IsTrue(member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Salary));
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Salary);
        }
        
        private void AssertDefaults()
        {
            Assert.IsFalse(_notSalaryVisibilityCheckBox.IsChecked);
        }
    }
}
