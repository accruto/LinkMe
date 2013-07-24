using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Utility.Test.Utilities
{
	[TestClass]
	public class HtmlUtilTest
	{
	    [TestMethod]
        public void TestContainsHtml()
        {
            Assert.IsFalse(HtmlUtil.ContainsHtml(null));
            Assert.IsFalse(HtmlUtil.ContainsHtml(""));
            Assert.IsFalse(HtmlUtil.ContainsHtml("whatever"));
            Assert.IsTrue(HtmlUtil.ContainsHtml("<html><body>text</body></html>"));
            Assert.IsTrue(HtmlUtil.ContainsHtml("<something"));

            // Case 1718 - &# shouldn't be detected as "script" on its own, but encoded HTML still should be.
            Assert.IsFalse(HtmlUtil.ContainsHtml("and some &#12 escaping"));
            Assert.IsTrue(HtmlUtil.ContainsHtml("and some &lt;b&gt; escaping"));
            Assert.IsTrue(HtmlUtil.ContainsHtml("and some &#x3c;b&#x3e; escaping"));
        }

        [TestMethod]
		public void TestContainsScript()
		{
			Assert.IsFalse(HtmlUtil.ContainsScript("whatever"));
            Assert.IsFalse(HtmlUtil.ContainsScript("<html><body>text</body></html>"));
			Assert.IsTrue(HtmlUtil.ContainsScript("<html><body><script>text</script></body></html>"));
			Assert.IsTrue(HtmlUtil.ContainsScript("<html><body><script >text</script></body></html>"));
			Assert.IsTrue(HtmlUtil.ContainsScript("<html><body><script>text</script </body></html>"));
			Assert.IsTrue(HtmlUtil.ContainsScript("<html><body><script >text</body></html>"));

            // Case 1521 - Microsoft .NET request filtering bypass vulnerability - LinkMe version

            TestXss("</XSS/*-*/STYLE=xss:e/**/xpression(alert(document.cookie))>");
            TestXss("<XSS STYLE=xss:e/**/xpression(alert(document.cookie))>");
            TestXss("<//*-*/XSSSTYLE=xss:e/**/xpression(alert(document.cookie))>");
            TestXss("</*-*/X/*-*/S/*-*/S ST/*-*/YLE=xss:e/**/xpression(alert(document.cookie))>");
        }

	    [TestMethod]
		public void TestCloseHtmlTags()
		{
			// No HTML.

			Assert.AreEqual(null, HtmlUtil.CloseHtmlTags(null));
			Assert.AreEqual("", HtmlUtil.CloseHtmlTags(""));
			Assert.AreEqual("no html", HtmlUtil.CloseHtmlTags("no html"));

			// Simple HTML.

			Assert.AreEqual("<html><valid>all closed</valid></html>",
				HtmlUtil.CloseHtmlTags("<html><valid>all closed</valid></html>"));
			Assert.AreEqual("<html><valid>all closed</valid></html",
				HtmlUtil.CloseHtmlTags("<html><valid>all closed</valid></html"));
			Assert.AreEqual("<html><center>some text</center></html>",
				HtmlUtil.CloseHtmlTags("<html><center>some text"));
			Assert.AreEqual("<html><center>some text</center></html>",
				HtmlUtil.CloseHtmlTags("<html><center>some text</center>"));
			Assert.AreEqual("<html><center>some text<center /><br></center></html>",
				HtmlUtil.CloseHtmlTags("<html><center>some text<center /><br></center>"));

			// Quotes.

			Assert.AreEqual("<html><center>some text<a name='blah'  /><br></center></html>",
				HtmlUtil.CloseHtmlTags("<html><center>some text<a name='blah'  /><br></center>"));
			Assert.AreEqual("some text <and>Some</AND> <html>text<SPAN class='blah'>more text</SPAN></html>",
				HtmlUtil.CloseHtmlTags("some text <and>Some</AND> <html>text<SPAN class='blah'>more text"));
			Assert.AreEqual("<html><a href=\"http://test/url\">link</a></html>",
				HtmlUtil.CloseHtmlTags("<html><a href=\"http://test/url\">link"));
			Assert.AreEqual("<html><a href='http://test\"url\'>link</a></html>",
				HtmlUtil.CloseHtmlTags("<html><a href='http://test\"url\'>link"));

			// Tags that don't need to be closed.

			Assert.AreEqual("<ul><li>one<li>two<li>three</ul>",
				HtmlUtil.CloseHtmlTags("<ul><li>one<li>two<li>three"));
			Assert.AreEqual("<hr><span>some text</span>",
				HtmlUtil.CloseHtmlTags("<hr><span>some text"));
		}

		[TestMethod]
		public void TestStripHtmlTags()
		{
            // Nothing to strip.

			Assert.IsNull(HtmlUtil.StripHtmlTags(null));
			Assert.AreEqual("", HtmlUtil.StripHtmlTags(""));
			Assert.AreEqual("no HTML", HtmlUtil.StripHtmlTags("no HTML"));

			// Strip to nothing.

			Assert.AreEqual("", HtmlUtil.StripHtmlTags("<html>"));
			Assert.AreEqual("", HtmlUtil.StripHtmlTags("</html>"));
			Assert.AreEqual("", HtmlUtil.StripHtmlTags("<html></html>"));
			Assert.AreEqual("", HtmlUtil.StripHtmlTags("<br />"));

			// Strip HTML tags and leave some text.

			Assert.AreEqual("plain text", HtmlUtil.StripHtmlTags("plain<br />text"));
			Assert.AreEqual("plain text", HtmlUtil.StripHtmlTags("<html>plain text</html>"));
			Assert.AreEqual("plain text", HtmlUtil.StripHtmlTags("plain<html> text</html>"));
			Assert.AreEqual("plain text", HtmlUtil.StripHtmlTags("plain <html>text</html>"));
			Assert.AreEqual("plain text", HtmlUtil.StripHtmlTags("plain<html>text</html>"));
			Assert.AreEqual("plain text and more", HtmlUtil.StripHtmlTags("plain<html>text</html> and<ul>more"));
            Assert.AreEqual("office tags with ns", HtmlUtil.StripHtmlTags("office<o:p>tags</o:p> with<st1:state w:st=\"on\">ns"));

			// Strip some HTML tags, ignore others.

			Assert.AreEqual("plain text and<ul>more", HtmlUtil.StripHtmlTags(
				"plain<html>text</html> and<ul>more", "ul"));
			Assert.AreEqual("plain<br />text", HtmlUtil.StripHtmlTags("plain<br />text", "BR"));
			Assert.AreEqual("plain</BR>", HtmlUtil.StripHtmlTags("plain</BR>", "BR"));
			Assert.AreEqual("plain<br />text<br>and</br> more", HtmlUtil.StripHtmlTags(
				"<html>plain<br />text<br>and</br></html>more", "BR"));

			// Unclosed tags

			Assert.AreEqual("Test one", HtmlUtil.StripHtmlTags("Test one<<ul>"));
			Assert.AreEqual("Test one <ul>", HtmlUtil.StripHtmlTags("Test one<<ul>", "ul"));
			Assert.AreEqual("Test one", HtmlUtil.StripHtmlTags("Test one<</ul>"));
			Assert.AreEqual("Test one", HtmlUtil.StripHtmlTags("Test one<li</ul>"));
			Assert.AreEqual("One > Two", HtmlUtil.StripHtmlTags("<li>One<li>> Two</ul>"));
			Assert.AreEqual("One /> Two", HtmlUtil.StripHtmlTags("<li>One<li>/> Two</ul>"));

            // Various ways to close the tag.

            Assert.AreEqual("something", HtmlUtil.StripHtmlTags("<tag> something"));
            Assert.AreEqual("something", HtmlUtil.StripHtmlTags("something <tag>"));
            Assert.AreEqual("", HtmlUtil.StripHtmlTags("<br/>"));
            Assert.AreEqual("", HtmlUtil.StripHtmlTags("<font size=5/>"));
            Assert.AreEqual("", HtmlUtil.StripHtmlTags("<font /"));
            Assert.AreEqual("", HtmlUtil.StripHtmlTags("<font /something"));
            Assert.AreEqual("", HtmlUtil.StripHtmlTags("<font / something"));
            Assert.AreEqual("finally closed", HtmlUtil.StripHtmlTags("<font / something> finally closed"));
            Assert.AreEqual("<", HtmlUtil.StripHtmlTags("<test <"));

            // Invalid characters in the tag name - don't treat them as tags.

            Assert.AreEqual("<not/atag>", HtmlUtil.StripHtmlTags("<not/atag>"));
            Assert.AreEqual("<this! is> not a tag but  is", HtmlUtil.StripHtmlTags("<this! is> not a tag but <this> is"));
            Assert.AreEqual("<http://some.url/which/should/stay.html> then a real  and <br><br /><br/>",
                HtmlUtil.StripHtmlTags("<http://some.url/which/should/stay.html> then a real <tag>"
                + " and <br><br /><br/>", "br"));
            Assert.AreEqual("< not", HtmlUtil.StripHtmlTags("< not <tag"));
        }

        [TestMethod]
        public void TestStripHtmlComments()
        {
            //Nothing to strip
            Assert.AreEqual("some text", HtmlUtil.StripHtmlComments("some text"));
            Assert.AreEqual("", HtmlUtil.StripHtmlComments(""));

            //Strip to nothing
            Assert.AreEqual("", HtmlUtil.StripHtmlComments("<!-- -->"));
            Assert.AreEqual("", HtmlUtil.StripHtmlComments("<!---->"));

            //Strip to end
            Assert.AreEqual("", HtmlUtil.StripHtmlComments("<!--"));
            Assert.AreEqual("some ", HtmlUtil.StripHtmlComments("some <!--text"));
            
            //strip at end
            Assert.AreEqual("some text ", HtmlUtil.StripHtmlComments("some text <!--with a comment-->"));

            //strip at beginning
            Assert.AreEqual("some text", HtmlUtil.StripHtmlComments("<!--with a comment-->some text"));

            //strip in the middle
            Assert.AreEqual("some  text", HtmlUtil.StripHtmlComments("some <!--with a comment--> text"));
        }

	    [TestMethod]
        public void TestCleanScriptAndEventTags()
        {
            const string doubleApostrophes = "This job ad's content contains a two apostrophes. It's getting truncated.";
            const string doubleApostrophesWithScript = "This job ad's content contains some <script>alert(0)</script>."
                + " It's getting truncated.";

            const string dirty1 = "<a href=\"linkme.com.au\" onClick=\"javascript:alert('hackslol11!!eleven!');\">hahah</a>";
            string clean1 = HtmlUtil.CleanScriptAndEventTags(dirty1);

            Assert.AreNotEqual(dirty1, clean1);
            Assert.IsFalse(HtmlUtil.ContainsScript(clean1));
            Assert.AreEqual("<a href=\"linkme.com.au\">hahah</a>", clean1);

            const string dirty2 = "<a href=\"linkme.com.au\" onClick='alert(\'hackslol11!!eleven!\');'>hahah</a>";
            string clean2 = HtmlUtil.CleanScriptAndEventTags(dirty1);

            Assert.AreNotEqual(dirty2, clean2);
            Assert.IsFalse(HtmlUtil.ContainsScript(clean2));
            Assert.AreEqual("<a href=\"linkme.com.au\">hahah</a>", clean2);

            Assert.AreEqual("", HtmlUtil.CleanScriptAndEventTags("<script type=\"text\\javascript\">alert('lolhax');</script>"));
            Assert.AreEqual("Valid text with  in it.", HtmlUtil.CleanScriptAndEventTags("Valid text with <script type=\"text\\javascript\">alert('lolhax');</script> in it."));
            Assert.AreEqual("", HtmlUtil.CleanScriptAndEventTags("<script <a href=\"linkme.com.au\" onclick=\"alert('smrt');\">hahah</a>>alert('This is smarta!');</script>"));

            // Bug 7104 - content between apostrophes gets removed.

            Assert.AreEqual(doubleApostrophes, HtmlUtil.CleanScriptAndEventTags(doubleApostrophes));
            Assert.AreEqual("This job ad's content contains some . It's getting truncated.",
                HtmlUtil.CleanScriptAndEventTags(doubleApostrophesWithScript));
        }

        private static void TestXss(string xss)
        {
            Assert.IsTrue(HtmlUtil.ContainsScript(xss));
            Assert.IsTrue(HtmlUtil.ContainsHtml(xss));
        }
    }
}
