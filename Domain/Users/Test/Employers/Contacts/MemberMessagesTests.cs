using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Representatives.Commands;
using Linkme.Domain.Users.Employers.Contacts;
using LinkMe.Domain.Users.Employers.Contacts;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Contacts
{
    [TestClass]
    public class MemberMessagesTests
        : EmployerMemberContactsTests
    {
        private const string Subject = "This is the subject";
        private const string Body = "This is the body";

        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private readonly IEmployerContactsRepository _employerContactsRepository = Resolve<IEmployerContactsRepository>();
        private readonly IRepresentativesCommand _representativesCommand = Resolve<IRepresentativesCommand>();

        [TestMethod]
        public void TestMessageAccess()
        {
            var employer0 = CreateEmployer(0);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer0.Id });
            var employer1 = CreateEmployer(1);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer1.Id });

            var member = CreateMember(0);

            // Create messages.

            var templateMessage = new ContactMemberMessage { Subject = Subject, Body = Body};
            _employerMemberContactsCommand.ContactMember(_app, employer0, _employerMemberViewsQuery.GetProfessionalView(employer0, member), templateMessage);

            // Check.

            var messages = _employerMemberContactsQuery.GetMessages(employer0, member.Id);
            Assert.AreEqual(1, messages.Count);
            AssertMessage(templateMessage, messages[0], null, _employerContactsRepository.GetMemberMessageRepresentative(messages[0].Id));

            messages = _employerMemberContactsQuery.GetMessages(employer1, member.Id);
            Assert.AreEqual(0, messages.Count);
        }

        [TestMethod]
        public void TestContactNoAttachments()
        {
            var employer = CreateEmployer(0);
            var member = CreateMember(0);
            _allocationsCommand.CreateAllocation(new Allocation {CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id});

            // Check attachments.

            AssertAttachments(employer);

            // Create messages.

            var templateMessage = new RejectionMemberMessage {Subject = Subject, Body = Body};
            _employerMemberContactsCommand.RejectMember(employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), templateMessage);

            // Check.

            var messages = _employerMemberContactsQuery.GetMessages(employer, member.Id);
            Assert.AreEqual(1, messages.Count);
            AssertMessage(templateMessage, messages[0], null, _employerContactsRepository.GetMemberMessageRepresentative(messages[0].Id));
        }

        [TestMethod]
        public void TestContactCopy()
        {
            var employer = CreateEmployer(0);
            var member = CreateMember(0);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id });

            // Check attachments.

            AssertAttachments(employer);

            // Create messages.

            var templateMessage = new ContactMemberMessage { Subject = Subject, Body = Body, SendCopy = true};
            _employerMemberContactsCommand.ContactMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), templateMessage);

            // Check.

            var messages = _employerMemberContactsQuery.GetMessages(employer, member.Id);
            Assert.AreEqual(1, messages.Count);
            AssertMessage(templateMessage, messages[0], null, _employerContactsRepository.GetMemberMessageRepresentative(messages[0].Id));
        }

        [TestMethod]
        public void TestContactFrom()
        {
            var employer = CreateEmployer(0);
            var member = CreateMember(0);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id });

            // Check attachments.

            AssertAttachments(employer);

            // Create messages.

            var templateMessage = new RejectionMemberMessage { Subject = Subject, Body = Body, From = "from@test.linkme.net.au" };
            _employerMemberContactsCommand.RejectMember(employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), templateMessage);

            // Check.

            var messages = _employerMemberContactsQuery.GetMessages(employer, member.Id);
            Assert.AreEqual(1, messages.Count);
            AssertMessage(templateMessage, messages[0], null, _employerContactsRepository.GetMemberMessageRepresentative(messages[0].Id));
        }

        [TestMethod]
        public void TestContactOneAttachment()
        {
            var employer = CreateEmployer(0);
            var member = CreateMember(0);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id });

            // Save attachment.

            var attachment = CreateMessageAttachment(employer, 0);

            // Check attachments.

            AssertAttachments(employer, attachment);

            // Create messages.

            var templateMessage = new ContactMemberMessage { Subject = Subject, Body = Body, AttachmentIds = new List<Guid> { attachment.Id } };
            _employerMemberContactsCommand.ContactMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), templateMessage);

            // Check.

            var messages = _employerMemberContactsQuery.GetMessages(employer, member.Id);
            Assert.AreEqual(1, messages.Count);
            AssertMessage(templateMessage, messages[0], null, _employerContactsRepository.GetMemberMessageRepresentative(messages[0].Id));
        }

        [TestMethod]
        public void TestContactMultipleAttachments()
        {
            var employer = CreateEmployer(0);
            var member = CreateMember(0);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id });

            // Save attachments.

            const int count = 3;
            var attachments = new MemberMessageAttachment[count];
            for (var index = 0; index < count; ++index)
            {
                attachments[index] = CreateMessageAttachment(employer, index);
            }

            // Check attachments.

            AssertAttachments(employer, attachments);

            // Create messages.

            var templateMessage = new ContactMemberMessage { Subject = Subject, Body = Body, AttachmentIds = (from a in attachments select a.Id).ToList() };
            _employerMemberContactsCommand.ContactMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), templateMessage);

            // Check.

            var messages = _employerMemberContactsQuery.GetMessages(employer, member.Id);
            Assert.AreEqual(1, messages.Count);
            AssertMessage(templateMessage, messages[0], null, _employerContactsRepository.GetMemberMessageRepresentative(messages[0].Id));
        }

        [TestMethod]
        public void TestMultipleContactsMultipleAttachments()
        {
            var employer = CreateEmployer(0);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id });

            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            // Save attachments.

            var attachments = new MemberMessageAttachment[count];
            for (var index = 0; index < count; ++index)
            {
                attachments[index] = CreateMessageAttachment(employer, index);
            }

            // Check attachments.

            AssertAttachments(employer, attachments);

            // Create messages.

            var templateMessage = new ContactMemberMessage { Subject = Subject, Body = Body, AttachmentIds = (from a in attachments select a.Id).ToList() };
            _employerMemberContactsCommand.ContactMembers(_app, employer, _employerMemberViewsQuery.GetProfessionalViews(employer, members), templateMessage);

            // Check.

            for (var index = 0; index < count; ++index)
            {
                var messages = _employerMemberContactsQuery.GetMessages(employer, members[index].Id);
                Assert.AreEqual(1, messages.Count);
                AssertMessage(templateMessage, messages[0], null, _employerContactsRepository.GetMemberMessageRepresentative(messages[0].Id));
            }
        }

        [TestMethod]
        public void TestNullSubject()
        {
            TestValidation(new ContactMemberMessage {Subject = null, Body = Body}, "Subject", typeof(RequiredValidationError));
        }

        [TestMethod]
        public void TestEmptySubject()
        {
            TestValidation(new ContactMemberMessage { Subject = string.Empty, Body = Body }, "Subject", typeof(RequiredValidationError));
        }

        [TestMethod]
        public void TestSubjectLength()
        {
            TestValidation(new ContactMemberMessage { Subject = new string('a', 600), Body = Body }, "Subject", typeof(MaximumLengthValidationError));
        }

        [TestMethod]
        public void TestNullBody()
        {
            TestValidation(new ContactMemberMessage { Subject = Subject, Body = null }, "Body", typeof(RequiredValidationError));
        }

        [TestMethod]
        public void TestEmptyBody()
        {
            TestValidation(new ContactMemberMessage { Subject = Subject, Body = string.Empty }, "Body", typeof(RequiredValidationError));
        }

        [TestMethod]
        public void TestInvalidFrom()
        {
            TestValidation(new ContactMemberMessage { Subject = Subject, Body = Body, From = "invalid" }, "From", typeof(EmailAddressValidationError));
        }

        [TestMethod]
        public void TestRepresentative()
        {
            var employer = CreateEmployer(0);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id });

            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            _representativesCommand.CreateRepresentative(member1.Id, member2.Id);

            // Create messages.

            var templateMessage = new ContactMemberMessage { Subject = Subject, Body = Body };
            _employerMemberContactsCommand.ContactMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member1), templateMessage);

            // Check.

            var messages = _employerMemberContactsQuery.GetMessages(employer, member1.Id);
            Assert.AreEqual(1, messages.Count);
            AssertMessage(templateMessage, messages[0], member2.Id, _employerContactsRepository.GetMemberMessageRepresentative(messages[0].Id));
        }

        [TestMethod]
        public void TestRepresentatives()
        {
            var employer = CreateEmployer(0);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id });

            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            var member3 = CreateMember(3);
            var member4 = CreateMember(4);
            var member5 = CreateMember(5);
            _representativesCommand.CreateRepresentative(member1.Id, member2.Id);
            _representativesCommand.CreateRepresentative(member3.Id, member4.Id);

            // Create messages.

            var templateMessage = new ContactMemberMessage { Subject = Subject, Body = Body };
            _employerMemberContactsCommand.ContactMembers(_app, employer, _employerMemberViewsQuery.GetProfessionalViews(employer, new[] { member1, member3, member5 }), templateMessage);

            // Check.

            var messages = _employerMemberContactsQuery.GetMessages(employer, member1.Id);
            Assert.AreEqual(1, messages.Count);
            AssertMessage(templateMessage, messages[0], member2.Id, _employerContactsRepository.GetMemberMessageRepresentative(messages[0].Id));

            messages = _employerMemberContactsQuery.GetMessages(employer, member3.Id);
            Assert.AreEqual(1, messages.Count);
            AssertMessage(templateMessage, messages[0], member4.Id, _employerContactsRepository.GetMemberMessageRepresentative(messages[0].Id));

            messages = _employerMemberContactsQuery.GetMessages(employer, member5.Id);
            Assert.AreEqual(1, messages.Count);
            AssertMessage(templateMessage, messages[0], null, _employerContactsRepository.GetMemberMessageRepresentative(messages[0].Id));
        }

        private void TestValidation(ContactMemberMessage message, string expectedName, Type expectedErrorType)
        {
            var employer = CreateEmployer(0);
            var member = CreateMember(0);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id });

            // Create messages.

            try
            {
                _employerMemberContactsCommand.ContactMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), message);
                Assert.Fail();
            }
            catch (ValidationErrorsException ex)
            {
                Assert.AreEqual(1, ex.Errors.Count);
                Assert.AreEqual(expectedName, ex.Errors[0].Name);
                Assert.IsInstanceOfType(ex.Errors[0], expectedErrorType);
            }
        }

        private static void AssertMessage(MemberMessage expectedMessage, MemberMessage message, Guid? expectedRepresentativeId, Guid? representativeId)
        {
            Assert.AreEqual(expectedMessage.Subject, message.Subject);
            Assert.AreEqual(expectedMessage.Body, message.Body);
            Assert.AreEqual(expectedMessage.From, message.From);
            Assert.AreEqual(expectedMessage.SendCopy, message.SendCopy);
            Assert.AreEqual(expectedMessage.GetType(), message.GetType());

            if (expectedMessage.AttachmentIds.IsNullOrEmpty())
            {
                Assert.IsTrue(message.AttachmentIds.IsNullOrEmpty());
            }
            else
            {
                Assert.AreEqual(expectedMessage.AttachmentIds.Count, message.AttachmentIds.Count);
                foreach (var attachmentId in expectedMessage.AttachmentIds)
                    Assert.IsTrue(message.AttachmentIds.Contains(attachmentId));
            }

            Assert.AreEqual(expectedRepresentativeId, representativeId);
        }
    }
}
