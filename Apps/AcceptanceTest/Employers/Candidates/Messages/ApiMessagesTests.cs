using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Messages
{
    [TestClass]
    public class ApiMessagesTests
        : ApiSendMessagesTests
    {
        private readonly IFilesQuery _filesQuery = Resolve<IFilesQuery>();

        [TestMethod]
        public void TestSimpleMessage()
        {
            TestSimpleMessages(false, CreateMember(0));
        }

        [TestMethod]
        public void TestSimpleMessageWithCopy()
        {
            TestSimpleMessages(true, CreateMember(0));
        }

        [TestMethod]
        public void TestSimpleMessages()
        {
            TestSimpleMessages(false, CreateMember(0), CreateMember(1));
        }

        [TestMethod]
        public void TestSimpleMessagesWithCopy()
        {
            TestSimpleMessages(true, CreateMember(0), CreateMember(1));
        }

        [TestMethod]
        public void TestHtmlSubjectMessage()
        {
            TestHtmlSubjectMessages(false, CreateMember(0));
        }

        [TestMethod]
        public void TestHtmlSubjectMessageWithCopy()
        {
            TestHtmlSubjectMessages(true, CreateMember(0));
        }

        [TestMethod]
        public void TestHtmlSubjectMessages()
        {
            TestHtmlSubjectMessages(false, CreateMember(0), CreateMember(1));
        }

        [TestMethod]
        public void TestHtmlSubjectMessagesWithCopy()
        {
            TestHtmlSubjectMessages(true, CreateMember(0), CreateMember(1));
        }

        [TestMethod]
        public void TestHtmlBodyMessage()
        {
            TestHtmlBodyMessages(false, CreateMember(0));
        }

        [TestMethod]
        public void TestHtmlBodyMessageWithCopy()
        {
            TestHtmlBodyMessages(true, CreateMember(0));
        }

        [TestMethod]
        public void TestHtmlBodyMessages()
        {
            TestHtmlBodyMessages(false, CreateMember(0), CreateMember(1));
        }

        [TestMethod]
        public void TestHtmlBodyMessagesWithCopy()
        {
            TestHtmlBodyMessages(true, CreateMember(0), CreateMember(1));
        }

        [TestMethod]
        public void TestSubstitutionMessage()
        {
            TestSubstitutionMessages(false, CreateMember(0, true));
        }

        [TestMethod]
        public void TestSubstitutionMessageWithCopy()
        {
            TestSubstitutionMessages(true, CreateMember(0, true));
        }

        [TestMethod]
        public void TestSubstitutionMessages()
        {
            TestSubstitutionMessages(false, CreateMember(0, true), CreateMember(1, true));
        }

        [TestMethod]
        public void TestSubstitutionMessagesWithCopy()
        {
            TestSubstitutionMessages(true, CreateMember(0, true), CreateMember(1, true));
        }

        [TestMethod]
        public void TestSubstitutionMessageInvisibleName()
        {
            TestSubstitutionMessages(false, CreateMember(0, false));
        }

        [TestMethod]
        public void TestSubstitutionMessageInvisibleNameWithCopy()
        {
            TestSubstitutionMessages(true, CreateMember(0, false));
        }

        [TestMethod]
        public void TestSubstitutionMessagesInvisibleName()
        {
            TestSubstitutionMessages(false, CreateMember(0, false), CreateMember(1, true));
        }

        [TestMethod]
        public void TestSubstitutionMessagesInvisibleNameWithCopy()
        {
            TestSubstitutionMessages(true, CreateMember(0, false), CreateMember(1, true));
        }

        [TestMethod]
        public void TestOverrideFromMessage()
        {
            TestOverrideFromMessages(false, CreateMember(0));
        }

        [TestMethod]
        public void TestOverrideFromMessageWithCopy()
        {
            TestOverrideFromMessages(true, CreateMember(0));
        }

        [TestMethod]
        public void TestOverrideFromMessages()
        {
            TestOverrideFromMessages(false, CreateMember(0), CreateMember(1));
        }

        [TestMethod]
        public void TestOverrideFromMessagesWithCopy()
        {
            TestOverrideFromMessages(true, CreateMember(0), CreateMember(1));
        }

        [TestMethod]
        public void TestMessageAttachment()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var fileModel = Attach(FileSystem.GetAbsolutePath(@"Test\Data\Attachments\TextAttachment.txt", RuntimeEnvironment.GetSourceFolder()));

            TestMessagesAttachments(employer, false, new[] { fileModel.Id }, CreateMember(0));
        }

        [TestMethod]
        public void TestMessageAttachmentWithCopy()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var fileModel = Attach(FileSystem.GetAbsolutePath(@"Test\Data\Attachments\TextAttachment.txt", RuntimeEnvironment.GetSourceFolder()));

            TestMessagesAttachments(employer, true, new[] { fileModel.Id }, CreateMember(0));
        }

        [TestMethod]
        public void TestMessagesAttachment()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var fileModel = Attach(FileSystem.GetAbsolutePath(@"Test\Data\Attachments\TextAttachment.txt", RuntimeEnvironment.GetSourceFolder()));

            TestMessagesAttachments(employer, false, new[] { fileModel.Id }, CreateMember(0), CreateMember(1));
        }

        [TestMethod]
        public void TestMessagesAttachmentWithCopy()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var fileModel = Attach(FileSystem.GetAbsolutePath(@"Test\Data\Attachments\TextAttachment.txt", RuntimeEnvironment.GetSourceFolder()));

            TestMessagesAttachments(employer, true, new[] { fileModel.Id }, CreateMember(0), CreateMember(1));
        }

        [TestMethod]
        public void TestMessageAttachments()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var fileModel1 = Attach(FileSystem.GetAbsolutePath(@"Test\Data\Attachments\TextAttachment.txt", RuntimeEnvironment.GetSourceFolder()));
            var fileModel2 = Attach(FileSystem.GetAbsolutePath(@"Test\Data\Attachments\WordAttachment.doc", RuntimeEnvironment.GetSourceFolder()));

            TestMessagesAttachments(employer, false, new[] { fileModel1.Id, fileModel2.Id }, CreateMember(0));
        }

        [TestMethod]
        public void TestMessageAttachmentsWithCopy()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var fileModel1 = Attach(FileSystem.GetAbsolutePath(@"Test\Data\Attachments\TextAttachment.txt", RuntimeEnvironment.GetSourceFolder()));
            var fileModel2 = Attach(FileSystem.GetAbsolutePath(@"Test\Data\Attachments\WordAttachment.doc", RuntimeEnvironment.GetSourceFolder()));

            TestMessagesAttachments(employer, true, new[] { fileModel1.Id, fileModel2.Id }, CreateMember(0));
        }

        [TestMethod]
        public void TestMessagesAttachments()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var fileModel1 = Attach(FileSystem.GetAbsolutePath(@"Test\Data\Attachments\TextAttachment.txt", RuntimeEnvironment.GetSourceFolder()));
            var fileModel2 = Attach(FileSystem.GetAbsolutePath(@"Test\Data\Attachments\WordAttachment.doc", RuntimeEnvironment.GetSourceFolder()));

            TestMessagesAttachments(employer, false, new[] { fileModel1.Id, fileModel2.Id }, CreateMember(0), CreateMember(1));
        }

        [TestMethod]
        public void TestMessagesAttachmentsWithCopy()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var fileModel1 = Attach(FileSystem.GetAbsolutePath(@"Test\Data\Attachments\TextAttachment.txt", RuntimeEnvironment.GetSourceFolder()));
            var fileModel2 = Attach(FileSystem.GetAbsolutePath(@"Test\Data\Attachments\WordAttachment.doc", RuntimeEnvironment.GetSourceFolder()));

            TestMessagesAttachments(employer, true, new[] { fileModel1.Id, fileModel2.Id }, CreateMember(0), CreateMember(1));
        }

        [TestMethod]
        public void TestNullSubject()
        {
            TestValidation(null, Body, null, "Subject", "The subject is required.");
        }

        [TestMethod]
        public void TestEmptySubject()
        {
            TestValidation(string.Empty, Body, null, "Subject", "The subject is required.");
        }

        [TestMethod]
        public void TestSubjectLength()
        {
            TestValidation(new string('a', 600), Body, null, "Subject", "The subject must be no more than 500 characters in length.");
        }

        [TestMethod]
        public void TestNullBody()
        {
            TestValidation(Subject, null, null, "Body", "The body is required.");
        }

        [TestMethod]
        public void TestEmptyBody()
        {
            TestValidation(Subject, string.Empty, null, "Body", "The body is required.");
        }

        [TestMethod]
        public void TestInvalidFrom()
        {
            TestValidation(Subject, Body, "invalid", "From", "The email address must be valid and have less than 320 characters.");
        }

        private void TestValidation(string subject, string body, string from, string expectedKey, string expectedMessage)
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var member = CreateMember(0);

            // Create messages.

            var model = SendMessages(subject, body, from, false, null, member);
            AssertJsonError(model, expectedKey, expectedMessage);
        }

        private void TestSimpleMessages(bool sendCopy, params Member[] members)
        {
            const string subject = "This is the subject";
            const string body = "This is the body";
            TestMessages(sendCopy, subject, body, null, null, members, subject, body);
        }

        private void TestHtmlSubjectMessages(bool sendCopy, params Member[] members)
        {
            const string subject = "This is <div>some html in</div>the subject";
            const string expectedSubject = "This is the subject";
            const string body = "This is the body";
            TestMessages(sendCopy, subject, body, null, null, members, expectedSubject, body);
        }

        private void TestSubstitutionMessages(bool sendCopy, params Member[] members)
        {
            const string subject = "This is the subject";
            const string body = "Hi, <img class='first-name' src='dfdsfsdf' />This is <div>some html</div> in <br />the <p><img class='first-name' src='dfdsfsdf' /> <img class='last-name' src='dfdsfsdf' /> body</p>";
            const string expectedSubject = "This is the subject";
            const string expectedBody = "Hi, <%= To.FirstName %>This is <div>some html</div> in <br />the <p><%= To.FirstName %> <%= To.LastName %> body</p>";
            TestMessages(sendCopy, subject, body, null, null, members, expectedSubject, expectedBody);
        }

        private void TestHtmlBodyMessages(bool sendCopy, params Member[] members)
        {
            const string subject = "This is the subject";
            const string body = "This is <div>some <a href=\"http://google.com\">html</a></div> in <br />the <p>body</p>";
            TestMessages(sendCopy, subject, body, null, null, members, subject, body);
        }

        private void TestOverrideFromMessages(bool sendCopy, params Member[] members)
        {
            const string subject = "This is the subject";
            const string body = "This is the body";
            TestMessages(sendCopy, subject, body, "override@test.linkme.net.au", null, members, subject, body);
        }

        private void TestMessagesAttachments(Employer employer, bool sendCopy, Guid[] attachmentIds, params Member[] members)
        {
            const string subject = "This is the subject";
            const string body = "This is the body";
            TestMessages(employer, sendCopy, subject, body, null, attachmentIds, members, subject, body);
        }

        private void TestMessages(bool sendCopy, string subject, string body, string from, Guid[] attachmentIds, Member[] members, string expectedSubject, string expectedBody)
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            TestMessages(employer, sendCopy, subject, body, from, attachmentIds, members, expectedSubject, expectedBody);
        }

        private void TestMessages(Employer employer, bool sendCopy, string subject, string body, string from, Guid[] attachmentIds, Member[] members, string expectedSubject, string expectedBody)
        {
            // Send a message.

            var model = SendMessages(subject, body, from, sendCopy, attachmentIds, members);
            AssertJsonSuccess(model);

            // Assert data.

            foreach (var member in members)
                AssertMessage(employer, member.Id, expectedSubject, expectedBody, from, sendCopy, attachmentIds);

            // Look up attachments.

            var attachments = GetAttachments(employer, attachmentIds);

            // Assert email.

            var length = sendCopy ? 2 * members.Length : members.Length;
            var emails = _emailServer.AssertEmailsSent(length);

            // Assuming the implementation means member and employer emails come in pairs ...

            var emailPairs = new List<Tuple<MockEmail, MockEmail>>();
            for (var index = 0; index < members.Length; ++index)
            {
                var memberEmail = sendCopy ? emails[2*index] : emails[index];
                var employerEmail = sendCopy ? emails[2*index + 1] : null;
                emailPairs.Add(new Tuple<MockEmail, MockEmail>(memberEmail, employerEmail));
            }

            foreach (var member in members)
            {
                var fullName = member.FullName;
                var pair = (from p in emailPairs where p.Item1.To[0].DisplayName == fullName select p).Single();

                // Names are always visible to the member.

                var substitutedBody = expectedBody.Replace("<%= To.FirstName %>", member.FirstName).Replace("<%= To.LastName %>", member.LastName);
                AssertEmail(pair.Item1, employer, member, expectedSubject, substitutedBody, from, attachments);

                if (sendCopy)
                {
                    // Show the name to the employer only if it is visible.

                    substitutedBody = member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Name)
                        ? expectedBody.Replace("<%= To.FirstName %>", member.FirstName).Replace("<%= To.LastName %>", member.LastName)
                        : expectedBody.Replace("<%= To.FirstName %>", "[Candidate first name]").Replace("<%= To.LastName %>", "[Candidate last name]");

                    AssertEmail(pair.Item2, employer, "Copy: " + expectedSubject, substitutedBody, from, attachments);
                }
            }
        }

        private MockEmailAttachment[] GetAttachments(IEmployer employer, ICollection<Guid> attachmentIds)
        {
            if (attachmentIds == null || attachmentIds.Count == 0)
                return null;

            var attachments = _employerMemberContactsQuery.GetMessageAttachments(employer, attachmentIds);
            var fileReferences = _filesQuery.GetFileReferences(from a in attachments select a.FileReferenceId, new Range());
            return (from f in fileReferences select new MockEmailAttachment { Name = f.FileName, MediaType = f.MediaType }).ToArray();
        }

        private Member CreateMember(int index, bool nameVisible)
        {
            var member = CreateMember(index);
            if (!nameVisible)
            {
                member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Name);
                _memberAccountsCommand.UpdateMember(member);
            }

            return member;
        }
    }
}
