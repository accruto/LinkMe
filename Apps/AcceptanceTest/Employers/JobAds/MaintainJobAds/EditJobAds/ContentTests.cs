using System;
using System.Web;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class ContentTests
        : EditJobAdTests
    {
        private const string Content = "Some content for the job ad";
        private const string ChangedContent = "Some changed content for the job ad";
        private const string ScriptContent = "This has <script>some</script> html.";
        private const string HtmlContent = "This has <b>some</b> html.";

        [TestMethod]
        public void TestHtml()
        {
            TestEdit(() => _contentTextBox.Text = HtmlContent, j => Assert.AreEqual(HttpUtility.HtmlEncode(HtmlContent), j.Description.Content));
        }

        [TestMethod]
        public void TestScript()
        {
            TestEdit(() => _contentTextBox.Text = ScriptContent, j => Assert.AreEqual(HttpUtility.HtmlEncode(ScriptContent), j.Description.Content));
        }

        protected override void SetValue(JobAd jobAd)
        {
            jobAd.Description.Content = Content;
        }

        protected override void SetDisplayValue()
        {
            _contentTextBox.Text = ChangedContent;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(ChangedContent, jobAd.Description.Content);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
            AssertErrorValue(save, jobAdId, () => _contentTextBox.Text = "", "The content is required.");
        }
    }
}
