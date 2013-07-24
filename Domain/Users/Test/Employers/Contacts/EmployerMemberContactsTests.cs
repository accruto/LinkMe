using System;
using System.IO;
using System.Linq;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Contacts;
using LinkMe.Domain.Users.Employers.Contacts.Commands;
using LinkMe.Domain.Users.Employers.Contacts.Queries;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Contacts
{
    [TestClass]
    public abstract class EmployerMemberContactsTests
        : TestClass
    {
        protected readonly IEmployerMemberContactsCommand _employerMemberContactsCommand = Resolve<IEmployerMemberContactsCommand>();
        protected readonly IEmployerMemberContactsQuery _employerMemberContactsQuery = Resolve<IEmployerMemberContactsQuery>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();

        protected readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        [TestInitialize]
        public void EmployerMemberContactsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected Member CreateMember(int index)
        {
            return _membersCommand.CreateTestMember(index);
        }

        protected IEmployer CreateEmployer(int index)
        {
            return _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
        }

        protected void AssertAttachments(IEmployer employer, params MemberMessageAttachment[] expectedAttachments)
        {
            var attachments = _employerMemberContactsQuery.GetMessageAttachments(employer, from a in expectedAttachments select a.Id);
            Assert.AreEqual(expectedAttachments.Length, attachments.Count);
            foreach (var expectedAttachment in expectedAttachments)
            {
                var attachmentId = expectedAttachment.Id;
                AssertAttachment(expectedAttachment, (from a in attachments where a.Id == attachmentId select a).Single());
                AssertAttachment(expectedAttachment, _employerMemberContactsQuery.GetMessageAttachment(employer, attachmentId));
            }
        }

        private static void AssertAttachment(MemberMessageAttachment expectedAttachment, MemberMessageAttachment attachment)
        {
            Assert.AreEqual(expectedAttachment.Id, attachment.Id);
            Assert.AreEqual(expectedAttachment.FileReferenceId, attachment.FileReferenceId);
        }

        protected MemberMessageAttachment CreateMessageAttachment(IEmployer employer, int index)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("This is the contents of the file.");
                    stream.Position = 0;
                    return _employerMemberContactsCommand.CreateMessageAttachment(employer, new Guid[0], new StreamFileContents(stream), string.Format("FileName{0}.txt", index));
                }
            }
        }
    }
}
