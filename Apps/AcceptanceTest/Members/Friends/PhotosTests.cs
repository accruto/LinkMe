using System;
using System.Net;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Friends
{
    [TestClass]
    public class PhotosTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();

        [TestMethod]
        public void TestDownloadPhoto()
        {
            var member1 = CreateMember(1);
            _memberAccountsCommand.AddTestProfilePhoto(member1);

            var member2 = CreateMember(2);
            LogIn(member2);

            Get(GetPhotoUrl(member1.Id));

            // Can see the photo.

            Assert.AreEqual(HttpStatusCode.OK, Browser.CurrentStatusCode);
            Assert.AreEqual("image/jpeg", Browser.ResponseHeaders["Content-Type"]);
        }

        [TestMethod, ExpectedException(typeof(NotFoundException))]
        public void TestDownloadNoPhoto()
        {
            var member1 = CreateMember(1);

            var member2 = CreateMember(2);
            LogIn(member2);

            Get(GetPhotoUrl(member1.Id));
        }

        [TestMethod, ExpectedException(typeof(NotFoundException))]
        public void TestDownloadUnknownMember()
        {
            var member = CreateMember(0);
            LogIn(member);

            Get(GetPhotoUrl(Guid.NewGuid()));
        }

        private static ReadOnlyUrl GetPhotoUrl(Guid friendId)
        {
            return new ReadOnlyApplicationUrl(true, "~/members/friends/" + friendId + "/photo");
        }

        private Member CreateMember(int index)
        {
            return _memberAccountsCommand.CreateTestMember(index);
        }
    }
}