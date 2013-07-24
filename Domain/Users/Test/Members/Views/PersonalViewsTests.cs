using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.Views
{
    [TestClass]
    public class PersonalViewsTests
        : ViewsTests
    {
        // Name subject to settings.

        [TestMethod]
        public void TestAccessName()
        {
            AssertName(true, CreateAccessView());
        }

        [TestMethod]
        public void TestNoAccessName()
        {
            AssertName(false, CreateNoAccessView());
        }

        // Email address subject to settings.

        [TestMethod]
        public void TestAccessEmailAddress()
        {
            AssertEmailAddress(true, CreateAccessView());
        }

        [TestMethod]
        public void TestNoAccessEmailAddress()
        {
            AssertEmailAddress(false, CreateNoAccessView());
        }

        // Photo subject to settings.

        [TestMethod]
        public void TestAccessPhoto()
        {
            AssertPhoto(true, CreateAccessView());
        }

        [TestMethod]
        public void TestNoAccessPhoto()
        {
            AssertPhoto(false, CreateNoAccessView());
        }

        // Affiliate not visible.

        [TestMethod]
        public void TestAccessAffiliateId()
        {
            AssertAffiliateId(false, CreateAccessView());
        }

        [TestMethod]
        public void TestNoAccessAffiliateId()
        {
            AssertAffiliateId(false, CreateNoAccessView());
        }

        // Phone numbers subject to settings.

        [TestMethod]
        public void TestAccessPhoneNumbers()
        {
            AssertPhoneNumbers(true, CreateAccessView());
        }

        [TestMethod]
        public void TestNoAccessPhoneNumbers()
        {
            AssertPhoneNumbers(false, CreateNoAccessView());
        }

        // Gender subject to settings.

        [TestMethod]
        public void TestAccessGender()
        {
            AssertGender(true, CreateAccessView());
        }

        [TestMethod]
        public void TestNoAccessGender()
        {
            AssertGender(false, CreateNoAccessView());
        }

        // Date of birth subject to settings.

        [TestMethod]
        public void TestAccessDateOfBirth()
        {
            AssertDateOfBirth(true, CreateAccessView());
        }

        [TestMethod]
        public void TestNoAccessDateOfBirth()
        {
            AssertDateOfBirth(false, CreateNoAccessView());
        }

        // Ethnic status not visible.

        [TestMethod]
        public void TestAccessEthnicStatus()
        {
            AssertEthnicStatus(false, CreateAccessView());
        }

        [TestMethod]
        public void TestNoAccessEthnicStatus()
        {
            AssertEthnicStatus(false, CreateNoAccessView());
        }

        private PersonalView CreateAccessView()
        {
            return new PersonalView(CreateMember(true), PersonalContactDegree.FirstDegree, PersonalContactDegree.FirstDegree);
        }

        private PersonalView CreateNoAccessView()
        {
            return new PersonalView(CreateMember(false), PersonalContactDegree.FirstDegree, PersonalContactDegree.FirstDegree);
        }
    }
}