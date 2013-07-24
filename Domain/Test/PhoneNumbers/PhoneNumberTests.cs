using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.PhoneNumbers.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.PhoneNumbers
{
    [TestClass]
    public class PhoneNumberTests
        : TestClass
    {
        private readonly IPhoneNumbersQuery _phoneNumbersQuery = Resolve<IPhoneNumbersQuery>();
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();

        [TestMethod]
        public void TestPhoneNumberTypes()
        {
            var australia = _locationQuery.GetCountry("Australia");
            var india = _locationQuery.GetCountry("India");

            Assert.AreEqual(PhoneNumberType.Mobile, _phoneNumbersQuery.GetPhoneNumberType("0412121212", australia));
            Assert.AreEqual(PhoneNumberType.Work, _phoneNumbersQuery.GetPhoneNumberType("0512121212", australia));
            Assert.AreEqual(PhoneNumberType.Work, _phoneNumbersQuery.GetPhoneNumberType("98888888", australia));
            Assert.AreEqual(PhoneNumberType.Mobile, _phoneNumbersQuery.GetPhoneNumberType("  0421323332", australia));

            Assert.AreEqual(PhoneNumberType.Work, _phoneNumbersQuery.GetPhoneNumberType("0412121212", india));
            Assert.AreEqual(PhoneNumberType.Work, _phoneNumbersQuery.GetPhoneNumberType("0512121212", india));
            Assert.AreEqual(PhoneNumberType.Work, _phoneNumbersQuery.GetPhoneNumberType("98888888", india));
            Assert.AreEqual(PhoneNumberType.Work, _phoneNumbersQuery.GetPhoneNumberType("  0421323332", india));
        }
    }
}
