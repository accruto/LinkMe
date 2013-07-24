using System.Collections.Specialized;
using System.Globalization;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Api
{
    [TestClass]
    public class ContactDetailsTests
        : ApiTests
    {
        private ReadOnlyUrl _contactDetailsUrl;

        private const string NewFirstName = "Barney";
        private const string NewLastName = "Gumble";
        private const string Country = "Australia";
        private const string NewCountry = "New Zealand";
        private const string NewLocation = "Dunedin";
        private const string NewEmailAddress = "bgumble@test.linkme.net.au";
        private const string NewSecondaryEmailAddress = "moe@test.linkme.net.au";
        private const string NewPhoneNumber = "88889999";
        private const PhoneNumberType NewPhoneNumberType = PhoneNumberType.Home;
        private const string NewSecondaryPhoneNumber = "66667777";
        private const PhoneNumberType NewSecondaryPhoneNumberType = PhoneNumberType.Mobile;
        private const string NewCitizenship = "Dutch";
        private const string UpdatedCitizenship = "Polish";

        [TestInitialize]
        public void TestInitialize()
        {
            _contactDetailsUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/contactdetails");
        }

        [TestMethod]
        public void TestRequired()
        {
            var member = CreateMember();
            LogIn(member);

            var model = ContactDetails(new NameValueCollection());
            AssertJsonErrors(model,
                "FirstName", "The first name is required.",
                "LastName", "The last name is required.",
                "Location", "The location is required.",
                "EmailAddress", "The email address is required.",
                "PhoneNumber", "The phone number is required.",
                "VisaStatus", "The visa status is required.",
                "Gender", "The gender is required.",
                "DateOfBirth", "The date of birth is required.");
        }

        [TestMethod]
        public void TestFirstName()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            member.FirstName = NewFirstName;
            var parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);
        }

        [TestMethod]
        public void TestFirstNameErrors()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            var parameters = GetParameters(member, candidate, null);

            parameters["FirstName"] = null;
            AssertJsonError(ContactDetails(parameters), "FirstName", "The first name is required.");

            parameters["FirstName"] = "a";
            AssertJsonError(ContactDetails(parameters), "FirstName", "The first name must be between 2 and 30 characters in length and not have any invalid characters.");

            parameters["FirstName"] = new string('a', 200);
            AssertJsonError(ContactDetails(parameters), "FirstName", "The first name must be between 2 and 30 characters in length and not have any invalid characters.");
        }

        [TestMethod]
        public void TestLastName()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            member.LastName = NewLastName;
            var parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);
        }

        [TestMethod]
        public void TestLastNameErrors()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            var parameters = GetParameters(member, candidate, null);

            parameters["LastName"] = null;
            AssertJsonError(ContactDetails(parameters), "LastName", "The last name is required.");

            parameters["LastName"] = "a";
            AssertJsonError(ContactDetails(parameters), "LastName", "The last name must be between 2 and 30 characters in length and not have any invalid characters.");

            parameters["LastName"] = new string('a', 200);
            AssertJsonError(ContactDetails(parameters), "LastName", "The last name must be between 2 and 30 characters in length and not have any invalid characters.");
        }

        [TestMethod]
        public void TestLocation()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            member.Address.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(NewCountry), NewLocation);
            var parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);
        }

        [TestMethod]
        public void TestResolvedLocation()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // This resolves into that via SoundEx.

            const string location = "sdaafsdfsdf";
            const string resolvedLocation = "Steppes TAS 7030";

            member.Address.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), resolvedLocation);
            var parameters = GetParameters(member, candidate, null);
            parameters["Location"] = location;

            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);
        }

        [TestMethod]
        public void TestLocationErrors()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            var parameters = GetParameters(member, candidate, null);

            parameters["Location"] = null;
            AssertJsonError(ContactDetails(parameters), "Location", "The location is required.");

            parameters["Location"] = "aaaa";
            AssertJsonError(ContactDetails(parameters), "Location", "The location must be a valid postal location.");
        }

        [TestMethod]
        public void TestEmailAddresses()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Primary email address only.

            member.EmailAddresses = new[]
            {
                new EmailAddress { Address = NewEmailAddress, IsVerified = false }
            };
            var parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);

            // Primary and secondary email addresses.

            member.EmailAddresses = new[]
            {
                new EmailAddress { Address = NewEmailAddress, IsVerified = false },
                new EmailAddress { Address = NewSecondaryEmailAddress, IsVerified = false }
            };
            parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);

            // Go back to single email address.

            member.EmailAddresses = new[]
            {
                new EmailAddress { Address = NewEmailAddress, IsVerified = false }
            };
            parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);
        }

        [TestMethod]
        public void TestEmailAddressErrors()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            var parameters = GetParameters(member, candidate, null);

            parameters["EmailAddress"] = null;
            AssertJsonError(ContactDetails(parameters), "EmailAddress", "The email address is required.");

            parameters["EmailAddress"] = "bogusemail.com";
            AssertJsonError(ContactDetails(parameters), "EmailAddress", "The email address must be valid and have less than 320 characters.");

            parameters["EmailAddress"] = member.EmailAddresses[0].Address;
            parameters["SecondaryEmailAddress"] = "bogusemail.com";
            AssertJsonError(ContactDetails(parameters), "SecondaryEmailAddress", "The secondary email address must be valid and have less than 320 characters.");
        }

        [TestMethod]
        public void TestPhoneNumbers()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Primary phone number only.

            member.PhoneNumbers = new[]
            {
                new PhoneNumber { Number = NewPhoneNumber, Type = NewPhoneNumberType }
            };
            var parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);

            // Primary and secondary phone numbers.

            member.PhoneNumbers = new[]
            {
                new PhoneNumber { Number = NewPhoneNumber, Type = NewPhoneNumberType },
                new PhoneNumber { Number = NewSecondaryPhoneNumber, Type = NewSecondaryPhoneNumberType }
            };
            parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);

            // Go back to single phone number.

            member.PhoneNumbers = new[]
            {
                new PhoneNumber { Number = NewPhoneNumber, Type = NewPhoneNumberType }
            };
            parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);
        }

        [TestMethod]
        public void TestPhoneNumberErrors()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            var parameters = GetParameters(member, candidate, null);

            parameters["PhoneNumber"] = null;
            AssertJsonError(ContactDetails(parameters), "PhoneNumber", "The phone number is required.");

            parameters["PhoneNumber"] = "1111";
            AssertJsonError(ContactDetails(parameters), "PhoneNumber", "The phone number must be between 8 and 20 characters in length and not have any invalid characters.");

            parameters["PhoneNumber"] = "abcdabcdabcd";
            AssertJsonError(ContactDetails(parameters), "PhoneNumber", "The phone number must be between 8 and 20 characters in length and not have any invalid characters.");

            parameters["PhoneNumber"] = member.PhoneNumbers[0].Number;
            parameters["SecondaryPhoneNumber"] = "1111";
            AssertJsonError(ContactDetails(parameters), "SecondaryPhoneNumber", "The secondary phone number must be between 8 and 20 characters in length and not have any invalid characters.");

            parameters["SecondaryPhoneNumber"] = "abcdabcdabcd";
            AssertJsonError(ContactDetails(parameters), "SecondaryPhoneNumber", "The secondary phone number must be between 8 and 20 characters in length and not have any invalid characters.");
        }

        [TestMethod]
        public void TestVisaStatus()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set visa status.

            candidate.VisaStatus = VisaStatus.RestrictedWorkVisa;
            var parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);

            // Change it.

            candidate.VisaStatus = VisaStatus.Citizen;
            parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);
        }

        [TestMethod]
        public void TestEthnicStatus()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set status.

            member.EthnicStatus = EthnicStatus.Aboriginal;
            var parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);

            // Change it.

            member.EthnicStatus = EthnicStatus.TorresIslander;
            parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);

            // Set both.

            member.EthnicStatus = EthnicStatus.TorresIslander | EthnicStatus.Aboriginal;
            parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);

            // Reset it.

            member.EthnicStatus = EthnicStatus.None;
            parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);
        }

        [TestMethod]
        public void TestGender()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set status.

            member.Gender = Gender.Female;
            var parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);

            // Change it.

            member.Gender = Gender.Male;
            parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);
        }

        [TestMethod]
        public void TestGenderErrors()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            var parameters = GetParameters(member, candidate, null);

            parameters["Gender"] = null;
            AssertJsonError(ContactDetails(parameters), "Gender", "The gender is required.");
        }

        [TestMethod]
        public void TestDateOfBirth()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            member.DateOfBirth = new PartialDate(1970, 1);
            var parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);

            // Change it.

            member.DateOfBirth = new PartialDate(1969, 12);
            parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, null, true);
        }

        [TestMethod]
        public void TestDateOfBirthErrors()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            var parameters = GetParameters(member, candidate, null);

            parameters["DateOfBirthMonth"] = null;
            AssertJsonError(ContactDetails(parameters), "DateOfBirth", "The date of birth is required.");

            parameters["DateOfBirthYear"] = null;
            AssertJsonError(ContactDetails(parameters), "DateOfBirth", "The date of birth is required.");
        }

        [TestMethod]
        public void TestCitizenship()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            var resume = new Resume { Citizenship = NewCitizenship };
            var parameters = GetParameters(member, candidate, resume);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, resume, true);

            // Change it.

            resume.Citizenship = UpdatedCitizenship;
            parameters = GetParameters(member, candidate, resume);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, resume, true);

            // Reset it.

            resume.Citizenship = null;
            parameters = GetParameters(member, candidate, null);
            AssertJsonSuccess(ContactDetails(parameters));
            AssertMember(member, candidate, resume, true);
        }

        [TestMethod]
        public void TestAllErrors()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            var parameters = GetParameters(member, candidate, null);

            parameters["FirstName"] = "a";
            parameters["LastName"] = new string('a', 200);
            parameters["Location"] = "aaaa";
            parameters["EmailAddress"] = "bogusemail.com";
            parameters["SecondaryEmailAddress"] = "bogusemail.com";
            parameters["PhoneNumber"] = "abcdabcdabcd";
            parameters["SecondaryPhoneNumber"] = "1111";

            AssertJsonErrors(ContactDetails(parameters),
                "FirstName", "The first name must be between 2 and 30 characters in length and not have any invalid characters.",
                "LastName", "The last name must be between 2 and 30 characters in length and not have any invalid characters.",
                "EmailAddress", "The email address must be valid and have less than 320 characters.",
                "SecondaryEmailAddress", "The secondary email address must be valid and have less than 320 characters.",
                "PhoneNumber", "The phone number must be between 8 and 20 characters in length and not have any invalid characters.",
                "SecondaryPhoneNumber", "The secondary phone number must be between 8 and 20 characters in length and not have any invalid characters.",
                "Location", "The location must be a valid postal location.");
        }

        private static NameValueCollection GetParameters(IMember member, ICandidate candidate, IResume resume)
        {
            var primaryEmailAddress = member.GetPrimaryEmailAddress();
            var secondaryEmailAddress = member.GetSecondaryEmailAddress();
            var primaryPhoneNumber = member.GetPrimaryPhoneNumber();
            var secondaryPhoneNumber = member.GetSecondaryPhoneNumber();

            return new NameValueCollection
            {
                {"FirstName", member.FirstName},
                {"LastName", member.LastName},
                {"CountryId", member.Address.Location.Country.Id.ToString(CultureInfo.InvariantCulture)},
                {"Location", member.Address.Location.ToString()},
                {"EmailAddress", primaryEmailAddress == null ? null : primaryEmailAddress.Address},
                {"SecondaryEmailAddress", secondaryEmailAddress == null ? null : secondaryEmailAddress.Address},
                {"PhoneNumber", primaryPhoneNumber.Number},
                {"PhoneNumberType", primaryPhoneNumber.Type.ToString()},
                {"SecondaryPhoneNumber", secondaryPhoneNumber == null ? null : secondaryPhoneNumber.Number},
                {"SecondaryPhoneNumberType", secondaryPhoneNumber == null ? null : secondaryPhoneNumber.Type.ToString()},
                {"Citizenship", resume == null ? null : resume.Citizenship},
                {"VisaStatus", candidate.VisaStatus == null ? null : candidate.VisaStatus.Value.ToString()},
                {"Aboriginal", member.EthnicStatus.IsFlagSet(EthnicStatus.Aboriginal) ? "true" : "false"},
                {"TorresIslander", member.EthnicStatus.IsFlagSet(EthnicStatus.TorresIslander) ? "true" : "false"},
                {"Gender", member.Gender == Gender.Male ? "Male" : member.Gender == Gender.Female ? "Female" : null},
                {"DateOfBirthMonth", member.DateOfBirth == null || member.DateOfBirth.Value.Month == null ? null : member.DateOfBirth.Value.Month.Value.ToString(CultureInfo.InvariantCulture)},
                {"DateOfBirthYear", member.DateOfBirth == null ? null : member.DateOfBirth.Value.Year.ToString(CultureInfo.InvariantCulture)},
            };
        }

        private JsonResponseModel ContactDetails(NameValueCollection parameters)
        {
            return Deserialize<JsonResponseModel>(Post(_contactDetailsUrl, parameters));
        }

        protected override JsonResponseModel Call()
        {
            return ContactDetails(new NameValueCollection());
        }
    }
}
