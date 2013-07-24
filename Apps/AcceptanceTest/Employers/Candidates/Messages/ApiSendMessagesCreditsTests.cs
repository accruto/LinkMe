using System.Collections.Specialized;
using System.Linq;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Messages
{
    [TestClass]
    public class ApiSendMessagesCreditsTests
        : ApiActionCreditsTests
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

        protected override JsonResponseModel CallAction(Member[] members)
        {
            var parameters = new NameValueCollection();
            if (members != null)
            {
                foreach (var member in members)
                    parameters.Add("candidateId", member.Id.ToString());
            }
            parameters.Add("Subject", Subject);
            parameters.Add("Body", Body);

            return Deserialize<JsonResponseModel>(Post(_sendMessagesUrl, parameters));
        }

        protected override MemberAccessReason? AssertModel(JsonResponseModel model, Employer employer, Member[] members)
        {
            AssertJsonSuccess(model);

            var emails = _emailServer.AssertEmailsSent(members.Length);
            foreach (var member in members)
            {
                var fullName = member.FullName;
                var email = (from e in emails where e.To[0].DisplayName == fullName select e).Single();
                email.AssertAddresses(employer, Return, member);
                email.AssertSubject(Subject);
                email.AssertHtmlViewContains(Body);
            }

            return MemberAccessReason.MessageSent;
        }
    }
}