using System;
using LinkMe.AcceptanceTest.FrontEnd.Extension;
using LinkMe.AcceptanceTest.Integration.JobAds.GetJobAds;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;

namespace LinkMe.AcceptanceTest.FrontEnd.Member.JobAd
{
    [TestClass]
    [Ignore]
    public class CareerOneTest : WebTestClass
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            (new JobAdsTests()).TestAddJobAdForCustomCss();
        }

        [TestMethod]
        [STAThread]
        public void TestEMailFriends()
        {
            using(var browser = new IE())
            {
                browser.Setup();
                browser.GoToHomePage();
                //job search
                browser.TextField(Find.ById("Keywords")).TypeText("linkme");
                browser.Div(Find.ById("search")).Click();
                browser.WaitForComplete();
                Assert.AreEqual(browser.Url, new ReadOnlyApplicationUrl(false, "~/search/jobs/advanced?performSearch=true&keywords=linkme").AbsoluteUri);
                //none CareerOne job, old job ad page, has 'Already a member?'
                browser.Link(Find.ByClass("title") && Find.ByText("LinkMe test job ads - non CareerOne")).Click();
                browser.WaitForComplete();
                Assert.IsTrue(browser.Elements.Filter(Find.ByText("Already a member?")).Count >= 1);
                browser.Back();
                //CareerOne job, new job ad page, has .emailbutton
                browser.Link(Find.ByClass("title") && Find.ByText("LinkMe test job ads")).Click();
                browser.WaitForComplete();
                Assert.AreEqual(browser.Eval("$(\".emailbutton\").length"), "1");
                //click email button, show email layer
                browser.Div(Find.ByClass("emailbutton")).Click();
                browser.WaitForComplete();
                Assert.AreEqual(browser.Eval("$(\".emailbutton\").hasClass(\"active\")"), "true");
                Assert.AreEqual(browser.Div(Find.ByClass("emaillayer")).Style.Display, "block");
                Assert.AreEqual(browser.Div(Find.ByClass("err-info")).Style.Display, "none");
                Assert.AreEqual(browser.Div(Find.ByClass("succ-info")).Style.Display, "none");
                //click cancel button, hide email layer
                browser.Eval("$(\".emaillayer .cancelbutton\").click();");
                browser.WaitForComplete();
                Assert.AreEqual(browser.Eval("$(\".emailbutton.active\").length"), "0");
                Assert.AreEqual(browser.Div(Find.ByClass("emaillayer")).Style.Display, "none");
                //click email button again, click submit then, test empty input
                browser.Div(Find.ByClass("emailbutton")).Click();
                browser.WaitForComplete();
                browser.Eval("$(\".emaillayer .sendbutton\").click();");
                browser.WaitForComplete();
                Assert.AreEqual(browser.Eval("$(\"#FromName\").closest(\".control\").hasClass(\"error\")"), "true");
                Assert.AreEqual(browser.Eval("$(\"#FromName\").closest(\".field\").find(\".error-msg\").length"), "1");
                Assert.AreEqual(browser.Eval("$(\"#FromEmailAddress\").closest(\".control\").hasClass(\"error\")"), "true");
                Assert.AreEqual(browser.Eval("$(\"#FromEmailAddress\").closest(\".field\").find(\".error-msg\").length"), "1");
                Assert.AreEqual(browser.Eval("$(\"#ToNames\").closest(\".control\").hasClass(\"error\")"), "true");
                Assert.AreEqual(browser.Eval("$(\"#ToNames\").closest(\".field\").find(\".error-msg\").length"), "1");
                Assert.AreEqual(browser.Eval("$(\"#ToEmailAddresses\").closest(\".control\").hasClass(\"error\")"), "true");
                Assert.AreEqual(browser.Eval("$(\"#ToEmailAddresses\").closest(\".field\").find(\".error-msg\").length"), "1");
                Assert.AreEqual(browser.Div(Find.ByClass("err-info")).Style.Display, "block");
                Assert.AreEqual(browser.Div(Find.ByClass("succ-info")).Style.Display, "none");
                //test numbers of ToNames and ToEmailAddresses are diff
                browser.TextField(Find.ById("ToNames")).TypeText("c,d");
                browser.TextField(Find.ById("ToEmailAddresses")).TypeText("c@gmail.com");
                browser.Eval("$(\".emaillayer .sendbutton\").click();");
                browser.WaitForComplete();
                Assert.AreEqual(browser.Eval("$(\".err-info:contains('match')\").length"), "1");
                Assert.AreEqual(browser.Div(Find.ByClass("err-info")).Style.Display, "block");
                Assert.AreEqual(browser.Div(Find.ByClass("succ-info")).Style.Display, "none");
                //test succ
                browser.TextField(Find.ById("FromName")).TypeText("a");
                browser.TextField(Find.ById("FromEmailAddress")).TypeText("a@gmail.com");
                browser.TextField(Find.ById("ToNames")).Clear();
                browser.TextField(Find.ById("ToNames")).TypeText("b,c");
                browser.TextField(Find.ById("ToEmailAddresses")).Clear();
                browser.TextField(Find.ById("ToEmailAddresses")).TypeText("b@gmail.com,c@gmail.com");
                browser.Eval("$(\".emaillayer .sendbutton\").click();");
                browser.WaitForComplete();
                Assert.AreEqual(browser.Div(Find.ByClass("err-info")).Style.Display, "none");
                Assert.AreEqual(browser.Div(Find.ByClass("succ-info")).Style.Display, "block");
            }
        }

        [TestMethod]
        [STAThread]
        public void TestApplied()
        {
            
        }
    }
}