using LinkMe.Apps.Services.External.JobSearch;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.External.JobSearch
{
    [TestClass]
    public class MapPhoneNumberTests
    {
        [TestMethod]
        public void TestEmptyNumber()
        {
            var contact = new ContactDetails {PhoneNumber = string.Empty};
            string area;
            string local;
            contact.MapPhoneNumber(false, out area, out local);

            Assert.AreEqual("00", area);
            Assert.AreEqual("00000000", local);
        }

        [TestMethod]
        public void TestFnnWithBrackets()
        {
            var contact = new ContactDetails { PhoneNumber = "(03) 8508 9111" };
            string area;
            string local;
            contact.MapPhoneNumber(false, out area, out local);

            Assert.AreEqual("03", area);
            Assert.AreEqual("85089111", local);
        }

        [TestMethod]
        public void TestFnnWithoutBrackets()
        {
            var contact = new ContactDetails { PhoneNumber = "03 8508 9111" };
            string area;
            string local;
            contact.MapPhoneNumber(false, out area, out local);

            Assert.AreEqual("03", area);
            Assert.AreEqual("85089111", local);
        }

        [TestMethod]
        public void TestFnnWithoutHyphens()
        {
            var contact = new ContactDetails { PhoneNumber = "03 8508-9111" };
            string area;
            string local;
            contact.MapPhoneNumber(false, out area, out local);

            Assert.AreEqual("03", area);
            Assert.AreEqual("85089111", local);
        }

        [TestMethod]
        public void TestFnnWithoutSpaces()
        {
            var contact = new ContactDetails { PhoneNumber = "0385089111" };
            string area;
            string local;
            contact.MapPhoneNumber(false, out area, out local);

            Assert.AreEqual("03", area);
            Assert.AreEqual("85089111", local);
        }

        [TestMethod]
        public void FreeCall()
        {
            var contact = new ContactDetails { PhoneNumber = "1300 54 65 63" };
            string area;
            string local;
            contact.MapPhoneNumber(false, out area, out local);

            Assert.AreEqual("1300", area);
            Assert.AreEqual("546563", local);
        }

        [TestMethod]
        public void TestInternationalWithArea()
        {
            var contact = new ContactDetails { PhoneNumber = "+61385089111" };
            string area;
            string local;
            contact.MapPhoneNumber(false, out area, out local);

            Assert.AreEqual("03", area);
            Assert.AreEqual("85089111", local);
        }

        [TestMethod]
        public void TestInternationalWithAreaInBrackets()
        {
            var contact = new ContactDetails { PhoneNumber = "+61(03)85089111" };
            string area;
            string local;
            contact.MapPhoneNumber(false, out area, out local);

            Assert.AreEqual("03", area);
            Assert.AreEqual("85089111", local);
        }
    }
}