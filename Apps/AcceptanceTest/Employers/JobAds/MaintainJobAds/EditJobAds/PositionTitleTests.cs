using System;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class PositionTitleTests
        : EditJobAdTests
    {
        private const string PositionTitle = "Lion tamer";
        private const string ChangedPositionTitle = "Tiger tamer";
        private const string ScriptPositionTitle = "This has <script>some</script> html.";
        private const string HtmlPositionTitle = "This has <b>some</b> html.";

        [TestMethod]
        public void TestHtml()
        {
            TestEdit(() => _positionTitleTextBox.Text = HtmlPositionTitle, j => Assert.AreEqual(HtmlPositionTitle, j.Description.PositionTitle));
        }

        protected override void SetValue(JobAd jobAd)
        {
            jobAd.Description.PositionTitle = PositionTitle;
        }

        protected override void SetDisplayValue()
        {
            _positionTitleTextBox.Text = ChangedPositionTitle;
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(ChangedPositionTitle, jobAd.Description.PositionTitle);
        }

        protected override void TestErrorValues(bool save, Guid jobAdId)
        {
            AssertErrorValue(save, jobAdId, () => _positionTitleTextBox.Text = new string('a', 250), "The position title must be no more than 200 characters in length.");
            AssertErrorValue(save, jobAdId, () => _positionTitleTextBox.Text = ScriptPositionTitle, "The position title appears to have some script in it.");
        }
    }
}
