using System.Linq;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Partials
{
    [TestClass]
    public class OtherTests
        : PartialProfileTests
    {
        private ReadOnlyUrl _otherUrl;

        private HtmlTextAreaTester _coursesTextBox;
        private HtmlTextBoxTester _awardsTextBox;
        private HtmlTextBoxTester _professionalTextBox;
        private HtmlTextAreaTester _interestsTextBox;
        private HtmlTextBoxTester _affiliationsTextBox;
        private HtmlTextAreaTester _otherTextBox;
        private HtmlTextAreaTester _refereesTextBox;

        [TestInitialize]
        public void TestInitialize()
        {
            _otherUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/other");

            _coursesTextBox = new HtmlTextAreaTester(Browser, "Courses");
            _awardsTextBox = new HtmlTextBoxTester(Browser, "Awards");
            _professionalTextBox = new HtmlTextBoxTester(Browser, "Professional");
            _interestsTextBox = new HtmlTextAreaTester(Browser, "Interests");
            _affiliationsTextBox = new HtmlTextBoxTester(Browser, "Affiliations");
            _otherTextBox = new HtmlTextAreaTester(Browser, "Other");
            _refereesTextBox = new HtmlTextAreaTester(Browser, "Referees");
        }

        protected override ReadOnlyUrl GetPartialUrl()
        {
            return _otherUrl;
        }

        protected override void AssertMember(IMember member, ICandidate candidate, IResume resume)
        {
            Assert.AreEqual(resume == null || resume.Courses == null ? "" : string.Join("\n", resume.Courses.ToArray()), _coursesTextBox.Text.Trim());
            Assert.AreEqual(resume == null || resume.Awards == null ? "" : string.Join("\n", resume.Awards.ToArray()), _awardsTextBox.Text);
            Assert.AreEqual(resume == null ? "" : resume.Professional, _professionalTextBox.Text);
            Assert.AreEqual(resume == null ? "" : resume.Interests, _interestsTextBox.Text.Trim());
            Assert.AreEqual(resume == null ? "" : resume.Affiliations, _affiliationsTextBox.Text);
            Assert.AreEqual(resume == null ? "" : resume.Other, _otherTextBox.Text.Trim());
            Assert.AreEqual(resume == null ? "" : resume.Referees, _refereesTextBox.Text.Trim());
        }
    }
}
