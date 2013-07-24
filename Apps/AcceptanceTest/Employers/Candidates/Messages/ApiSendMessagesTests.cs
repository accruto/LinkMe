using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Contacts.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Employers.Models.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Messages
{
    [TestClass]
    public abstract class ApiSendMessagesTests
        : WebTestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected readonly IEmployerMemberContactsQuery _employerMemberContactsQuery = Resolve<IEmployerMemberContactsQuery>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();

        protected const string Subject = "This is the subject";
        protected const string Body = "This is the body";

        private ReadOnlyUrl _sendMessagesUrl;
        private ReadOnlyUrl _attachUrl;

        [TestInitialize]
        public void ApiSendMessagesTestsInitialize()
        {
            _emailServer.ClearEmails();
            _sendMessagesUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/sendmessages");
            _attachUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/attach");
        }

        protected Employer CreateEmployer(int index)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id });
            return employer;
        }

        protected Member CreateMember(int index)
        {
            return _memberAccountsCommand.CreateTestMember(index);
        }

        protected JsonFileModel Attach(string file)
        {
            var files = new NameValueCollection { { "file", file } };
            var response = Post(_attachUrl, null, files);
            return new JavaScriptSerializer().Deserialize<JsonFileModel>(response);
        }

        protected JsonResponseModel SendMessages(string subject, string body, string from, bool sendCopy, Guid[] attachmentIds, params Member[] members)
        {
            var variables = new NameValueCollection();
            if (members != null)
            {
                foreach (var member in members)
                    variables.Add("candidateId", member.Id.ToString());
            }

            if (attachmentIds != null)
            {
                foreach (var attachmentId in attachmentIds)
                    variables.Add("attachmentId", attachmentId.ToString());
            }

            variables.Add("subject", subject);
            variables.Add("body", body);
            variables.Add("from", from);
            variables.Add("sendCopy", sendCopy ? "true" : "false");

            return Deserialize<JsonResponseModel>(Post(_sendMessagesUrl, variables));
        }

        protected void AssertMessage(IEmployer employer, Guid memberId, string subject, string body, string from, bool sendCopy, Guid[] attachmentIds)
        {
            var messages = _employerMemberContactsQuery.GetMessages(employer, memberId);
            Assert.AreEqual(1, messages.Count);
            var message = messages[0];
            Assert.AreEqual(subject, message.Subject);
            Assert.AreEqual(body, message.Body);
            Assert.AreEqual(from, message.From);
            Assert.AreEqual(sendCopy, message.SendCopy);

            if (attachmentIds == null || attachmentIds.Length == 0)
            {
                Assert.IsTrue(message.AttachmentIds == null || message.AttachmentIds.Count == 0);
            }
            else
            {
                foreach (var attachmentId in attachmentIds)
                    Assert.IsTrue((from a in message.AttachmentIds where a == attachmentId select a).Any());
            }
        }

        protected void AssertEmail(MockEmail email, Employer employer, Member member, string subject, string body, string from, MockEmailAttachment[] attachments)
        {
            if (from == null)
                email.AssertAddresses(employer, Return, member);
            else
                email.AssertAddresses(new EmailRecipient(from, employer.FullName), Return, member);
            email.AssertSubject(subject);
            email.AssertHtmlViewContains(body);
            email.AssertHtmlViewDoesNotContain("<%= To.FirstName %>");
            email.AssertHtmlViewDoesNotContain("<%= To.LastName %>");
            
            if (attachments == null || attachments.Length == 0)
                email.AssertNoAttachments();
            else
                email.AssertAttachments(attachments);
        }

        protected void AssertEmail(MockEmail email, Employer employer, string subject, string body, string from, MockEmailAttachment[] attachments)
        {
            if (from == null)
                email.AssertAddresses(Return, Return, employer);
            else
                email.AssertAddresses(Return, Return, new EmailRecipient(from, employer.FullName));
            email.AssertSubject(subject);
            email.AssertHtmlViewContains(body);
            email.AssertHtmlViewDoesNotContain("<%= To.FirstName %>");
            email.AssertHtmlViewDoesNotContain("<%= To.LastName %>");

            if (attachments == null || attachments.Length == 0)
                email.AssertNoAttachments();
            else
                email.AssertAttachments(attachments);
        }
    }
}
