using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.PersonalDetails
{
    [TestClass]
    public class ErrorTests
        : JoinTests
    {
        [TestMethod]
        public void TestAllRequiredErrors()
        {
            // Get to the personal details page.

            Get(GetJoinUrl());
            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();

            // Test.

            var member = CreateMember("", "", "");
            UpdateMember(member, "", PhoneNumberType.Mobile, "");
            var candidate = CreateCandidate();
            candidate.Status = CandidateStatus.Unspecified;

            SubmitPersonalDetails(instanceId, member, candidate);
            AssertUrl(GetPersonalDetailsUrl(instanceId));

            AssertErrorMessages(
                "Please provide a password.",
                "Please provide a confirm password.",
                "Please accept the terms and conditions.",
                "Please provide a first name.",
                "Please provide a last name.",
                "Please provide a primary email address.",
                "Please provide a location.",
                "Please provide a phone number.",
                "Please indicate your availability.",
                "Please specify a minimum expected salary.");
        }

        [TestMethod]
        public void TestInvalidLocationErrors()
        {
            // Get to the personal details page.

            Get(GetJoinUrl());
            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();

            // Test.

            var member = CreateMember("", "", "");
            UpdateMember(member, "", PhoneNumberType.Mobile, "London");
            var candidate = CreateCandidate();
            candidate.Status = CandidateStatus.Unspecified;

            SubmitPersonalDetails(instanceId, member, candidate);
            AssertUrl(GetPersonalDetailsUrl(instanceId));

            // Location supplied but should get an invalid location error now.

            AssertErrorMessages(
                "Please provide a password.",
                "Please provide a confirm password.",
                "Please accept the terms and conditions.",
                "Please provide a first name.",
                "Please provide a last name.",
                "Please provide a primary email address.",
                "The location must be a valid postal location.",
                "Please provide a phone number.",
                "Please indicate your availability.",
                "Please specify a minimum expected salary.");
        }
    }
}
