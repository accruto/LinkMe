using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join.Activate
{
    [TestClass]
    public class ActivateTests
        : JoinTests
    {
        [TestMethod]
        public void TestActivate()
        {
            var testResume = TestResume.Complete;
            var parsedResume = testResume.GetParsedResume();

            // Get to job details page by uploading resume.

            var member = CreateMember(parsedResume.FirstName, parsedResume.LastName, parsedResume.EmailAddresses[0].Address);
            UpdateMember(member, parsedResume.PhoneNumbers[0].Number, parsedResume.PhoneNumbers[0].Type, parsedResume.Address.Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            UploadResume(testResume);
            var instanceId = GetInstanceId();
            var resume = parsedResume.Resume;
            UpdateMember(member, Gender, parsedResume.DateOfBirth);

            SubmitPersonalDetails(instanceId, member, candidate, Password);
            SubmitJobDetails(instanceId, member, candidate, resume, true, null, true);

            // Assert.

            AssertUrl(GetActivateUrl(instanceId));
            AssertPageContains("Your profile has been created.");
            AssertPageContains("We've sent an email containing an activation link to:");
            AssertPageContains(member.EmailAddresses[0].Address);
        }
    }
}
