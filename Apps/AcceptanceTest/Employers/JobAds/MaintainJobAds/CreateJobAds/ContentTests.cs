using System.Web;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class ContentTests
        : CreateJobAdTests
    {
        private const string Content = "Some content for the job ad";
        private const string ScriptContent = "This has <script>some</script> html.";
        private const string HtmlContent = "This has <b>some</b> html.";

        [TestMethod]
        public void TestHtml()
        {
            TestInput(() => _contentTextBox.Text = HtmlContent, j => Assert.AreEqual(HttpUtility.HtmlEncode(HtmlContent), j.Description.Content));
        }

        [TestMethod]
        public void TestScript()
        {
            TestInput(() => _contentTextBox.Text = ScriptContent, j => Assert.AreEqual(HttpUtility.HtmlEncode(ScriptContent), j.Description.Content));
        }

        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual("", _contentTextBox.Text.Trim());
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.AreEqual(DefaultContent, jobAd.Description.Content);
        }

        protected override void SetDisplayValue()
        {
            _contentTextBox.Text = Content;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(Content, jobAd.Description.Content);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => _contentTextBox.Text = "", "The content is required.");
        }
    }
}
