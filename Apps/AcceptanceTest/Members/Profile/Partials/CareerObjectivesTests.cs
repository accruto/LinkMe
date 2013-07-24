using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Partials
{
    [TestClass]
    public class CareerObjectivesTests
        : PartialProfileTests
    {
        private ReadOnlyUrl _careerObjectivesUrl;

        private HtmlTextAreaTester _objectiveTextArea;
        private HtmlTextAreaTester _summaryTextArea;
        private HtmlTextAreaTester _skillsTextArea;

        [TestInitialize]
        public void TestInitialize()
        {
            _careerObjectivesUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/careerobjectives");

            _objectiveTextArea = new HtmlTextAreaTester(Browser, "Objective");
            _summaryTextArea = new HtmlTextAreaTester(Browser, "Summary");
            _skillsTextArea = new HtmlTextAreaTester(Browser, "Skills");
        }

        protected override ReadOnlyUrl GetPartialUrl()
        {
            return _careerObjectivesUrl;
        }

        protected override void AssertMember(IMember member, ICandidate candidate, IResume resume)
        {
            Assert.AreEqual(resume == null ? "" : resume.Objective, _objectiveTextArea.Text.Trim());
            Assert.AreEqual(resume == null ? "" : resume.Summary, _summaryTextArea.Text.Trim());
            Assert.AreEqual(resume == null ? "" : resume.Skills, _skillsTextArea.Text.Trim());
        }
    }
}
