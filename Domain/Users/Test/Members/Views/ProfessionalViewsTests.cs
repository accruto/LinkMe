using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.Views
{
    [TestClass]
    public class ProfessionalViewsTests
        : ViewsTests
    {
        // Name subject to settings.

        [TestMethod]
        public void TestResumeOnOthersOnName()
        {
            AssertName(false, CreateView(true, true, ProfessionalContactDegree.Public, false));
            AssertName(false, CreateView(true, true, ProfessionalContactDegree.NotContacted, false));
            AssertName(true, CreateView(true, true, ProfessionalContactDegree.Contacted, true));
            AssertName(true, CreateView(true, true, ProfessionalContactDegree.Applicant, false));
            AssertName(true, CreateView(true, true, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOnOthersOffName()
        {
            AssertName(false, CreateView(true, false, ProfessionalContactDegree.Public, false));
            AssertName(false, CreateView(true, false, ProfessionalContactDegree.NotContacted, false));
            AssertName(false, CreateView(true, false, ProfessionalContactDegree.Contacted, true));
            AssertName(true, CreateView(true, false, ProfessionalContactDegree.Applicant, false));
            AssertName(true, CreateView(true, false, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOnName()
        {
            AssertName(false, CreateView(false, true, ProfessionalContactDegree.Public, false));
            AssertName(false, CreateView(false, true, ProfessionalContactDegree.NotContacted, false));
            AssertName(false, CreateView(false, true, ProfessionalContactDegree.Contacted, true));
            AssertName(true, CreateView(false, true, ProfessionalContactDegree.Applicant, false));
            AssertName(true, CreateView(false, true, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOffName()
        {
            AssertName(false, CreateView(false, false, ProfessionalContactDegree.Public, false));
            AssertName(false, CreateView(false, false, ProfessionalContactDegree.NotContacted, false));
            AssertName(false, CreateView(false, false, ProfessionalContactDegree.Contacted, true));
            AssertName(true, CreateView(false, false, ProfessionalContactDegree.Applicant, false));
            AssertName(true, CreateView(false, false, ProfessionalContactDegree.Self, false));
        }

        // Email address subject to settings, but not resume

        [TestMethod]
        public void TestResumeOnOthersOnEmailAddress()
        {
            AssertEmailAddress(false, CreateView(true, true, ProfessionalContactDegree.Public, false));
            AssertEmailAddress(false, CreateView(true, true, ProfessionalContactDegree.NotContacted, false));
            AssertEmailAddress(true, CreateView(true, true, ProfessionalContactDegree.Contacted, true));
            AssertEmailAddress(true, CreateView(true, true, ProfessionalContactDegree.Applicant, false));
            AssertEmailAddress(true, CreateView(true, true, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOnOthersOffEmailAddress()
        {
            AssertEmailAddress(false, CreateView(true, false, ProfessionalContactDegree.Public, false));
            AssertEmailAddress(false, CreateView(true, false, ProfessionalContactDegree.NotContacted, false));
            AssertEmailAddress(true, CreateView(true, false, ProfessionalContactDegree.Contacted, true));
            AssertEmailAddress(true, CreateView(true, false, ProfessionalContactDegree.Applicant, false));
            AssertEmailAddress(true, CreateView(true, false, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOnEmailAddress()
        {
            AssertEmailAddress(false, CreateView(false, true, ProfessionalContactDegree.Public, false));
            AssertEmailAddress(false, CreateView(false, true, ProfessionalContactDegree.NotContacted, false));
            AssertEmailAddress(false, CreateView(false, true, ProfessionalContactDegree.Contacted, true));
            AssertEmailAddress(true, CreateView(false, true, ProfessionalContactDegree.Applicant, false));
            AssertEmailAddress(true, CreateView(false, true, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOffEmailAddress()
        {
            AssertEmailAddress(false, CreateView(false, false, ProfessionalContactDegree.Public, false));
            AssertEmailAddress(false, CreateView(false, false, ProfessionalContactDegree.NotContacted, false));
            AssertEmailAddress(false, CreateView(false, false, ProfessionalContactDegree.Contacted, true));
            AssertEmailAddress(true, CreateView(false, false, ProfessionalContactDegree.Applicant, false));
            AssertEmailAddress(true, CreateView(false, false, ProfessionalContactDegree.Self, false));
        }

        // Photo subject to settings.

        [TestMethod]
        public void TestResumeOnOthersOnPhoto()
        {
            AssertPhoto(false, CreateView(true, true, ProfessionalContactDegree.Public, false));
            AssertPhoto(false, CreateView(true, true, ProfessionalContactDegree.NotContacted, false));
            AssertPhoto(true, CreateView(true, true, ProfessionalContactDegree.Contacted, true));
            AssertPhoto(true, CreateView(true, true, ProfessionalContactDegree.Applicant, false));
            AssertPhoto(true, CreateView(true, true, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOnOthersOffPhoto()
        {
            AssertPhoto(false, CreateView(true, false, ProfessionalContactDegree.Public, false));
            AssertPhoto(false, CreateView(true, false, ProfessionalContactDegree.NotContacted, false));
            AssertPhoto(false, CreateView(true, false, ProfessionalContactDegree.Contacted, true));
            AssertPhoto(false, CreateView(true, false, ProfessionalContactDegree.Applicant, false));
            AssertPhoto(true, CreateView(true, false, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOnPhoto()
        {
            AssertPhoto(false, CreateView(false, true, ProfessionalContactDegree.Public, false));
            AssertPhoto(false, CreateView(false, true, ProfessionalContactDegree.NotContacted, false));
            AssertPhoto(false, CreateView(false, true, ProfessionalContactDegree.Contacted, true));
            AssertPhoto(true, CreateView(false, true, ProfessionalContactDegree.Applicant, false));
            AssertPhoto(true, CreateView(false, true, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOffPhoto()
        {
            AssertPhoto(false, CreateView(false, false, ProfessionalContactDegree.Public, false));
            AssertPhoto(false, CreateView(false, false, ProfessionalContactDegree.NotContacted, false));
            AssertPhoto(false, CreateView(false, false, ProfessionalContactDegree.Contacted, true));
            AssertPhoto(false, CreateView(false, false, ProfessionalContactDegree.Applicant, false));
            AssertPhoto(true, CreateView(false, false, ProfessionalContactDegree.Self, false));
        }

        // Affiliate subject to settings.

        [TestMethod]
        public void TestResumeOnOthersOnAffiliateId()
        {
            AssertAffiliateId(false, CreateView(true, true, ProfessionalContactDegree.Public, false));
            AssertAffiliateId(true, CreateView(true, true, ProfessionalContactDegree.NotContacted, false));
            AssertAffiliateId(true, CreateView(true, true, ProfessionalContactDegree.Contacted, true));
            AssertAffiliateId(true, CreateView(true, true, ProfessionalContactDegree.Applicant, false));
            AssertAffiliateId(true, CreateView(true, true, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOnOthersOffAffiliateId()
        {
            AssertAffiliateId(false, CreateView(true, false, ProfessionalContactDegree.Public, false));
            AssertAffiliateId(false, CreateView(true, false, ProfessionalContactDegree.NotContacted, false));
            AssertAffiliateId(false, CreateView(true, false, ProfessionalContactDegree.Contacted, true));
            AssertAffiliateId(true, CreateView(true, false, ProfessionalContactDegree.Applicant, false));
            AssertAffiliateId(true, CreateView(true, false, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOnAffiliateId()
        {
            AssertAffiliateId(false, CreateView(false, true, ProfessionalContactDegree.Public, false));
            AssertAffiliateId(false, CreateView(false, true, ProfessionalContactDegree.NotContacted, false));
            AssertAffiliateId(false, CreateView(false, true, ProfessionalContactDegree.Contacted, true));
            AssertAffiliateId(true, CreateView(false, true, ProfessionalContactDegree.Applicant, false));
            AssertAffiliateId(true, CreateView(false, true, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOffAffiliateId()
        {
            AssertAffiliateId(false, CreateView(false, false, ProfessionalContactDegree.Public, false));
            AssertAffiliateId(false, CreateView(false, false, ProfessionalContactDegree.NotContacted, false));
            AssertAffiliateId(false, CreateView(false, false, ProfessionalContactDegree.Contacted, true));
            AssertAffiliateId(true, CreateView(false, false, ProfessionalContactDegree.Applicant, false));
            AssertAffiliateId(true, CreateView(false, false, ProfessionalContactDegree.Self, false));
        }

        // Phone numbers subject to settings.

        [TestMethod]
        public void TestResumeOnOthersOnPhoneNumbers()
        {
            AssertPhoneNumbers(false, CreateView(true, true, ProfessionalContactDegree.Public, false));
            AssertPhoneNumbers(false, CreateView(true, true, ProfessionalContactDegree.NotContacted, false));
            AssertPhoneNumbers(true, CreateView(true, true, ProfessionalContactDegree.Contacted, true));
            AssertPhoneNumbers(true, CreateView(true, true, ProfessionalContactDegree.Applicant, false));
            AssertPhoneNumbers(true, CreateView(true, true, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOnOthersOffPhoneNumbers()
        {
            AssertPhoneNumbers(false, CreateView(true, false, ProfessionalContactDegree.Public, false));
            AssertPhoneNumbers(false, CreateView(true, false, ProfessionalContactDegree.NotContacted, false));
            AssertPhoneNumbers(false, CreateView(true, false, ProfessionalContactDegree.Contacted, true));
            AssertPhoneNumbers(true, CreateView(true, false, ProfessionalContactDegree.Applicant, false));
            AssertPhoneNumbers(true, CreateView(true, false, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOnPhoneNumbers()
        {
            AssertPhoneNumbers(false, CreateView(false, true, ProfessionalContactDegree.Public, false));
            AssertPhoneNumbers(false, CreateView(false, true, ProfessionalContactDegree.NotContacted, false));
            AssertPhoneNumbers(false, CreateView(false, true, ProfessionalContactDegree.Contacted, true));
            AssertPhoneNumbers(true, CreateView(false, true, ProfessionalContactDegree.Applicant, false));
            AssertPhoneNumbers(true, CreateView(false, true, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOffPhoneNumbers()
        {
            AssertPhoneNumbers(false, CreateView(false, false, ProfessionalContactDegree.Public, false));
            AssertPhoneNumbers(false, CreateView(false, false, ProfessionalContactDegree.NotContacted, false));
            AssertPhoneNumbers(false, CreateView(false, false, ProfessionalContactDegree.Contacted, true));
            AssertPhoneNumbers(true, CreateView(false, false, ProfessionalContactDegree.Applicant, false));
            AssertPhoneNumbers(true, CreateView(false, false, ProfessionalContactDegree.Self, false));
        }

        // Gender not visible.

        [TestMethod]
        public void TestResumeOnOthersOnGender()
        {
            AssertGender(false, CreateView(true, true, ProfessionalContactDegree.Public, false));
            AssertGender(false, CreateView(true, true, ProfessionalContactDegree.NotContacted, false));
            AssertGender(false, CreateView(true, true, ProfessionalContactDegree.Contacted, true));
            AssertGender(false, CreateView(true, true, ProfessionalContactDegree.Applicant, false));
            AssertGender(false, CreateView(true, true, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOnOthersOffGender()
        {
            AssertGender(false, CreateView(true, false, ProfessionalContactDegree.Public, false));
            AssertGender(false, CreateView(true, false, ProfessionalContactDegree.NotContacted, false));
            AssertGender(false, CreateView(true, false, ProfessionalContactDegree.Contacted, true));
            AssertGender(false, CreateView(true, false, ProfessionalContactDegree.Applicant, false));
            AssertGender(false, CreateView(true, false, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOnGender()
        {
            AssertGender(false, CreateView(false, true, ProfessionalContactDegree.Public, false));
            AssertGender(false, CreateView(false, true, ProfessionalContactDegree.NotContacted, false));
            AssertGender(false, CreateView(false, true, ProfessionalContactDegree.Contacted, true));
            AssertGender(false, CreateView(false, true, ProfessionalContactDegree.Applicant, false));
            AssertGender(false, CreateView(false, true, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOffGender()
        {
            AssertGender(false, CreateView(false, false, ProfessionalContactDegree.Public, false));
            AssertGender(false, CreateView(false, false, ProfessionalContactDegree.NotContacted, false));
            AssertGender(false, CreateView(false, false, ProfessionalContactDegree.Contacted, true));
            AssertGender(false, CreateView(false, false, ProfessionalContactDegree.Applicant, false));
            AssertGender(false, CreateView(false, false, ProfessionalContactDegree.Self, false));
        }

        // Date of birth not visible.

        [TestMethod]
        public void TestResumeOnOthersOnDateOfBirth()
        {
            AssertDateOfBirth(false, CreateView(true, true, ProfessionalContactDegree.Public, false));
            AssertDateOfBirth(false, CreateView(true, true, ProfessionalContactDegree.NotContacted, false));
            AssertDateOfBirth(false, CreateView(true, true, ProfessionalContactDegree.Contacted, true));
            AssertDateOfBirth(false, CreateView(true, true, ProfessionalContactDegree.Applicant, false));
            AssertDateOfBirth(false, CreateView(true, true, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOnOthersOffDateOfBirth()
        {
            AssertDateOfBirth(false, CreateView(true, false, ProfessionalContactDegree.Public, false));
            AssertDateOfBirth(false, CreateView(true, false, ProfessionalContactDegree.NotContacted, false));
            AssertDateOfBirth(false, CreateView(true, false, ProfessionalContactDegree.Contacted, true));
            AssertDateOfBirth(false, CreateView(true, false, ProfessionalContactDegree.Applicant, false));
            AssertDateOfBirth(false, CreateView(true, false, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOnDateOfBirth()
        {
            AssertDateOfBirth(false, CreateView(false, true, ProfessionalContactDegree.Public, false));
            AssertDateOfBirth(false, CreateView(false, true, ProfessionalContactDegree.NotContacted, false));
            AssertDateOfBirth(false, CreateView(false, true, ProfessionalContactDegree.Contacted, true));
            AssertDateOfBirth(false, CreateView(false, true, ProfessionalContactDegree.Applicant, false));
            AssertDateOfBirth(false, CreateView(false, true, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOffDateOfBirth()
        {
            AssertDateOfBirth(false, CreateView(false, false, ProfessionalContactDegree.Public, false));
            AssertDateOfBirth(false, CreateView(false, false, ProfessionalContactDegree.NotContacted, false));
            AssertDateOfBirth(false, CreateView(false, false, ProfessionalContactDegree.Contacted, true));
            AssertDateOfBirth(false, CreateView(false, false, ProfessionalContactDegree.Applicant, false));
            AssertDateOfBirth(false, CreateView(false, false, ProfessionalContactDegree.Self, false));
        }

        // Ethnic status always visible.

        [TestMethod]
        public void TestResumeOnOthersOnEthnicStatus()
        {
            AssertEthnicStatus(true, CreateView(true, true, ProfessionalContactDegree.Public, false));
            AssertEthnicStatus(true, CreateView(true, true, ProfessionalContactDegree.NotContacted, false));
            AssertEthnicStatus(true, CreateView(true, true, ProfessionalContactDegree.Contacted, true));
            AssertEthnicStatus(true, CreateView(true, true, ProfessionalContactDegree.Applicant, false));
            AssertEthnicStatus(true, CreateView(true, true, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOnOthersOffEthnicStatus()
        {
            AssertEthnicStatus(true, CreateView(true, false, ProfessionalContactDegree.Public, false));
            AssertEthnicStatus(true, CreateView(true, false, ProfessionalContactDegree.NotContacted, false));
            AssertEthnicStatus(true, CreateView(true, false, ProfessionalContactDegree.Contacted, true));
            AssertEthnicStatus(true, CreateView(true, false, ProfessionalContactDegree.Applicant, false));
            AssertEthnicStatus(true, CreateView(true, false, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOnEthnicStatus()
        {
            AssertEthnicStatus(true, CreateView(false, true, ProfessionalContactDegree.Public, false));
            AssertEthnicStatus(true, CreateView(false, true, ProfessionalContactDegree.NotContacted, false));
            AssertEthnicStatus(true, CreateView(false, true, ProfessionalContactDegree.Contacted, true));
            AssertEthnicStatus(true, CreateView(false, true, ProfessionalContactDegree.Applicant, false));
            AssertEthnicStatus(true, CreateView(false, true, ProfessionalContactDegree.Self, false));
        }

        [TestMethod]
        public void TestResumeOffOthersOffEthnicStatus()
        {
            AssertEthnicStatus(true, CreateView(false, false, ProfessionalContactDegree.Public, false));
            AssertEthnicStatus(true, CreateView(false, false, ProfessionalContactDegree.NotContacted, false));
            AssertEthnicStatus(true, CreateView(false, false, ProfessionalContactDegree.Contacted, true));
            AssertEthnicStatus(true, CreateView(false, false, ProfessionalContactDegree.Applicant, false));
            AssertEthnicStatus(true, CreateView(false, false, ProfessionalContactDegree.Self, false));
        }

        protected Member CreateMember(bool isResumeOn, bool areOthersOn)
        {
            var member = CreateMember(areOthersOn);

            member.VisibilitySettings.Professional.EmploymentVisibility = isResumeOn
                ? member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.Resume)
                : member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Resume);

            return member;
        }

        protected ProfessionalView CreateView(bool isResumeOn, bool areOthersOn, ProfessionalContactDegree contactDegree, bool hasBeenAccessed)
        {
            return CreateView(CreateMember(isResumeOn, areOthersOn), contactDegree, hasBeenAccessed);
        }

        protected ProfessionalView CreateView(Member member, ProfessionalContactDegree contactDegree, bool hasBeenAccessed)
        {
            return CreateView(member, null, contactDegree, hasBeenAccessed, false);
        }

        protected virtual ProfessionalView CreateView(Member member, int? contactCredits, ProfessionalContactDegree contactDegree, bool hasBeenAccessed, bool isRepresented)
        {
            return new ProfessionalView(member, contactCredits, contactDegree, hasBeenAccessed, isRepresented);
        }
    }
}