using LinkMe.Domain.Users.Employers.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Contacts
{
    [TestClass]
    public class MemberMessageSubstitionsTests
    {
        [TestMethod]
        public void TestSubstituteFirstName()
        {
            const string html = @"<div><img class=""first-name"" src=""anything"" /></div>";
            Assert.AreEqual(@"<div><%= To.FirstName %></div>", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestSubstituteLastName()
        {
            const string html = @"<div><img class=""last-name"" src=""anything"" /></div>";
            Assert.AreEqual(@"<div><%= To.LastName %></div>", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestSubstituteFullName()
        {
            const string html = @"<div><span class=""full-name"">Homer Simpson</span></div>";
            Assert.AreEqual(@"<div><%= To.FullName %></div>", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestNoSpanSubstitution()
        {
            const string html = @"<div><span class=""something"">Homer Simpson</span></div>";
            Assert.AreEqual(@"<div><span>Homer Simpson</span></div>", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestNoImgSubstitution()
        {
            const string html = @"<div><img class=""something"" src=""anything"" /></div>";
            Assert.AreEqual(@"<div></div>", new MemberMessageCleaner().CleanBody(html));
        }
    }
}
