using System.Collections.Specialized;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Messages
{
    [TestClass]
    public class ApiCheckCanSendMessagesCreditsTests
        : ApiActionCreditsTests
    {
        private ReadOnlyUrl _checkCanSendMessagesUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _emailServer.ClearEmails();
            _checkCanSendMessagesUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/checkcansendmessages");
        }

        protected override JsonResponseModel CallAction(Member[] members)
        {
            var parameters = new NameValueCollection();
            if (members != null)
            {
                foreach (var member in members)
                    parameters.Add("candidateId", member.Id.ToString());
            }

            return Deserialize<JsonResponseModel>(Post(_checkCanSendMessagesUrl, parameters));
        }

        protected override MemberAccessReason? AssertModel(JsonResponseModel model, Employer employer, Member[] members)
        {
            AssertJsonSuccess(model);

            // Just a check so not actual credits should be used and no email sent.

            _emailServer.AssertNoEmailSent();
            return null;
        }
    }
}