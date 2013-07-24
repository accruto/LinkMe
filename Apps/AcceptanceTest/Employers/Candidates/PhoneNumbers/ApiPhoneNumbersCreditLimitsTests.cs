using System.Collections.Generic;
using System.Collections.Specialized;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.PhoneNumbers
{
    [TestClass]
    public class ApiPhoneNumbersCreditLimitsTests
        : ApiActionCreditLimitsTests
    {
        private const string MobilePhoneNumberFormat = "1999999{0}";
        private const string HomePhoneNumberFormat = "2999999{0}";
        private const string WorkPhoneNumberFormat = "3999999{0}";

        private ReadOnlyUrl _phoneNumbersUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _phoneNumbersUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/phonenumbers");
        }

        protected override MemberAccessReason GetReason()
        {
            return MemberAccessReason.PhoneNumberViewed;
        }

        protected override Member CreateMember(int index)
        {
            var member = base.CreateMember(index);
            member.PhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber {Number = string.Format(MobilePhoneNumberFormat, index), Type = PhoneNumberType.Mobile},
                new PhoneNumber {Number = string.Format(HomePhoneNumberFormat, index), Type = PhoneNumberType.Home},
                new PhoneNumber {Number = string.Format(WorkPhoneNumberFormat, index), Type = PhoneNumberType.Work},
            };
            _memberAccountsCommand.UpdateMember(member);
            return member;
        }

        protected override JsonResponseModel CallAction(Member member)
        {
            var parameters = new NameValueCollection {{"candidateId", member.Id.ToString()}};
            return Deserialize<JsonResponseModel>(Post(_phoneNumbersUrl, parameters));
        }
    }
}