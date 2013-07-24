using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Views
{
    [TestClass]
    public class VisaStatusTests
        : ViewsTests
    {
        [TestMethod]
        public void TestNone()
        {
            var member = CreateMember(null);
            TestCandidateUrls(member, () => AssertVisaStatus(null));
        }

        [TestMethod]
        public void TestVisaStatus()
        {
            var member = CreateMember(VisaStatus.RestrictedWorkVisa);
            TestCandidateUrls(member, () => AssertVisaStatus(VisaStatus.RestrictedWorkVisa));
        }

        private Member CreateMember(VisaStatus? visaStatus)
        {
            var member = base.CreateMember(0);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            candidate.VisaStatus = visaStatus;
            _candidatesCommand.UpdateCandidate(candidate);
            return member;
        }

        private void AssertVisaStatus(VisaStatus? visaStatus)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='Personal-details']//div[@class='personal_section resume_section']//table[@class='personal']//tr[position()=6]//span[@class='description']");
            Assert.IsNotNull(node);
            Assert.AreEqual(visaStatus == null ? "N/A" : GetDisplayText(visaStatus.Value), node.InnerText.Trim());
        }

        private static string GetDisplayText(VisaStatus visaStatus)
        {
            switch (visaStatus)
            {
                case VisaStatus.RestrictedWorkVisa:
                    return "I am currently in possession of a restricted Australian work visa";

                default:
                    Assert.Fail();
                    return null;
            }
        }
    }
}
