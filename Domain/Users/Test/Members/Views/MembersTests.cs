using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.Views
{
    [TestClass]
    public class MembersTests
        : ViewsTests
    {
        [TestMethod]
        public void TestName()
        {
            AssertName(true, CreateMember(false));
        }

        [TestMethod]
        public void TestEmailAddress()
        {
            AssertEmailAddress(true, CreateMember(false));
        }

        [TestMethod]
        public void TestPhoto()
        {
            AssertPhoto(true, CreateMember(false));
        }

        [TestMethod]
        public void TestAffiliateId()
        {
            AssertAffiliateId(true, CreateMember(false));
        }

        [TestMethod]
        public void TestPhoneNumbers()
        {
            AssertPhoneNumbers(true, CreateMember(false));
        }

        [TestMethod]
        public void TestGender()
        {
            AssertGender(true, CreateMember(false));
        }

        [TestMethod]
        public void TestDateOfBirth()
        {
            AssertDateOfBirth(true, CreateMember(false));
        }

        [TestMethod]
        public void TestEthnicStatus()
        {
            AssertEthnicStatus(true, CreateMember(false));
        }
    }
}
