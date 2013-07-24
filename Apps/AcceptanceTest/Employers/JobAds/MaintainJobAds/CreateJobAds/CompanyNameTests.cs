using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class CompanyNameTests
        : CreateJobAdTests
    {
        private const string CompanyName = "Acme";
        private const string ScriptCompanyName = "This has <script>some</script> html.";
        private const string HtmlCompanyName = "This has <b>some</b> html.";

        [TestMethod]
        public void TestHtml()
        {
            TestInput(() => _companyNameTextBox.Text = HtmlCompanyName, j => Assert.AreEqual(HtmlCompanyName, j.Description.CompanyName));
        }

        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual("", _companyNameTextBox.Text);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.IsNull(jobAd.Description.CompanyName);
        }

        protected override void SetDisplayValue()
        {
            _companyNameTextBox.Text = CompanyName;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(CompanyName, jobAd.Description.CompanyName);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => _companyNameTextBox.Text = ScriptCompanyName, "The company name appears to have some script in it.");
        }
    }
}
