using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class BulletPointTests
        : CreateJobAdTests
    {
        private const string BulletPointFormat = "This is bullet point {0}.";
        private const string ScriptBulletPoint = "This has <script>some</script> html.";
        private const string HtmlBulletPointFormat = "This has <b>some</b> {0}html.";

        [TestMethod]
        public void TestHtml()
        {
            TestInput(
                () =>
                {
                    _bulletPoint1TextBox.Text = string.Format(HtmlBulletPointFormat, 1);
                    _bulletPoint2TextBox.Text = string.Format(HtmlBulletPointFormat, 2);
                    _bulletPoint3TextBox.Text = string.Format(HtmlBulletPointFormat, 3);
                },
                j =>
                {
                    Assert.AreEqual(string.Format(HtmlBulletPointFormat, 1), j.Description.BulletPoints[0]);
                    Assert.AreEqual(string.Format(HtmlBulletPointFormat, 2), j.Description.BulletPoints[1]);
                    Assert.AreEqual(string.Format(HtmlBulletPointFormat, 3), j.Description.BulletPoints[2]);
                });
        }

        protected override void AssertDefaultDisplayValue(IEmployer employer)
        {
            Assert.AreEqual("", _bulletPoint1TextBox.Text);
            Assert.AreEqual("", _bulletPoint2TextBox.Text);
            Assert.AreEqual("", _bulletPoint2TextBox.Text);
        }

        protected override void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd)
        {
            Assert.AreEqual(0, jobAd.Description.BulletPoints.Count);
        }

        protected override void SetDisplayValue()
        {
            _bulletPoint1TextBox.Text = string.Format(BulletPointFormat, 1);
            _bulletPoint2TextBox.Text = string.Format(BulletPointFormat, 2);
            _bulletPoint3TextBox.Text = string.Format(BulletPointFormat, 3);
        }

        protected override void AssertSavedValue(JobAd jobAd)
        {
            Assert.AreEqual(3, jobAd.Description.BulletPoints.Count);
            Assert.AreEqual(string.Format(BulletPointFormat, 1), jobAd.Description.BulletPoints[0]);
            Assert.AreEqual(string.Format(BulletPointFormat, 2), jobAd.Description.BulletPoints[1]);
            Assert.AreEqual(string.Format(BulletPointFormat, 3), jobAd.Description.BulletPoints[2]);
        }

        protected override void TestErrorValues(bool save, IEmployer employer)
        {
            AssertErrorValue(save, employer, () => _bulletPoint1TextBox.Text = new string('a', 300), "The bullet points must be no more than 255 characters in length.");
            AssertErrorValue(save, employer, () => _bulletPoint2TextBox.Text = new string('a', 300), "The bullet points must be no more than 255 characters in length.");
            AssertErrorValue(save, employer, () => _bulletPoint3TextBox.Text = new string('a', 300), "The bullet points must be no more than 255 characters in length.");
            AssertErrorValue(save, employer, () => _bulletPoint1TextBox.Text = ScriptBulletPoint, "The bullet points appears to have some script in it.");
            AssertErrorValue(save, employer, () => _bulletPoint2TextBox.Text = ScriptBulletPoint, "The bullet points appears to have some script in it.");
            AssertErrorValue(save, employer, () => _bulletPoint3TextBox.Text = ScriptBulletPoint, "The bullet points appears to have some script in it.");
        }
    }
}
