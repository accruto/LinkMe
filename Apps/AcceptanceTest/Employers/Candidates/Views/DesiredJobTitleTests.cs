using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Views
{
    [TestClass]
    public class DesiredJobTitleTests
        : ViewsTests
    {
        private const string DesiredJobTitle = "Archeologist";

        [TestMethod]
        public void TestNoDesiredJobTitle()
        {
            var member = CreateMember(null);
            TestCandidateUrls(member, () => AssertDesiredJobTitle(null));
        }

        [TestMethod]
        public void TestDesiredJobTitle()
        {
            var member = CreateMember(DesiredJobTitle);
            TestCandidateUrls(member, () => AssertDesiredJobTitle(DesiredJobTitle));
        }

        private Member CreateMember(string desiredJobTitle)
        {
            var member = CreateMember(0);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            candidate.DesiredJobTitle = desiredJobTitle;
            _candidatesCommand.UpdateCandidate(candidate);
            return member;
        }

        private void AssertDesiredJobTitle(string desiredJobTitle)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='professional_section resume_section']//table[@class='professional']//tr");
            Assert.IsNotNull(node);
            var spanNodes = node[1].SelectNodes("//td//span[@class='detail-content']");
            Assert.IsNotNull(spanNodes);

            if (desiredJobTitle == null)
                Assert.AreEqual("Open to all job types", spanNodes[0].InnerText);
            else
                Assert.AreEqual(desiredJobTitle, spanNodes[0].InnerText);
        }
    }
}
