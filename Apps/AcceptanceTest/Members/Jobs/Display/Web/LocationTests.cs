using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Display.Web
{
    [TestClass]
    public class LocationTests
        : DisplayTests
    {
        [TestMethod]
        public void TestPostalSuburb()
        {
            TestLocation("Melbourne VIC 3000", "Melbourne VIC 3000", "melbourne-vic-3000");
        }

        [TestMethod]
        public void TestLocality()
        {
            TestLocation("3004", "3004 VIC", "3004-vic");
        }

        [TestMethod]
        public void TestRegion()
        {
            TestLocation("Melbourne", "Melbourne", "melbourne");
        }

        [TestMethod]
        public void TestCountrySubdivision()
        {
            TestLocation("VIC", "VIC", "vic");
        }

        [TestMethod]
        public void TestCountry()
        {
            TestLocation(null, "Australia", "australia");
        }

        [TestMethod]
        public void TestUnresolved()
        {
            TestLocation("Xyz", "Xyz", "xyz");
        }

        private void TestLocation(string location, string expectedLocation, string urlName)
        {
            var employer = CreateEmployer();

            var locationReference = _locationQuery.ResolveLocation(Australia, location);
            var jobAd = PostJobAd(employer, j => { j.Description.Location = locationReference; });

            // Get the job.

            var url = GetJobUrl(jobAd.Id, jobAd.Title, locationReference, jobAd.Description.Industries);
            Assert.AreEqual(new ReadOnlyApplicationUrl("~/").Path + "jobs/" + urlName + "/-/" + jobAd.Title.ToLower() + "/" + jobAd.Id, url.PathAndQuery);
            Get(url);
            AssertUrl(url);

            // Check the item on the page.

            Assert.AreEqual(expectedLocation, GetLocation());
        }

        private string GetLocation()
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='summary']//div[@class='item']/div[@class='desc']");
            Assert.IsTrue(nodes.Count > 0);
            return nodes[0].InnerText;
        }
    }
}