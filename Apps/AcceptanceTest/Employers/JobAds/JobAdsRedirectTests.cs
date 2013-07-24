using LinkMe.AcceptanceTest.Navigation;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds
{
    [TestClass]
    public class JobAdsRedirectTests
        : RedirectTests
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        [TestMethod]
        public void TestNewJobAd()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            LogIn(employer);

            var url = new ReadOnlyApplicationUrl(true, "~/ui/registered/employers/EmployerNewJobAd.aspx");
            var redirectUrl = new ReadOnlyApplicationUrl(true, "~/employers/jobads/jobad");
            AssertRedirect(url, redirectUrl, redirectUrl);
        }
    }
}
