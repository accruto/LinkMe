using LinkMe.Apps.Agents.Communications.Emails;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails
{
    [TestClass]
    public class EmailsExtensionsTests
    {
        private const string Html = @"<p>Dear Robert<br /><br /></p>
<p><span>My client is looking for a Strong Database Developer to work for a large consulting organisation. </span></p>
<p><span>Please find the role attributes - </span></p>
<p><span>Would this be of interest to you or your colleagues? </span><br /><br />Regards,<br /><br />Monty Burns</p>
<p><span><strong>M:</strong></span><span> 0400111111 &nbsp;|&nbsp; </span><strong><span>T:</span></strong><span> 02 9999 9999&nbsp; |&nbsp; </span><strong><span>F:</span></strong><span> 02 9999 9999 <br /></span><strong><span>E:</span></strong><span> <a href=""mailto:monty@test.linkme.net.au""><span>monty.burns@test.linkme.net.au</span></a>&nbsp; |&nbsp; </span><strong><span>W:</span></strong><span> <a href=""http://www.linkme.com.au/""><span>www.linkme.com.au <http://www.linkme.com.au/> </span></a> <br /></span><strong><span>A:</span></strong><span> Level 1, 20 Smithers Street, Sydney NSW 2000</span>&nbsp;</p>
<p>&nbsp;</p>
";

        private const string PlainHtml = @"

Dear Robert



My client is looking for a Strong Database Developer to work for a large consulting organisation.

Please find the role attributes -

Would this be of interest to you or your colleagues?

Regards,

Monty Burns

M:0400111111 | T:02 9999 9999 | F:02 9999 9999
E:monty.burns@test.linkme.net.au | W:www.linkme.com.au
A:Level 1, 20 Smithers Street, Sydney NSW 2000 

 ";

        [TestMethod]
        public void TestConvertHtmlToPlainText()
        {
            Assert.AreEqual(PlainHtml, Html.ConvertHtmlToPlainText());
        }
    }
}
