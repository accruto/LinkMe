using System;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class PackageTests
        : EditJobAdTests
    {
        private const string Package = "Lots of money";
        private const string ChangedPackage = "Lots more money";
        private const string ScriptPackage = "This has <script>some</script> html.";
        private const string HtmlPackage = "This has <b>some</b> html.";

        [TestMethod]
        public void TestHtml()
        {
            TestEdit(() => _packageTextBox.Text = HtmlPackage, j => Assert.AreEqual(HtmlPackage, j.Description.Package));
        }

        protected override void SetValue(JobAd jobAd)
        {
            jobAd.Description.Package = Package;
        }

        protected override void SetDisplayValue()
        {
            _packageTextBox.Text = ChangedPackage;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(ChangedPackage, jobAd.Description.Package);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
            AssertErrorValue(save, jobAdId, () => _packageTextBox.Text = new string('a', 250), "The package must be no more than 200 characters in length.");
            AssertErrorValue(save, jobAdId, () => _packageTextBox.Text = ScriptPackage, "The package appears to have some script in it.");
        }
    }
}
