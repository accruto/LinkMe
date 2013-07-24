using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class TitleTests
        : CreateJobAdTests
    {
        private const string Title = "Archeologist";
        private const string ScriptTitle = "This has <script>some</script> html.";
        private const string HtmlTitle = "This has <b>some</b> html.";

        [TestMethod]
        public void TestHtml()
        {
            TestInput(() => _titleTextBox.Text = HtmlTitle, j => Assert.AreEqual(HtmlTitle, j.Title));
        }

        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual("", _titleTextBox.Text);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.AreEqual(DefaultTitle, jobAd.Title);
        }

        protected override void SetDisplayValue()
        {
            _titleTextBox.Text = Title;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(Title, jobAd.Title);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => _titleTextBox.Text = "", "The title is required.");
            AssertErrorValue(save, employer, () => _titleTextBox.Text = new string('a', 250), "The title must be no more than 200 characters in length.");
            AssertErrorValue(save, employer, () => _titleTextBox.Text = ScriptTitle, "The title appears to have some script in it.");
        }
    }
}
