using System;
using LinkMe.Apps.Services.External.JobSearch;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.External.JobSearch
{
    [TestClass]
    public class ContentMapperTests
    {
        private const string Host = "testhost";
        private const string AppPath = "linkme";
        private readonly Guid _jobAdId = Guid.NewGuid();
        private string _applyUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            //ApplicationContext.SetupApplications(WebSite.LinkMe); // set up the application to point to the web site if needed
            Application.Current = new Application(Host, 80, AppPath);
            _applyUrl = string.Format("http://{0}/{1}/jobs/{2}", Host, AppPath, _jobAdId.ToString());
        }

        [TestMethod]
        public void ShortTextContent()
        {
            string description = new ContentMapper(50, 2000).MapBody(_jobAdId, null, "Hello DEEWR!");
            const string expected = "Hello DEEWR!<br/><br/><b><a href=\"{0}\">Please click here to apply.</a></b>";
            Assert.AreEqual(string.Format(expected, _applyUrl), description);
        }

        [TestMethod]
        public void ShortTextContentWithBullets()
        {
            string description = new ContentMapper(50, 2000).MapBody(_jobAdId, new[] { "bullet 1", "bullet 2" }, "Hello DEEWR!");
            const string expected = "<ul><li>bullet 1. </li><li>bullet 2. </li></ul>Hello DEEWR!<br/><br/><b><a href=\"{0}\">Please click here to apply.</a></b>";
            Assert.AreEqual(string.Format(expected, _applyUrl), description);
        }

        [TestMethod]
        public void ShortHtmlContent()
        {
            string description = new ContentMapper(50, 2000).MapBody(_jobAdId, null, "<p>Para 1</p><p>Para 2</p>");
            const string expected = "Para 1<br/><br/>Para 2<br/><br/><b><a href=\"{0}\">Please click here to apply.</a></b>";
            Assert.AreEqual(string.Format(expected, _applyUrl), description);
        }

        [TestMethod]
        public void ShortTextContentWithNbsp()
        {
            string description = new ContentMapper(50, 2000).MapBody(_jobAdId, null, "Hello\u00A0DEEWR!");
            const string expected = "Hello&#160;DEEWR!<br/><br/><b><a href=\"{0}\">Please click here to apply.</a></b>";
            Assert.AreEqual(string.Format(expected, _applyUrl), description);
        }

        [TestMethod]
        public void ShortHtmlContentWithNbsp()
        {
            string description = new ContentMapper(50, 2000).MapBody(_jobAdId, null, "Hello&nbsp;DEEWR!");
            const string expected = "Hello&#160;DEEWR!<br/><br/><b><a href=\"{0}\">Please click here to apply.</a></b>";
            Assert.AreEqual(string.Format(expected, _applyUrl), description);
        }

        [TestMethod]
        public void SimpleRealHtmlContent()
        {
            const string content = "<html><body><ul><li>Established Company</li><li>Permanent position</li><li>Cutting-edge technology</li></ul><p>My client a leader in providing application services to specific vertical markets are seeking an experienced Developer/ Analyst Programmer who is able to provide skills and experience within the C++ Socket Programming space. You will be able to to provide development skills with the networking of applications which include low-level (deep) architecture in socket programming. UNIX Platforms &amp; Web Servers.</p><p><strong>Requirements:</strong></p><p>To be considered for this highly sort after position, you will require the following requirements: </p><ul><li>5 + years experience in pure C# / C++ development </li><li>Current or recent experience of working within a demanding and complex development project. </li><li>Outstanding communication skills to work closely with the Business </li><li>Experience with development/ application encryption/ security</li><li>Socket-Programming</li><li>Experience with MDX (although not essential)</li><li>.Net Experience highly desirable</li><li>C++ and socket programming, Encryption technologies, C# .NET and remoting, web service programming such as SOAP</li></ul><p>This role is based in Sydney and wuld suit those with excellent skills in Socket programming &amp; encryption skills.</p><p>If this sounds like you, then please &quot;APPLY NOW&quot; or alternatively call Ray Chau on 02 8234 3514 to discuss the opportunity in greater detail.</p><p> </p></html></body>";
            string description = new ContentMapper(50, 2000).MapBody(_jobAdId, new[] { "bullet 1", "bullet 2" }, content);
            const string expected = "<ul><li>bullet 1. </li><li>bullet 2. </li></ul>Established Company<br/><br/>Permanent position<br/><br/>Cutting-edge technology<br/><br/>My client a leader in providing application services to specific vertical markets are seeking an experienced Developer/ Analyst Programmer who is able to provide skills and experience within the C++ Socket Programming space. You will be able to to provide development skills with the networking of applications which include low-level (deep) architecture in socket programming. UNIX Platforms &amp; Web Servers.<br/><br/>Requirements:<br/><br/>To be considered for this highly sort after position, you will require the following requirements:<br/><br/>5 + years experience in pure C# / C++ development<br/><br/>Current or recent experience of working within a demanding and complex development project.<br/><br/>Outstanding communication skills to work closely with the Business<br/><br/>Experience with development/ application encryption/ security<br/><br/>Socket-Programming<br/><br/>Experience with MDX (although not essential)<br/><br/>.Net Experience highly desirable<br/><br/>C++ and socket programming, Encryption technologies, C# .NET and remoting, web service programming such as SOAP<br/><br/>This role is based in Sydney and wuld suit those with excellent skills in Socket programming &amp; encryption skills.<br/><br/>If this sounds like you, then please &quot;APPLY NOW&quot; or alternatively call Ray Chau on 02 8234 3514 to discuss the opportunity in greater detail.<br/><br/><br/><br/><b><a href=\"{0}\">Please click here to apply.</a></b>";
            Assert.AreEqual(string.Format(expected, _applyUrl), description);
        }

        [TestMethod]
        public void TruncatedSimpleRealHtmlContent()
        {
            const string content = "<html><body><ul><li>Established Company</li><li>Permanent position</li><li>Cutting-edge technology</li></ul><p>My client a leader in providing application services to specific vertical markets are seeking an experienced Developer/ Analyst Programmer who is able to provide skills and experience within the C++ Socket Programming space. You will be able to to provide development skills with the networking of applications which include low-level (deep) architecture in socket programming. UNIX Platforms &amp; Web Servers.</p><p><strong>Requirements:</strong></p><p>To be considered for this highly sort after position, you will require the following requirements: </p><ul><li>5 + years experience in pure C# / C++ development </li><li>Current or recent experience of working within a demanding and complex development project. </li><li>Outstanding communication skills to work closely with the Business </li><li>Experience with development/ application encryption/ security</li><li>Socket-Programming</li><li>Experience with MDX (although not essential)</li><li>.Net Experience highly desirable</li><li>C++ and socket programming, Encryption technologies, C# .NET and remoting, web service programming such as SOAP</li></ul><p>This role is based in Sydney and wuld suit those with excellent skills in Socket programming &amp; encryption skills.</p><p>If this sounds like you, then please &quot;APPLY NOW&quot; or alternatively call Ray Chau on 02 8234 3514 to discuss the opportunity in greater detail.</p><p> </p></html></body>";
            string description = new ContentMapper(50, 250).MapBody(_jobAdId, new[] { "bullet 1", "bullet 2" }, content);
            const string expected = "<ul><li>bullet 1. </li><li>bullet 2. </li></ul>Established Company<br/><br/>Permanent position<br/><br/>Cutting-edge...<br/><br/><b><a href=\"{0}\">Please click here to apply.</a></b>";
            Assert.AreEqual(string.Format(expected, _applyUrl), description);
        }

        [TestMethod]
        public void TruncatedComplexRealHtmlContent()
        {
            const string content = "<P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">A Casual and Full Time opportunity now exist for customer service driven contact center operators, based in our state of the art Monitoring Centre servicing residential and commercial customer alarms. <o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">&nbsp;<o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">Family owned and operated, SNP Security is Australia's largest integrated security solutions provider and strive to be an industry ?Employer of Choice?. Our values are fundamental to&nbsp;SNP Security's&nbsp;success, they are the foundation of our company, define who we are and set us apart from the competition.&nbsp;Five core values resonate at SNP Security: <B>Customer Focus</B>, <B>Teamwork</B>, <B>Leadership</B>, <B>Growth</B> and <B>Development</B>, and <B>Family Company Values</B>. <o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">&nbsp;<o:p></o:p></SPAN></P> <P class=MsoBodyText style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US><FONT face=Calibri size=3>You will work along side an experienced team of operators and customer service staff servicing our key residential and commercial customers across Australia. </FONT></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">Working in our state of the art office, you will use your exceptional customer service skills to respond in line with account requirements to alarms activations. <o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">&nbsp;<o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">To be considered, you must posses: <o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">?<SPAN style=\"mso-tab-count: 1\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</SPAN>Exceptional customer service skills<o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">?<SPAN style=\"mso-tab-count: 1\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</SPAN>Excellent written &amp; verbal communication skills<o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">?<SPAN style=\"mso-tab-count: 1\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </SPAN>Ability to solve problems and negotiate difficult customer interactions<o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">?<SPAN style=\"mso-tab-count: 1\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </SPAN>Strong attention to detail<o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">?<SPAN style=\"mso-tab-count: 1\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Provisional </SPAN>Class 1A Security Licence - minimum requirement<BR></SPAN><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">&nbsp;<o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">As an online role, a level of computer savviness will be required ? experience using MAS would be highly regarded but is not essential.<o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">&nbsp;<o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">SNP employees have access to discounts at hundreds of stores, ongoing training and development and more.<o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">&nbsp;<o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">Interested applicants can apply by visiting the SNP website at jobs.snpsecurity.com.au and applying for Vacancy 95: Monitoring Operator. Be sure to attach a resume with cover letter specifying what type of security licence you hold and what skills you possess that make you suitable for our vacancy. <o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">&nbsp;<o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">Applications close Friday 5<SUP>th</SUP> March 2010.<o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">&nbsp;<o:p></o:p></SPAN></P> <P class=MsoNormal style=\"MARGIN: 0cm 0cm 0pt\"><SPAN lang=EN-US style=\"FONT-SIZE: 11pt; FONT-FAMILY: Calibri; mso-bidi-font-size: 12.0pt\">M/L 400674602<o:p></o:p></SPAN></P>";
            string description = new ContentMapper(50, 2000).MapBody(_jobAdId, new[] { "bullet 1", "bullet 2" }, content);
            const string expected = "<ul><li>bullet 1. </li><li>bullet 2. </li></ul>A Casual and Full Time opportunity now exist for customer service driven contact center operators, based in our state of the art Monitoring Centre servicing residential and commercial customer alarms.<br/><br/>&#160;<br/><br/>Family owned and operated, SNP Security is Australia&#39;s largest integrated security solutions provider and strive to be an industry ?Employer of Choice?. Our values are fundamental to&#160;SNP Security&#39;s&#160;success, they are the foundation of our company, define who we are and set us apart from the competition.&#160;Five core values resonate at SNP Security: Customer Focus , Teamwork , Leadership , Growth and Development , and Family Company Values .<br/><br/>&#160;<br/><br/>You will work along side an experienced team of operators and customer service staff servicing our key residential and commercial customers across Australia.<br/><br/>Working in our state of the art office, you will use your exceptional customer service skills to respond in line with account requirements to alarms activations.<br/><br/>&#160;<br/><br/>To be considered, you must posses:<br/><br/>? &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Exceptional customer service skills<br/><br/>? &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Excellent written &amp; verbal communication skills<br/><br/>? &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Ability to solve problems and negotiate difficult customer interactions<br/><br/>? &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Strong attention to detail<br/><br/>? &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Provisional Class 1A Security Licence - minimum requirement...<br/><br/><b><a href=\"{0}\">Please click here to apply.</a></b>";
            Assert.AreEqual(string.Format(expected, _applyUrl), description);
        }

        [TestMethod]
        public void ShortTitle()
        {
            string actual = new ContentMapper(50, 2000).MapTitle("CEO");
            const string expected = "CEO";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LongTitle()
        {
            const string title = "Quality Assurance Analyst | Accountants Practice Software";
            const string expected = "Quality Assurance Analyst | Accountants...";
            string actual = new ContentMapper(50, 2000).MapTitle(title);
            Assert.AreEqual(expected, actual);
        }
    }
}