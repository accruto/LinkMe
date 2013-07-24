using System;
using System.Collections.Generic;
using System.Net;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Communications;
using LinkMe.Web.Areas.Members.Models.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Email
{
    [TestClass]
    public class ApiEmailJobAdsTests
        : EmailJobAdsTests
    {
        private const string FromName = "Homer Simpson";
        private const string FromEmailAddress = "from@test.linkme.net.au";
        private const string ToName = "Monty Burns";
        private const string ToEmailAddress = "to@test.linkme.net.au";
        private const string ToName2 = "Waylon Smithers";
        private const string ToEmailAddress2 = "to2@test.linkme.net.au";
        private const string BadEmailAddress = "notanemailaddress";
        private const string UnknownEmailAddress = "aaa@mail.linkme.net.au";

        [TestMethod]
        public void TestUnknownJobAd()
        {
            var model = new EmailJobAdsModel
            {
                FromName = FromName,
                FromEmailAddress = FromEmailAddress,
                Tos = new[] {new EmailToModel {ToName = ToName, ToEmailAddress = ToEmailAddress}},
                JobAdIds = new List<Guid>{Guid.NewGuid()}
            };
            AssertJsonError(Deserialize<JsonResponseModel>(Post(HttpStatusCode.NotFound, GetApiEmailUrl(null), JsonContentType, Serialize(model))), null, "400", "The job ad cannot be found.");
        }

        [TestMethod]
        public void TestFromNameErrors()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Not logged in required.

            var model = new EmailJobAdsModel { FromEmailAddress = FromEmailAddress, Tos = new[] { new EmailToModel { ToName = ToName, ToEmailAddress = ToEmailAddress } } };
            AssertJsonError(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(jobAd.Id), JsonContentType, Serialize(model))), "FromName", "Your name is required.");

            // Logged in, cannot be changed.

            var member = CreateMember();
            LogIn(member);

            model = new EmailJobAdsModel { Tos = new[] { new EmailToModel { ToName = ToName, ToEmailAddress = ToEmailAddress } } };
            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(jobAd.Id), JsonContentType, Serialize(model))));
        }

        [TestMethod]
        public void TestFromEmailAddressErrors()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Not logged in required.

            var model = new EmailJobAdsModel { FromName = FromName, Tos = new[] { new EmailToModel { ToName = ToName, ToEmailAddress = ToEmailAddress } } };
            AssertJsonError(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(jobAd.Id), JsonContentType, Serialize(model))), "FromEmailAddress", "Your email address is required.");

            // Bad email address.

            model.FromEmailAddress = BadEmailAddress;
            AssertJsonError(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(jobAd.Id), JsonContentType, Serialize(model))), "FromEmailAddress", "Your email address must be valid and have less than 320 characters.");

            model.FromEmailAddress = UnknownEmailAddress;
            AssertJsonError(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(jobAd.Id), JsonContentType, Serialize(model))), "FromEmailAddress", "Your email address is not recognised.");

            // Logged in, cannot be changed.

            var member = CreateMember();
            LogIn(member);

            model = new EmailJobAdsModel { Tos = new[] { new EmailToModel { ToName = ToName, ToEmailAddress = ToEmailAddress } } };
            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(jobAd.Id), JsonContentType, Serialize(model))));
        }

        [TestMethod]
        public void TestToNameErrors()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Not logged in.

            TestToNameErrors(jobAd.Id);

            // Logged in.

            var member = CreateMember();
            LogIn(member);
            TestToNameErrors(jobAd.Id);
        }

        [TestMethod]
        public void TestToEmailAddressErrors()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Not logged in.

            TestToEmailAddressErrors(jobAd.Id);

            // Logged in.

            var member = CreateMember();
            LogIn(member);
            TestToEmailAddressErrors(jobAd.Id);
        }

        [TestMethod]
        public void TestSingleRecipient()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var model = new EmailJobAdsModel
            {
                FromName = FromName,
                FromEmailAddress = FromEmailAddress,
                Tos = new[] {new EmailToModel {ToName = ToName, ToEmailAddress = ToEmailAddress}},
                JobAdIds = new List<Guid>{jobAd.Id}
            };
            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(null), JsonContentType, Serialize(model))));

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(FromEmailAddress, FromName), Return, new EmailRecipient(ToEmailAddress, ToName));
        }

        [TestMethod]
        public void TestMultipleRecipients()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var model = new EmailJobAdsModel
            {
                FromName = FromName,
                FromEmailAddress = FromEmailAddress,
                Tos = new[]
                {
                    new EmailToModel { ToName = ToName, ToEmailAddress = ToEmailAddress },
                    new EmailToModel { ToName = ToName2, ToEmailAddress = ToEmailAddress2 }
                },
                JobAdIds = new List<Guid>{jobAd.Id},
            };
            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(null), JsonContentType, Serialize(model))));

            var emails = _emailServer.AssertEmailsSent(2);
            emails[0].AssertAddresses(new EmailRecipient(FromEmailAddress, FromName), Return, new EmailRecipient(ToEmailAddress, ToName));
            emails[1].AssertAddresses(new EmailRecipient(FromEmailAddress, FromName), Return, new EmailRecipient(ToEmailAddress2, ToName2));
        }

        [TestMethod]
        public void TestSingleRecipientLoggedIn()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var member = CreateMember();
            LogIn(member);

            var model = new EmailJobAdsModel
            {
                Tos = new[] {new EmailToModel {ToName = ToName, ToEmailAddress = ToEmailAddress}},
                JobAdIds = new List<Guid> {jobAd.Id}
            };
            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(null), JsonContentType, Serialize(model))));

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(member.EmailAddresses[0].Address, member.FullName), Return, new EmailRecipient(ToEmailAddress, ToName));
        }

        [TestMethod]
        public void TestMultipleRecipientsLoggedIn()
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            var member = CreateMember();
            LogIn(member);

            var model = new EmailJobAdsModel
            {
                Tos = new[]
                {
                    new EmailToModel { ToName = ToName, ToEmailAddress = ToEmailAddress },
                    new EmailToModel { ToName = ToName2, ToEmailAddress = ToEmailAddress2 }
                },
                JobAdIds = new List<Guid>{jobAd.Id},
            };
            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(null), JsonContentType, Serialize(model))));

            var emails = _emailServer.AssertEmailsSent(2);
            emails[0].AssertAddresses(new EmailRecipient(member.EmailAddresses[0].Address, member.FullName), Return, new EmailRecipient(ToEmailAddress, ToName));
            emails[1].AssertAddresses(new EmailRecipient(member.EmailAddresses[0].Address, member.FullName), Return, new EmailRecipient(ToEmailAddress2, ToName2));
        }

        private void TestToNameErrors(Guid jobAdId)
        {
            // Required.

            var model = new EmailJobAdsModel { FromName = FromName, FromEmailAddress = FromEmailAddress };
            AssertJsonErrors(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(jobAdId), JsonContentType, Serialize(model))), "Tos", "Your friend's email address is required.", "Your friend's name is required.");

            model.Tos = new[] { new EmailToModel { ToEmailAddress = ToEmailAddress } };
            AssertJsonError(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(jobAdId), JsonContentType, Serialize(model))), "ToName", "Your friend's name is required.");

            model.Tos = new[] { new EmailToModel { ToName = ToName, ToEmailAddress = ToEmailAddress }, new EmailToModel { ToEmailAddress = ToEmailAddress2 } };
            AssertJsonError(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(jobAdId), JsonContentType, Serialize(model))), "ToName", "Your friend's name is required.");
        }

        private void TestToEmailAddressErrors(Guid jobAdId)
        {
            // Required.

            var model = new EmailJobAdsModel { FromName = FromName, FromEmailAddress = FromEmailAddress };
            AssertJsonErrors(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(jobAdId), JsonContentType, Serialize(model))), "Tos", "Your friend's email address is required.", "Your friend's name is required.");

            model.Tos = new[] { new EmailToModel { ToName = ToName } };
            AssertJsonError(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(jobAdId), JsonContentType, Serialize(model))), "ToEmailAddress", "Your friend's email address is required.");

            model.Tos = new[] { new EmailToModel { ToName = ToName, ToEmailAddress = ToEmailAddress }, new EmailToModel { ToName = ToName2 } };
            AssertJsonError(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(jobAdId), JsonContentType, Serialize(model))), "ToEmailAddress", "Your friend's email address is required.");

            // Bad email address.

            model.Tos = new[] { new EmailToModel { ToName = ToName, ToEmailAddress = BadEmailAddress } };
            AssertJsonError(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(jobAdId), JsonContentType, Serialize(model))), "ToEmailAddress", "Your friend's email address must be valid and have less than 320 characters.");

            model.Tos = new[] { new EmailToModel { ToName = ToName, ToEmailAddress = ToEmailAddress }, new EmailToModel { ToName = ToName2, ToEmailAddress = BadEmailAddress } };
            AssertJsonError(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(jobAdId), JsonContentType, Serialize(model))), "ToEmailAddress", "Your friend's email address must be valid and have less than 320 characters.");

            model.Tos = new[] { new EmailToModel { ToName = ToName, ToEmailAddress = UnknownEmailAddress } };
            AssertJsonError(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(jobAdId), JsonContentType, Serialize(model))), "ToEmailAddress", "Your friend's email address is not recognised.");

            model.Tos = new[] { new EmailToModel { ToName = ToName, ToEmailAddress = ToEmailAddress }, new EmailToModel { ToName = ToName2, ToEmailAddress = UnknownEmailAddress } };
            AssertJsonError(Deserialize<JsonResponseModel>(Post(GetApiEmailUrl(jobAdId), JsonContentType, Serialize(model))), "ToEmailAddress", "Your friend's email address is not recognised.");
        }
    }
}