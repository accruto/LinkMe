using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Accounts.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds.PostJobAds
{
    [TestClass]
    public class PostJobAdErrorsTests
        : PostJobAdsTests
    {
        private readonly IUserAccountsCommand _userAccountsCommand = Resolve<IUserAccountsCommand>();

        [TestMethod]
        public void TestDisabledEmployer()
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = CreateEmployer(0);

            // Disable the employer.

            _userAccountsCommand.DisableUserAccount(employer, Guid.NewGuid());

            // Submit a new job ad.

            var jobAd1 = CreateJobAd(1);
            PostJobAdsFailure(integratorUser, employer, true, new[] { jobAd1 }, "Job poster user '" + employer.GetLoginId() + "' is disabled.");
        }
    }
}
