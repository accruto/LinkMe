using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Views
{
    [TestClass]
    public class RecentProfessionSeniorityTests
        : ViewsTests
    {
        [TestMethod]
        public void TestNone()
        {
            var member = CreateMember(null, null);
            TestCandidateUrls(member, () => AssertRecentProfession(null, null));
        }

        [TestMethod]
        public void TestRecentProfession()
        {
            var member = CreateMember(Profession.QualityAssurance, null);
            TestCandidateUrls(member, () => AssertRecentProfession(Profession.QualityAssurance, null));
        }

        [TestMethod]
        public void TestRecentSeniority()
        {
            var member = CreateMember(null, Seniority.EntryLevel);
            TestCandidateUrls(member, () => AssertRecentProfession(null, Seniority.EntryLevel));
        }

        [TestMethod]
        public void TestBoth()
        {
            var member = CreateMember(Profession.QualityAssurance, Seniority.EntryLevel);
            TestCandidateUrls(member, () => AssertRecentProfession(Profession.QualityAssurance, Seniority.EntryLevel));
        }

        private Member CreateMember(Profession? profession, Seniority? seniority)
        {
            var member = CreateMember(0);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            candidate.RecentProfession = profession;
            candidate.RecentSeniority = seniority;
            _candidatesCommand.UpdateCandidate(candidate);
            return member;
        }

        private void AssertRecentProfession(Profession? profession, Seniority? seniority)
        {
            AssertRecentProfession(profession, seniority, "Summary");
            AssertRecentProfession(profession, seniority, "Employment");
        }

        private void AssertRecentProfession(Profession? profession, Seniority? seniority, string tab)
        {
            var nodeList = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@id='" + tab + "-details']//div[@class='employment-history_section resume_section']//table[@class='profession']//span[@class='description']");
            Assert.IsNotNull(nodeList);

            if (profession == null)
                Assert.AreEqual("N/A", nodeList[0].InnerText.Trim());
            else
                Assert.AreEqual(GetDisplayText(profession.Value), nodeList[0].InnerText.Trim());
            if (seniority == null)
                Assert.AreEqual("N/A", nodeList[1].InnerText.Trim());
            else
                Assert.AreEqual(GetDisplayText(seniority.Value), nodeList[1].InnerText.Trim());
        }

        private static string GetDisplayText(Seniority seniority)
        {
            switch (seniority)
            {
                case Seniority.EntryLevel:
                    return "Entry level";

                default:
                    Assert.Fail();
                    return null;
            }
        }

        private static string GetDisplayText(Profession profession)
        {
            switch (profession)
            {
                case Profession.QualityAssurance:
                    return "Quality assurance";

                default:
                    Assert.Fail();
                    return null;
            }
        }
    }
}
