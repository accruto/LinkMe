using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class PositionTitleTests
        : CreateJobAdTests
    {
        private const string PositionTitle = "Lion tamer";
        private const string ScriptPositionTitle = "This has <script>some</script> html.";
        private const string HtmlPositionTitle = "This has <b>some</b> html.";

        [TestMethod]
        public void TestHtml()
        {
            TestInput(() => _positionTitleTextBox.Text = HtmlPositionTitle, j => Assert.AreEqual(HtmlPositionTitle, j.Description.PositionTitle));
        }

        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual("", _positionTitleTextBox.Text);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.IsNull(jobAd.Description.PositionTitle);
        }

        protected override void SetDisplayValue()
        {
            _positionTitleTextBox.Text = PositionTitle;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(PositionTitle, jobAd.Description.PositionTitle);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => _positionTitleTextBox.Text = new string('a', 250), "The position title must be no more than 200 characters in length.");
            AssertErrorValue(save, employer, () => _positionTitleTextBox.Text = ScriptPositionTitle, "The position title appears to have some script in it.");
        }
    }
}
