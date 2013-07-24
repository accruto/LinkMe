using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Api
{
    [TestClass]
    public class SendResumeTests
        : ApiTests
    {
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();

        private ReadOnlyUrl _sendResumeUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _emailServer.ClearEmails();
            _sendResumeUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/sendresume");
        }

        [TestMethod]
        public void TestSendResume()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = _candidateResumesCommand.AddTestResume(candidate);
            LogIn(member);

            AssertJsonSuccess(SendResume());
            AssertMember(member, candidate, resume, true);

            // Check that an email was sent (it would be nice to test the contents of the email as well ...).

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, member);
            email.AssertAttachment(member.FullName + ".doc", "application/msword");
        }

        private JsonResponseModel SendResume()
        {
            return Deserialize<JsonResponseModel>(Post(_sendResumeUrl));
        }

        protected override JsonResponseModel Call()
        {
            return SendResume();
        }
    }
}
