using LinkMe.Domain.Users.Employers.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Contacts
{
    [TestClass]
    public class MemberMessageCleanSubjectTests
    {
        [TestMethod]
        public void TestEmptyString()
        {
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanSubject(string.Empty));
        }
        
        [TestMethod]
        public void TestNoTags()
        {
            const string html = "This has no tags!";
            Assert.AreEqual(html, new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestOnlyATag()
        {
            const string html = "<div>";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestOnlyConsecutiveTags()
        {
            const string html = "<div><bar><baz />";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestTextBeforeTag()
        {
            const string html = "Hello<div>";
            Assert.AreEqual("Hello", new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestTextAfterTag()
        {
            const string html = "<div>World";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestTextBetweenTags()
        {
            const string html = "<p><div>World</div></p>";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestClosingTagInAttrValue()
        {
            const string html = "<div title=\"/>\" />";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestTagClosedByStartTag()
        {
            const string html = "<div <>Test";
            Assert.AreEqual("Test", new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestSingleQuotedAttrContainingDoubleQuotesAndEndTagChar()
        {
            const string html = @"<div ='test""/>title' />";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestDoubleQuotedAttributeContainingSingleQuotesAndEndTagChar()
        {
            const string html = @"<div =""test'/>title"" />";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestNonQuotedAttribute()
        {
            const string html = @"<div title=test>title />";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestNonQuotedAttributeContainingDoubleQuotes()
        {
            const string html = @"<p title = test-test""-test>title />Test</p>";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestNonQuotedAttributeContainingQuotedSection()
        {
            const string html = @"<p title = test-test""- >""test> ""title />Test</p>";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestTagClosingCharInAttributeValueWithNoNameFollowedByText()
        {
            const string html = @"<div = "" />title"" />Test";
            Assert.AreEqual(@"title"" />Test", new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestTextThatLooksLikeTag()
        {
            const string html = @"<çoo = "" />title"" />Test";
            Assert.AreEqual(@"title"" />Test", new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestCommentOnly()
        {
            const string html = "<!-- this go bye bye>";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestNonDashDashComment()
        {
            const string html = "<! this go bye bye>";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestTwoConsecutiveComments()
        {
            const string html = "<!-- this go bye bye><!-- another comment>";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestTextBeforeComment()
        {
            const string html = "Hello<!-- this go bye bye -->";
            Assert.AreEqual("Hello", new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestTextAfterComment()
        {
            const string html = "<!-- this go bye bye -->World";
            Assert.AreEqual("World", new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestAngleBracketsButNotHtml()
        {
            const string html = "<$)*(@&$(@*>";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestCommentInterleavedWithText()
        {
            const string html = "Hello <!-- this go bye bye --> World <!--> This is fun";
            Assert.AreEqual("Hello  World  This is fun", new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestCommentBetweenNonTagButLooksLikeTagDoes()
        {
            const string html = @"<ç123 title=""<!bc def>"">";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestTagClosedByStartComment()
        {
            const string html = "<div<!--div>Test";
            Assert.AreEqual(string.Empty, new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestTagClosedByProperComment()
        {
            const string html = "<div<!-- div -->Test";
            Assert.AreEqual("Test", new MemberMessageCleaner().CleanSubject(html));
        }

        [TestMethod]
        public void TestTagClosedByEmptyComment()
        {
            const string html = "<div<!>Test";
            Assert.AreEqual("Test", new MemberMessageCleaner().CleanSubject(html));
        }
    }
}
