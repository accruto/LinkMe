using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Views
{
    [TestClass]
    public class HighestEducationLevelTests
        : ViewsTests
    {
        [TestMethod]
        public void TestNone()
        {
            var member = CreateMember(null);
            TestCandidateUrls(member, () => AssertHighestEducationLevel(null));
        }

        [TestMethod]
        public void TestHighestEducationLevel()
        {
            var member = CreateMember(EducationLevel.TradeCertificate);
            TestCandidateUrls(member, () => AssertHighestEducationLevel(EducationLevel.TradeCertificate));
        }

        private Member CreateMember(EducationLevel? educationLevel)
        {
            var member = base.CreateMember(0);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            candidate.HighestEducationLevel = educationLevel;
            _candidatesCommand.UpdateCandidate(candidate);
            return member;
        }

        private void AssertHighestEducationLevel(EducationLevel? educationLevel)
        {
            AssertHighestEducationLevel(educationLevel, "Summary");
            AssertHighestEducationLevel(educationLevel, "Education");
        }

        private void AssertHighestEducationLevel(EducationLevel? educationLevel, string tab)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='" + tab + "-details']//div[@class='education-history_section resume_section']//table[@class='education-level']//span[@class='description']");
            Assert.IsNotNull(node);
            Assert.AreEqual(educationLevel == null ? "N/A" : GetDisplayText(educationLevel.Value), node.InnerText);
        }

        private static string GetDisplayText(EducationLevel educationLevel)
        {
            switch (educationLevel)
            {
                case EducationLevel.TradeCertificate:
                    return "TAFE/Trade certificate";

                default:
                    Assert.Fail();
                    return null;
            }
        }
    }
}
