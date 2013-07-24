using System;
using System.Web;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class SummaryTests
        : EditJobAdTests
    {
        private const string Summary = "Doing it";
        private const string ChangedSummary = "Keep doing it";
        private const string ScriptSummary = "This has <script>some</script> html.";
        private const string HtmlSummary = "This has <b>some</b> html.";

        [TestMethod]
        public void TestHtml()
        {
            TestEdit(() => _summaryTextBox.Text = HtmlSummary, j => Assert.AreEqual(HttpUtility.HtmlEncode(HtmlSummary), j.Description.Summary));
        }

        [TestMethod]
        public void TestScript()
        {
            TestEdit(() => _summaryTextBox.Text = ScriptSummary, j => Assert.AreEqual(HttpUtility.HtmlEncode(ScriptSummary), j.Description.Summary));
        }

        protected override void SetValue(JobAd jobAd)
        {
            jobAd.Description.Summary = Summary;
        }

        protected override void SetDisplayValue()
        {
            _summaryTextBox.Text = ChangedSummary;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(ChangedSummary, jobAd.Description.Summary);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
            AssertErrorValue(save, jobAdId, () => _summaryTextBox.Text = new string('a', 350), "The summary must be no more than 300 characters in length.");
        }
    }
}
