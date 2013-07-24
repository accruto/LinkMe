using System;
using System.Collections.Specialized;
using System.Linq;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Users.Employers.Contacts;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Employers.Models.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Messages
{
    [TestClass]
    public class ApiAttachmentsTests
        : ApiSendMessagesTests
    {
        private readonly IFilesQuery _filesQuery = Resolve<IFilesQuery>();

        private ReadOnlyUrl _detachUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _detachUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/detach");
        }

        [TestMethod]
        public void TestAttachment()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            
            var model = Attach(FileSystem.GetAbsolutePath(@"Test\Data\Attachments\TextAttachment.txt", RuntimeEnvironment.GetSourceFolder()));
            AssertModel("TextAttachment.txt", model);

            AssertAttachments(employer, model);
        }

        [TestMethod]
        public void TestMultipleAttachments()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var model1 = Attach(FileSystem.GetAbsolutePath(@"Test\Data\Attachments\TextAttachment.txt", RuntimeEnvironment.GetSourceFolder()));
            AssertModel("TextAttachment.txt", model1);

            var model2 = Attach(FileSystem.GetAbsolutePath(@"Test\Data\Attachments\WordAttachment.doc", RuntimeEnvironment.GetSourceFolder()));
            AssertModel("WordAttachment.doc", model2);

            AssertAttachments(employer, model1, model2);
        }

        [TestMethod]
        public void TestDetach()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var model1 = Attach(FileSystem.GetAbsolutePath(@"Test\Data\Attachments\TextAttachment.txt", RuntimeEnvironment.GetSourceFolder()));
            var model2 = Attach(FileSystem.GetAbsolutePath(@"Test\Data\Attachments\WordAttachment.doc", RuntimeEnvironment.GetSourceFolder()));

            AssertAttachments(employer, model1, model2);

            // Detach the first.

            var model = Detach(model1.Id);
            AssertJsonSuccess(model);

            Assert.IsNull(_employerMemberContactsQuery.GetMessageAttachment(employer, model1.Id));
            AssertAttachments(employer, model2);
        }

        private JsonResponseModel Detach(Guid attachmentId)
        {
            var parameters = new NameValueCollection { { "attachmentId", attachmentId.ToString() } };
            return Deserialize<JsonResponseModel>(Post(_detachUrl, parameters));
        }

        private void AssertAttachments(IEmployer employer, params JsonFileModel[] models)
        {
            foreach (var model in models)
            {
                var attachment = _employerMemberContactsQuery.GetMessageAttachment(employer, model.Id);
                Assert.IsNotNull(attachment);
                AssertAttachment(model, attachment);
            }

            var attachments = _employerMemberContactsQuery.GetMessageAttachments(employer, from m in models select m.Id);
            Assert.AreEqual(models.Length, attachments.Count);
            foreach (var model in models)
            {
                var attachmentId = model.Id;
                AssertAttachment(model, (from a in attachments where a.Id == attachmentId select a).Single());
            }
        }

        private void AssertAttachment(JsonFileModel model, MemberMessageAttachment attachment)
        {
            Assert.AreEqual(model.Id, attachment.Id);
            var fileReference = _filesQuery.GetFileReference(attachment.FileReferenceId);
            Assert.IsNotNull(fileReference);
            Assert.AreEqual(model.Name, fileReference.FileName);
            Assert.AreEqual(model.Size, fileReference.FileData.ContentLength);
        }

        private static void AssertModel(string expectedFileName, JsonFileModel model)
        {
            AssertJsonSuccess(model);
            Assert.AreEqual(expectedFileName, model.Name);
        }
    }
}
