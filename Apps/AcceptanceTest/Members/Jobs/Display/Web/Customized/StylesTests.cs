using System;
using System.Linq;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Display.Web.Customized
{
    [TestClass]
    public class StylesTests
        : CustomizedDisplayTests
    {
        [TestMethod]
        public void TestStyleSheet()
        {
            var employer = CreateEmployer();
            var jobAd = PostJobAd(employer);

            // Get the job.

            Get(GetJobUrl(jobAd.Id));

            var stylesheetUrl = new ReadOnlyApplicationUrl("~/Content/Organisations/Css/JobAds/Database Consultants Australia (VIC) - 703317d4-da51-49e8-a553-b9c94af70156.css");
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//link");
            Assert.IsNotNull(nodes);
            Assert.IsTrue(nodes.Any(n => string.Equals(n.Attributes["href"].Value, stylesheetUrl.Path, StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
