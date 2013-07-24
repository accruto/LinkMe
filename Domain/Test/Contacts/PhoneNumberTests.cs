using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Contacts
{
    [TestClass]
    public class PhoneNumberTests
    {
        private const string Number1 = "11111111";
        private const PhoneNumberType Type1 = PhoneNumberType.Mobile;
        private const string Number2 = "22222222";
        private const PhoneNumberType Type2 = PhoneNumberType.Home;
        private const string Number3 = "33333333";
        private const PhoneNumberType Type3 = PhoneNumberType.Work;

        [TestMethod]
        public void TestNullPhoneNumbers()
        {
            var member = new Member();
            Assert.IsNull(member.PhoneNumbers);
            Assert.IsNull(member.GetBestPhoneNumber());
        }

        [TestMethod]
        public void TestNoPhoneNumbers()
        {
            var member = new Member { PhoneNumbers = new List<PhoneNumber>() };
            Assert.IsNull(member.GetBestPhoneNumber());
        }

        [TestMethod]
        public void TestOnePhoneNumber()
        {
            var member = new Member
            {
                PhoneNumbers = new List<PhoneNumber>
                {
                    new PhoneNumber {Number = Number1, Type = Type1},
                }
            };

            AssertPhoneNumber(Number1, Type1, member.GetBestPhoneNumber());
        }

        [TestMethod]
        public void TestTwoPhoneNumbers()
        {
            var member = new Member
            {
                PhoneNumbers = new List<PhoneNumber>
                {
                    new PhoneNumber {Number = Number1, Type = Type1},
                    new PhoneNumber {Number = Number2, Type = Type2},
                }
            };

            AssertPhoneNumber(Number1, Type1, member.GetBestPhoneNumber());
        }

        [TestMethod]
        public void TestThreePhoneNumbers()
        {
            var member = new Member
            {
                PhoneNumbers = new List<PhoneNumber>
                {
                    new PhoneNumber {Number = Number1, Type = Type1},
                    new PhoneNumber {Number = Number2, Type = Type2},
                    new PhoneNumber {Number = Number3, Type = Type3},
                }
            };

            AssertPhoneNumber(Number1, Type1, member.GetBestPhoneNumber());
        }

        private static void AssertPhoneNumber(string expectedNumber, PhoneNumberType expectedType, PhoneNumber phoneNumber)
        {
            Assert.IsNotNull(phoneNumber);
            Assert.AreEqual(expectedNumber, phoneNumber.Number);
            Assert.AreEqual(expectedType, phoneNumber.Type);
        }
    }
}
