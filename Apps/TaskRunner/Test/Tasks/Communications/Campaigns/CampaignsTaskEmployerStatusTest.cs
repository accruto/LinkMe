using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications.Campaigns
{
    [TestClass]
    public class CampaignsTaskEmployerStatusTest
        : CampaignsTaskTests
    {
        [TestMethod]
        public void TestNoCampaigns()
        {
            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestNotActivatedCampaign()
        {
            // Create employers.

            CreateEmployers(0, 1, EmployerSubRole.Employer);

            // Create a campaign.

            Campaign campaign;
            Template template;
            CreateCampaign(1, CampaignCategory.Employer, null, CampaignStatus.Draft, out campaign, out template);
            CreateCampaign(2, CampaignCategory.Employer, null, CampaignStatus.Deleted, out campaign, out template);
            CreateCampaign(3, CampaignCategory.Employer, null, CampaignStatus.Running, out campaign, out template);
            CreateCampaign(4, CampaignCategory.Employer, null, CampaignStatus.Stopped, out campaign, out template);

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestActivatedCampaign()
        {
            // Create employers.

            var employers = CreateEmployers(0, 1, EmployerSubRole.Employer);

            // Create a campaign.

            Campaign campaign;
            Template template;
            CreateCampaign(1, CampaignCategory.Employer, null, CampaignStatus.Activated, out campaign, out template);
            Assert.AreEqual(CampaignStatus.Activated, campaign.Status);

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(1);
            AssertEmails(employers, emails);

            // Should now be stopped.

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();

            campaign = _campaignsQuery.GetCampaign(campaign.Id);
            Assert.AreEqual(CampaignStatus.Stopped, campaign.Status);
        }

        [TestMethod]
        public void TestReactivatedCampaign()
        {
            // Create employers.

            var employers = CreateEmployers(0, 1, EmployerSubRole.Employer);

            // Create a campaign.

            Campaign campaign;
            Template template;
            CreateCampaign(1, CampaignCategory.Employer, null, CampaignStatus.Activated, out campaign, out template);
            Assert.AreEqual(CampaignStatus.Activated, campaign.Status);

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(1);
            AssertEmails(employers, emails);

            // Should now be stopped.

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();

            // Reactivate.

            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);

            // Employer should not get it again.

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();

            // Reactivate and add new employer.

            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            employers = CreateEmployers(1, 1, EmployerSubRole.Employer);

            new CampaignsTask().ExecuteTask();
            emails = _emailServer.AssertEmailsSent(1);
            AssertEmails(employers, emails);
        }

        [TestMethod]
        public void TestMultipleCampaigns()
        {
            // Create employers.

            var employers1 = CreateEmployers(0, 1, EmployerSubRole.Employer);

            // Create campaigns.

            Campaign campaign1;
            Template template1;
            CreateCampaign(1, CampaignCategory.Employer, null, CampaignStatus.Activated, out campaign1, out template1);

            Campaign campaign2;
            Template template2;
            CreateCampaign(2, CampaignCategory.Employer, null, CampaignStatus.Activated, out campaign2, out template2);

            Campaign campaign3;
            Template template3;
            CreateCampaign(3, CampaignCategory.Employer, null, CampaignStatus.Activated, out campaign3, out template3);

            Campaign campaign4;
            Template template4;
            CreateCampaign(4, CampaignCategory.Employer, null, CampaignStatus.Draft, out campaign4, out template4);

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(3);

            AssertEmails(employers1, emails, new [] {campaign1, campaign2, campaign3}, new[] {template1, template2, template3});

            // Should now be stopped.

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();

            // Add some more employers.

            var employers2 = CreateEmployers(1, 2, EmployerSubRole.Employer);
            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();

            // Reactivate.

            _campaignsCommand.UpdateStatus(campaign1, CampaignStatus.Activated);
            _campaignsCommand.UpdateStatus(campaign2, CampaignStatus.Activated);
            _campaignsCommand.UpdateStatus(campaign3, CampaignStatus.Activated);

            new CampaignsTask().ExecuteTask();
            emails = _emailServer.AssertEmailsSent(6);
            AssertEmails(employers2, emails, new[] { campaign1, campaign2, campaign3 }, new[] { template1, template2, template3 });

            // Add some more campaigns.

            Campaign campaign5;
            Template template5;
            CreateCampaign(5, CampaignCategory.Employer, null, CampaignStatus.Draft, out campaign5, out template5);

            Campaign campaign6;
            Template template6;
            CreateCampaign(6, CampaignCategory.Employer, null, CampaignStatus.Activated, out campaign6, out template6);

            Campaign campaign7;
            Template template7;
            CreateCampaign(7, CampaignCategory.Employer, null, CampaignStatus.Activated, out campaign7, out template7);

            new CampaignsTask().ExecuteTask();
            emails = _emailServer.AssertEmailsSent(6);
            AssertEmails(employers1.Concat(employers2).ToList(), emails, new[] { campaign6, campaign7 }, new[] { template6, template7 });

            new CampaignsTask().ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        private static void AssertEmails(ICollection<Employer> employers, ICollection<MockEmail> emails)
        {
            Assert.AreEqual(employers.Count, emails.Count);
            foreach (var employer in employers)
                Assert.IsTrue((from e in emails where e.To[0].Address == employer.EmailAddress.Address select e).Any());
            foreach (var email in emails)
                Assert.IsTrue((from e in employers where e.EmailAddress.Address == email.To[0].Address select e).Any());
        }

        private static void AssertEmails(ICollection<Employer> employers, ICollection<MockEmail> emails, ICollection<Campaign> campaigns, IEnumerable<Template> templates)
        {
            // Each employer should have received an email for each campaign.

            Assert.AreEqual(employers.Count * campaigns.Count, emails.Count);
            foreach (var employer in employers)
            {
                foreach (var template in templates)
                    Assert.IsTrue((from e in emails where e.To[0].Address == employer.EmailAddress.Address && e.GetHtmlView().Body.Contains(template.Body) select e).Any());
            }
            foreach (var email in emails)
            {
                Assert.IsTrue((from e in employers where e.EmailAddress.Address == email.To[0].Address select e).Any()
                    && ((from t in templates where email.GetHtmlView().Body.Contains(t.Body) select t).Any()));
            }
        }
    }
}