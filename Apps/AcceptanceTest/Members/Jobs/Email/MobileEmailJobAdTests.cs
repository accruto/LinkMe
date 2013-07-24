using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Communications;
using LinkMe.Web.Areas.Members.Models.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Email
{
    [TestClass]
    public class MobileEmailJobAdTests
        : EmailJobAdsTests
    {
        private const string FromName = "Homer Simpson";
        private const string FromEmailAddress = "from@test.linkme.net.au";
        private const string ToName = "Monty Burns";
        private const string ToEmailAddress = "to@test.linkme.net.au";

        [TestInitialize]
        public void TestInitialize()
        {
            Browser.UseMobileUserAgent = true;
        }

        [TestMethod]
        public void TestSendEmail()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Get the page.

            Get(GetEmailUrl(jobAd.Id));
            AssertJobAdEmail();

            // Send the email (simulating the JavaScript).

            var model = new EmailJobAdsModel
            {
                FromName = FromName,
                FromEmailAddress = FromEmailAddress,
                Tos = new[] { new EmailToModel { ToName = ToName, ToEmailAddress = ToEmailAddress } },
                JobAdIds = new List<Guid> { jobAd.Id }
            };
            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(null), JsonContentType, Serialize(model))));

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(FromEmailAddress, FromName), Return, new EmailRecipient(ToEmailAddress, ToName));

            // Simulate getting the success notification.

            Get(GetEmailSentUrl(jobAd.Id));
            AssertPageContains("job was emailed successfully.");
        }

        private void AssertJobAdEmail()
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='button send']");
            Assert.IsNotNull(node);
            node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//a[@class='button cancel']");
            Assert.IsNotNull(node);
        }
    }
}
