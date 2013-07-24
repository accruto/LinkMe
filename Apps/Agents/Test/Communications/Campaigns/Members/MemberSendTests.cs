using LinkMe.Apps.Agents.Communications.Campaigns.Emails;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Campaigns.Members
{
    [TestClass]
    public class MemberSendTests
        : CategorySendTests
    {
        [TestMethod]
        public void SendEmailTest()
        {
            SendEmail();
        }

        [TestMethod]
        public void SendTestEmailTwiceTest()
        {
            SendTestEmailTwice();
        }

        [TestMethod]
        public void SendNonTestEmailTwiceTest()
        {
            SendNonTestEmailTwice();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            CampaignsTestHelper.TestInitialize();
        }

        protected override CampaignEmail CreateEmail(Campaign campaign, ICommunicationUser to)
        {
            return CampaignsTestHelper.CreateEmail(campaign, to);
        }

        protected override string GetBody(CampaignEmail email, string content)
        {
            return CampaignsTestHelper.GetBody(email, content);
        }

        protected override CampaignCategory GetCampaignCategory()
        {
            return CampaignsTestHelper.GetCampaignCategory();
        }
    }
}