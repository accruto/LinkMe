using System.Web;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class SummaryTests
        : CreateJobAdTests
    {
        private const string Summary = "Doing it";
        private const string ScriptSummary = "This has <script>some</script> html.";
        private const string HtmlSummary = "This has <b>some</b> html.";

        [TestMethod]
        public void TestHtml()
        {
            TestInput(() => _summaryTextBox.Text = HtmlSummary, j => Assert.AreEqual(HttpUtility.HtmlEncode(HtmlSummary), j.Description.Summary));
        }

        [TestMethod]
        public void TestScript()
        {
            TestInput(() => _summaryTextBox.Text = ScriptSummary, j => Assert.AreEqual(HttpUtility.HtmlEncode(ScriptSummary), j.Description.Summary));
        }

        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual("", _summaryTextBox.Text.Trim());
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.IsNull(jobAd.Description.Summary);
        }

        protected override void SetDisplayValue()
        {
            _summaryTextBox.Text = Summary;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(Summary, jobAd.Description.Summary);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => _summaryTextBox.Text = new string('a', 350), "The summary must be no more than 300 characters in length.");
        }
    }
}
