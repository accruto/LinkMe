using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Members.Models.Profiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Photos
{
    [TestClass]
    public class PhotosTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly IFilesQuery _filesQuery = Resolve<IFilesQuery>();

        private ReadOnlyUrl _photoUrl;
        private ReadOnlyUrl _uploadPhotoUrl;
        private ReadOnlyUrl _removePhotoUrl;
        private ReadOnlyUrl _loginUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _photoUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/photo");
            _uploadPhotoUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/uploadphoto");
            _removePhotoUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/removephoto");
            _loginUrl = new ReadOnlyApplicationUrl(true, "~/login");
        }

        [TestMethod]
        public void TestAuthorisation()
        {
            // Download.

            Get(_photoUrl);
            var loginUrl = _loginUrl.AsNonReadOnly();
            loginUrl.QueryString.Add("returnUrl", _photoUrl.Path);
            AssertUrl(loginUrl);

            // Upload.

            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Photos\ProfilePhoto.jpg", RuntimeEnvironment.GetSourceFolder());
            AssertJsonError(UploadPhoto(filePath), null, "100", "The user is not logged in.");

            // Remove.

            AssertJsonError(RemovePhoto(), null, "100", "The user is not logged in.");
        }

        [TestMethod]
        public void TestDownloadPhoto()
        {
            var member = CreateMember(0);
            _memberAccountsCommand.AddTestProfilePhoto(member);
            LogIn(member);

            Get(_photoUrl);

            // Can see the photo.

            Assert.AreEqual(HttpStatusCode.OK, Browser.CurrentStatusCode);
            Assert.AreEqual("image/jpeg", Browser.ResponseHeaders["Content-Type"]);
        }

        [TestMethod, ExpectedException(typeof(NotFoundException))]
        public void TestDownloadNoPhoto()
        {
            var member = CreateMember(0);
            LogIn(member);

            Get(_photoUrl);
        }

        [TestMethod]
        public void TestRemovePhoto()
        {
            var member = CreateMember(0);
            _memberAccountsCommand.AddTestProfilePhoto(member);
            Assert.IsNotNull(member.PhotoId);

            LogIn(member);
            Get(_photoUrl);
            Assert.AreEqual("image/jpeg", Browser.ResponseHeaders["Content-Type"]);

            // Remove it.

            AssertJsonSuccess(RemovePhoto());
            Get(HttpStatusCode.NotFound, _photoUrl);

            member = _membersQuery.GetMember(member.Id);
            Assert.IsNull(member.PhotoId);

            // Removing again does nothing.

            AssertJsonSuccess(RemovePhoto());
        }

        [TestMethod]
        public void TestUploadPhoto()
        {
            var member = CreateMember(0);
            LogIn(member);

            Get(HttpStatusCode.NotFound, _photoUrl);

            // Upload.

            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Photos\ProfilePhoto.jpg", RuntimeEnvironment.GetSourceFolder());
            var model = UploadPhoto(filePath);
            AssertJsonSuccess(model);
            Assert.IsNotNull(model.PhotoId);

            // Get the new photo.

            Get(_photoUrl);
            Assert.AreEqual("image/jpeg", Browser.ResponseHeaders["Content-Type"]);

            // Check the member.

            member = _membersQuery.GetMember(member.Id);
            Assert.IsNotNull(member.PhotoId);

            // Check the file.

            var fileReference = _filesQuery.GetFileReference(member.PhotoId.Value);
            Assert.AreEqual(Path.GetFileName(filePath), fileReference.FileName);
            Assert.AreEqual("image/jpeg", fileReference.MediaType);
            Assert.AreEqual(".jpg", fileReference.FileData.FileExtension);
            Assert.AreEqual(FileType.ProfilePhoto, fileReference.FileData.FileType);
        }

        [TestMethod]
        public void TestUploadInvalidPhoto()
        {
            var member = CreateMember(0);
            LogIn(member);

            Get(HttpStatusCode.NotFound, _photoUrl);

            // Upload.

            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Resumes\ProfessionalResume.doc", RuntimeEnvironment.GetSourceFolder());
            var model = UploadPhoto(filePath);
            AssertJsonError(model, null, "The extension 'doc' is not supported.");
        }

        private Member CreateMember(int index)
        {
            return _memberAccountsCommand.CreateTestMember(index);
        }

        private JsonResponseModel RemovePhoto()
        {
            return Deserialize<JsonResponseModel>(Post(_removePhotoUrl));
        }

        private JsonProfilePhotoModel UploadPhoto(string file)
        {
            var files = new NameValueCollection { { "file", file } };
            var response = Post(_uploadPhotoUrl, null, files);
            return new JavaScriptSerializer().Deserialize<JsonProfilePhotoModel>(response);
        }
    }
}