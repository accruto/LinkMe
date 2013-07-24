using LinkMe.Domain.Users.Employers.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Contacts
{
    [TestClass]
    public class MemberMessageCleanBodyTests
    {
        [TestMethod]
        public void TestEmptyString()
        {
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanBody(string.Empty));
        }
        
        [TestMethod]
        public void TestNoTags()
        {
            const string html = "This has no tags!";
            Assert.AreEqual(html, new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestOnlyTag()
        {
            const string html = "<div>";
            Assert.AreEqual("<div></div>", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestBrTag()
        {
            const string html = "<br/>";
            Assert.AreEqual("<br />", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestPTag()
        {
            const string html = "<p></p>";
            Assert.AreEqual("<p />", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestATag()
        {
            const string html = "<a href=\"http://google.com\" target=\"_blank\">Click here</a>";
            Assert.AreEqual("<a href=\"http://google.com\">Click here</a>", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestOnlyConsecutiveTags()
        {
            const string html = "<div><bar><baz />";
            Assert.AreEqual("<div></div>", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestTextBeforeTag()
        {
            const string html = "Hello<div>";
            Assert.AreEqual("Hello<div></div>", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestTextAfterTag()
        {
            const string html = "<div>World";
            Assert.AreEqual("<div>World</div>", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestTextBetweenTags()
        {
            const string html = "<p><div>World</div></p>";
            Assert.AreEqual("<p><div>World</div></p>", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestClosingTagInAttrValue()
        {
            const string html = "<div title=\"/>\" />";
            Assert.AreEqual("<div></div>", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestTagClosedByStartTag()
        {
            const string html = "<div <>Test";
            Assert.AreEqual("<div></div>Test", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestSingleQuotedAttrContainingDoubleQuotesAndEndTagChar()
        {
            const string html = @"<div ='test""/>title' />";
            Assert.AreEqual("<div>title' /></div>", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestDoubleQuotedAttributeContainingSingleQuotesAndEndTagChar()
        {
            const string html = @"<div =""test'/>title"" />";
            Assert.AreEqual(@"<div>title"" /></div>", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestNonQuotedAttribute()
        {
            const string html = @"<div title=test>title />";
            Assert.AreEqual("<div>title /></div>", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestNonQuotedAttributeContainingDoubleQuotes()
        {
            const string html = @"<p title = test-test""-test>title />Test</p>";
            Assert.AreEqual("<p>title />Test</p>", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestNonQuotedAttributeContainingQuotedSection()
        {
            const string html = @"<p title = test-test""- >""test> ""title />Test</p>";
            Assert.AreEqual(@"<p>""test> ""title />Test</p>", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestTagClosingCharInAttributeValueWithNoNameFollowedByText()
        {
            const string html = @"<div = "" />title"" />Test";
            Assert.AreEqual(@"<div></div>title"" />Test", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestTextThatLooksLikeTag()
        {
            const string html = @"<çoo = "" />title"" />Test";
            Assert.AreEqual(@"title"" />Test", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestCommentOnly()
        {
            const string html = "<!-- this go bye bye>";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestNonDashDashComment()
        {
            const string html = "<! this go bye bye>";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestTwoConsecutiveComments()
        {
            const string html = "<!-- this go bye bye><!-- another comment>";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestTextBeforeComment()
        {
            const string html = "Hello<!-- this go bye bye -->";
            Assert.AreEqual("Hello", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestTextAfterComment()
        {
            const string html = "<!-- this go bye bye -->World";
            Assert.AreEqual("World", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestAngleBracketsButNotHtml()
        {
            const string html = "<$)*(@&$(@*>";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestCommentInterleavedWithText()
        {
            const string html = "Hello <!-- this go bye bye --> World <!--> This is fun";
            Assert.AreEqual("Hello  World  This is fun", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestCommentBetweenNonTagButLooksLikeTagDoes()
        {
            const string html = @"<ç123 title=""<!bc def>"">";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestTagClosedByStartComment()
        {
            const string html = "<div<!--div>Test";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestTagClosedByProperComment()
        {
            const string html = "<div<!-- div -->Test";
            Assert.AreEqual("Test", new MemberMessageCleaner().CleanBody(html));
        }

        [TestMethod]
        public void TestTagClosedByEmptyComment()
        {
            const string html = "<div<!>Test";
            Assert.AreEqual("Test", new MemberMessageCleaner().CleanBody(html));
        }
    }
}
