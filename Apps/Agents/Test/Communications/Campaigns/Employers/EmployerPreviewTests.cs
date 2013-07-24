using LinkMe.Apps.Agents.Communications.Campaigns.Emails;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Campaigns.Employers
{
    [TestClass]
    public class EmployerPreviewTests
        : CategoryPreviewTests
    {
        [TestMethod]
        public void TestSimplePreview()
        {
            const string subject = "This is the subject";
            const string body = "This is the body";
            TestPreview(subject, body);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            CampaignsTestHelper.TestInitialize();
        }

        protected override string GetBody(CampaignEmail email, string content)
        {
            return CampaignsTestHelper.GetBody(email, content);
        }

        protected override CampaignCategory GetCampaignCategory()
        {
            return CampaignsTestHelper.GetCampaignCategory();
        }

        protected override CampaignEmail CreateEmail(Campaign campaign, ICommunicationUser to)
        {
            return CampaignsTestHelper.CreateEmail(campaign, to);
        }
    }
}