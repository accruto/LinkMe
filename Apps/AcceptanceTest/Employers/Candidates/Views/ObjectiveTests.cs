using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Views
{
    [TestClass]
    public class ObjectiveTests
        : ViewsTests
    {
        private const string Objective = "To be rich";

        [TestMethod]
        public void TestNoObjective()
        {
            var member = CreateMember(null);
            TestCandidateUrls(member, () => AssertObjective(null));
        }

        [TestMethod]
        public void TestObjective()
        {
            var member = CreateMember(Objective);
            TestCandidateUrls(member, () => AssertObjective(Objective));
        }

        private Member CreateMember(string objective)
        {
            var member = CreateMember(0);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            var resume = _candidateResumesCommand.AddTestResume(candidate);
            resume.Objective = objective;
            _resumesCommand.UpdateResume(resume);
            return member;
        }

        private void AssertObjective(string objective)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='professional_section resume_section']//table[@class='professional']//tr");
            Assert.IsNotNull(node);
            var spanNodes = node[3].SelectNodes(".//span[@class='description']");
            Assert.IsNotNull(spanNodes);
            Assert.AreEqual(1, spanNodes.Count);
            Assert.AreEqual(objective ?? "", spanNodes[0].InnerText.Trim());
        }
    }
}
