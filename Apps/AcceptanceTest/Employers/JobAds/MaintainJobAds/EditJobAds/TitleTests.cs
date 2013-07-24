using System;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class TitleTests
        : EditJobAdTests
    {
        private const string Title = "Archeologist";
        private const string ChangedTitle = "Paleontologist";
        private const string ScriptTitle = "This has <script>some</script> html.";
        private const string HtmlTitle = "This has <b>some</b> html.";

        [TestMethod]
        public void TestHtml()
        {
            TestEdit(() => _titleTextBox.Text = HtmlTitle, j => Assert.AreEqual(HtmlTitle, j.Title));
        }

        protected override void SetValue(JobAd jobAd)
        {
            jobAd.Title = Title;
        }

        protected override void SetDisplayValue()
        {
            _titleTextBox.Text = ChangedTitle;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(ChangedTitle, jobAd.Title);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
            AssertErrorValue(save, jobAdId, () => _titleTextBox.Text = "", "The title is required.");
            AssertErrorValue(save, jobAdId, () => _titleTextBox.Text = new string('a', 250), "The title must be no more than 200 characters in length.");
            AssertErrorValue(save, jobAdId, () => _titleTextBox.Text = ScriptTitle, "The title appears to have some script in it.");
        }
    }
}
