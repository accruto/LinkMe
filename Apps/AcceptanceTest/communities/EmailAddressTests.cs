using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Workflow.Components.PeriodicWorkflow.SuggestedJobs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities
{
    [TestClass]
    public class EmailAddressTests
        : CommunityTests
    {
        private readonly IEmailsCommand _emailsCommand = Resolve<IEmailsCommand>();
            
        private const string OtdReturnEmailAddress = "enquiries@oztraveldirect.com";
        private const string OtdReturnDisplayName = "Oz Travel Direct";
        private readonly static EmailRecipient OtdReturnAddress = new EmailRecipient(OtdReturnEmailAddress, OtdReturnDisplayName);

        [TestInitialize]
        public void TestInitialize()
        {
            _emailServer.ClearEmails();
        }

        [TestMethod]
        public void TestSuggestedJobsEmail()
        {
            var data = TestCommunity.Otd.GetCommunityTestData();
            var community = data.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Create a member.

            var member = _memberAccountsCommand.CreateTestMember(0, community.Id);

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAds = new JobAd[3];
            for (var index = 0; index < jobAds.Length; ++index)
                jobAds[index] = _jobAdsCommand.PostTestJobAd(employer);

            _emailsCommand.TrySend(SuggestedJobsEmail.Create(member, jobAds, jobAds.Length));

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(OtdReturnAddress, OtdReturnAddress, member);
        }
    }
}
