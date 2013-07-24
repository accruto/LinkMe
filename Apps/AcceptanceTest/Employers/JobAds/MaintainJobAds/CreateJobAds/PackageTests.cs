using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class PackageTests
        : CreateJobAdTests
    {
        private const string Package = "Lots of money";
        private const string ScriptPackage = "This has <script>some</script> html.";
        private const string HtmlPackage = "This has <b>some</b> html.";

        [TestMethod]
        public void TestHtml()
        {
            TestInput(() => _packageTextBox.Text = HtmlPackage, j => Assert.AreEqual(HtmlPackage, j.Description.Package));
        }

        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual("", _packageTextBox.Text);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.IsNull(jobAd.Description.Package);
        }

        protected override void SetDisplayValue()
        {
            _packageTextBox.Text = Package;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(Package, jobAd.Description.Package);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => _packageTextBox.Text = new string('a', 250), "The package must be no more than 200 characters in length.");
            AssertErrorValue(save, employer, () => _packageTextBox.Text = ScriptPackage, "The package appears to have some script in it.");
        }
    }
}
