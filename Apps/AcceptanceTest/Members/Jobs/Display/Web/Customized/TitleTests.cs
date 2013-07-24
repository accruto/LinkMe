using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Display.Web.Customized
{
    [TestClass]
    public class TitleTests
        : CustomizedDisplayTests
    {
        [TestMethod]
        public void TestExtraCharacters()
        {
            var employer = CreateEmployer();

            // Create a job with a title with a '\n' plus extra characters.

            const string title = "CREDIT SUPERVISOR/ASSISTANT CREDIT MANAGER - sth east - great\n        $$ - industry guru";
            var jobAd = PostJobAd(employer, j => { j.Title = title; });

            // Get the job.

            var url = GetJobUrl(jobAd.Id, jobAd.Title, jobAd.Description.Location, jobAd.Description.Industries);
            Assert.AreEqual(new ReadOnlyApplicationUrl("~/").Path + "jobs/-/-/credit-supervisorassistant-credit-manager-sth-east-great-industry-guru/" + jobAd.Id, url.PathAndQuery);
            Get(url);
            AssertUrl(url);

            // Check the page title.

            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='title']");
            Assert.AreEqual(title, node.InnerText);
        }
    }
}