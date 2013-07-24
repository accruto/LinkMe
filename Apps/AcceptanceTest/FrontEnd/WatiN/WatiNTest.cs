using System;
using LinkMe.AcceptanceTest.FrontEnd.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;

namespace LinkMe.AcceptanceTest.FrontEnd.WatiN
{
    [TestClass, Ignore]
    public class WatiNTest
    {
        [TestMethod]
        [STAThread]
        public void TestGoogle()
        {
            using (var browser = new IE())
            {
                browser.Setup();
                browser.GoTo("http://www.google.com.au/");
                browser.TextField(Find.ByName("q")).TypeText("Hello WatiN");
                browser.Button(Find.ByName("btnG")).Click();
                var link = browser.Link(Find.ByUrl("http://watin.org"));
                Assert.AreEqual(link.Text, "WatiN");
            }
        }
    }
}
