using LinkMe.Apps.Agents.Communications.Campaigns.Emails;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Campaigns.Employers
{
    [TestClass]
    public class EmployerContentTests
        : CategoryContentTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            CampaignsTestHelper.TestInitialize();
        }

        [TestMethod]
        public void TestSendSimpleEmail()
        {
            TestContent("Test subject", "Test body");
        }

        [TestMethod]
        public void TestSendHtmlEmail()
        {
            TestContent("Test subject", "<p>Some HTML</p>");
        }

        [TestMethod]
        public void TestSendSubstitutedSubject()
        {
            const string templateSubject = "Look out <%=To.FirstName%>";
            const string expectedSubject = "Look out " + FirstName;
            const string body = "This is the body";
            TestContent(templateSubject, body, expectedSubject, body);
        }

        [TestMethod]
        public void TestSendSubstitutedBody()
        {
            const string templateBody = @"
                <p>
                    Hi <%=To.FirstName%>,
                </p>
                <p>
                    Hope it is going well <%=To.FullName%>.
                </p>";

            const string expectedBody = @"
                <p>
                    Hi " + FirstName + @",
                </p>
                <p>
                    Hope it is going well " + FirstName + " " + LastName + @".
                </p>";

            TestContent("Test subject", templateBody, expectedBody);
        }

        protected override CampaignEmail CreateEmail(Campaign campaign, ICommunicationUser to)
        {
            return CampaignsTestHelper.CreateEmail(campaign, to);
        }

        protected override CampaignCategory GetCampaignCategory()
        {
            return CampaignsTestHelper.GetCampaignCategory();
        }

        protected override string GetBody(CampaignEmail email, string content)
        {
            return CampaignsTestHelper.GetBody(email, content);
        }
    }
}