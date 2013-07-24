using System.Net;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join
{
    [TestClass]
    public class JoinParameterTests
        : JoinTests
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();

        [TestMethod]
        public void TestPromotionCode()
        {
            // Attach promo code to join url.

            const string pcode = "chucknorris";
            var url = GetJoinUrl().AsNonReadOnly();
            url.QueryString["pcode"] = pcode;
            Get(url);

            SubmitJoin();
            var instanceId = GetInstanceId();

            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            SubmitPersonalDetails(instanceId, member, candidate, Password);

            // Promotion code should have been saved.

            AssertAffiliationReferral(pcode, _loginCredentialsQuery.GetUserId(member.GetBestEmailAddress().Address).Value);
        }

        [TestMethod]
        public void TestJobPromotionCode()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Get the job url.

            Get(new ReadOnlyApplicationUrl("~/jobs/" + jobAd.Id));
            var url = new Url(Browser.CurrentUrl);

            // Attach promo code to job url.

            const string pcode = "chucknorris";
            url.QueryString["pcode"] = pcode;

            Browser.Cookies = new CookieContainer();
            Get(url);

            // Join.

            Get(GetJoinUrl());
            SubmitJoin();
            var instanceId = GetInstanceId();

            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            SubmitPersonalDetails(instanceId, member, candidate, Password);

            // Promotion code should have been saved.

            AssertAffiliationReferral(pcode, _loginCredentialsQuery.GetUserId(member.GetBestEmailAddress().Address).Value);
        }

        [TestMethod]
        public void TestJSeekerJobPromotionCode()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Get the job url.

            Get(new ReadOnlyApplicationUrl("~/jobs/" + jobAd.Id));
            var url = new Url(Browser.CurrentUrl);

            // Attach promo code to job url. JSeeker did not set urls right and simply appended to the url instead of using the proper query string.

            const string pcode = "jobjseek";
            url = new Url(url.AbsoluteUri + "&pcode=" + pcode);

            Browser.Cookies = new CookieContainer();
            Get(url);

            // Join.

            Get(GetJoinUrl());
            SubmitJoin();
            var instanceId = GetInstanceId();

            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            SubmitPersonalDetails(instanceId, member, candidate, Password);

            // Promotion code should have been saved.

            AssertAffiliationReferral(pcode, _loginCredentialsQuery.GetUserId(member.GetBestEmailAddress().Address).Value);
        }
    }
}
