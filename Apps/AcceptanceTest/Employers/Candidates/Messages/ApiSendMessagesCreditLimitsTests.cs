using System.Collections.Specialized;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Messages
{
    [TestClass]
    public class ApiSendMessagesCreditLimitsTests
        : ApiActionCreditLimitsTests
    {
        private const string Subject = "This is the subject";
        private const string Body = "This is the body";

        private ReadOnlyUrl _sendMessagesUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _emailServer.ClearEmails();
            _sendMessagesUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/sendmessages");
        }

        protected override MemberAccessReason GetReason()
        {
            return MemberAccessReason.MessageSent;
        }

        protected override JsonResponseModel CallAction(Member member)
        {
            var parameters = new NameValueCollection
            {
                {"candidateId", member.Id.ToString()},
                {"Subject", Subject},
                {"Body", Body}
            };
            return Deserialize<JsonResponseModel>(Post(_sendMessagesUrl, parameters));
        }
    }
}