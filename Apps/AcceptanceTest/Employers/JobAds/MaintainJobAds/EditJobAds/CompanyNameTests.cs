using System;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class CompanyNameTests
        : EditJobAdTests
    {
        private const string CompanyName = "Acme";
        private const string ChangedCompanyName = "Ajax";
        private const string ScriptCompanyName = "This has <script>some</script> html.";
        private const string HtmlCompanyName = "This has <b>some</b> html.";

        [TestMethod]
        public void TestHtml()
        {
            TestEdit(() => _companyNameTextBox.Text = HtmlCompanyName, j => Assert.AreEqual(HtmlCompanyName, j.Description.CompanyName));
        }

        protected override void SetValue(JobAd jobAd)
        {
            jobAd.Description.CompanyName = CompanyName;
        }

        protected override void SetDisplayValue()
        {
            _companyNameTextBox.Text = ChangedCompanyName;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(ChangedCompanyName, jobAd.Description.CompanyName);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
            AssertErrorValue(save, jobAdId, () => _companyNameTextBox.Text = ScriptCompanyName, "The company name appears to have some script in it.");
        }
    }
}
