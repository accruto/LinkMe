using System.Linq;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.JobAds
{
    [TestClass]
    public class JobAdReflectionTest
        : TestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IMemberJobAdViewsQuery _memberJobAdViewsQuery = Resolve<IMemberJobAdViewsQuery>();
        private readonly IEmployerCreditsQuery _employerCreditsQuery = Resolve<IEmployerCreditsQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestJobAdFeedCreation()
        {
            const string testLoginId = "employer";

            var employer = _employerAccountsCommand.CreateTestEmployer(testLoginId, _organisationsCommand.CreateTestOrganisation(0));
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var view = _memberJobAdViewsQuery.GetMemberJobAdView(null, jobAd.Id);

            var viewJobAdUrl = string.Format("{0}jobs/Job.aspx?jobAdId={1}", new ReadOnlyApplicationUrl("~/").AbsoluteUri, jobAd.Id);
            var applyJobAdUrl = string.Format("{0}ui/unregistered/common/JobApplicationSignInForm.aspx?jobAdId={1}", new ReadOnlyApplicationUrl("~/").AbsoluteUri, jobAd.Id);
            var credits = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);

            AssertJobAdFeed(view.Map(viewJobAdUrl, applyJobAdUrl, credits.RemainingQuantity > 0), jobAd, employer, viewJobAdUrl.ToLower(), applyJobAdUrl.ToLower());
        }

        private static void AssertJobAdFeed(JobAdFeedElement jobAdFeed, JobAd jobAd, IEmployer employer, string viewJobAdUrl, string applyJobAdUrl)
        {
            Assert.AreEqual(jobAdFeed.ViewJobAdUrl, viewJobAdUrl);
            Assert.AreEqual(jobAdFeed.ApplyJobAdUrl, applyJobAdUrl);
            Assert.AreEqual(jobAdFeed.Id, jobAd.Id);
            Assert.AreEqual(jobAdFeed.RecruiterCompanyName, employer.Organisation.Name);
            Assert.AreEqual(jobAdFeed.EmployerCompanyName, jobAd.Description.CompanyName);
            Assert.AreEqual(jobAdFeed.ContactDetails.FirstName, jobAd.ContactDetails.FirstName);
            Assert.AreEqual(jobAdFeed.ContactDetails.LastName, jobAd.ContactDetails.LastName);
            Assert.AreEqual(jobAdFeed.ContactDetails.EmailAddress, jobAd.ContactDetails.EmailAddress);
            Assert.AreEqual(jobAdFeed.ContactDetails.SecondaryEmailAddresses, jobAd.ContactDetails.SecondaryEmailAddresses);
            Assert.AreEqual(jobAdFeed.ContactDetails.PhoneNumber, jobAd.ContactDetails.PhoneNumber);
            Assert.AreEqual(jobAdFeed.ContactDetails.FaxNumber, jobAd.ContactDetails.FaxNumber);
            Assert.AreEqual(jobAdFeed.ExternalReferenceId, jobAd.Integration.ExternalReferenceId);
            Assert.AreEqual(jobAdFeed.ExternalApplyUrl, jobAd.Integration.ExternalApplyUrl);
            Assert.AreEqual(jobAdFeed.Title, jobAd.Title);
            Assert.AreEqual(jobAdFeed.PositionTitle, jobAd.Description.PositionTitle);
            Assert.AreEqual(jobAdFeed.Summary, jobAd.Description.Summary);
            Assert.AreEqual(jobAdFeed.Content, jobAd.Description.Content);
            Assert.AreEqual(jobAdFeed.JobTypes, jobAd.Description.JobTypes);
            Assert.AreEqual(jobAdFeed.Location, jobAd.Description.Location.ToString());
            Assert.AreEqual(jobAdFeed.Postcode, jobAd.Description.Location.Postcode);
            Assert.AreEqual(jobAdFeed.Salary, jobAd.Description.Salary);
            Assert.AreEqual(jobAdFeed.PackageDetails, jobAd.Description.Package);
            Assert.AreEqual(jobAdFeed.ResidencyRequired, jobAd.Description.ResidencyRequired);
            Assert.IsTrue(jobAdFeed.Industries.ToArray().NullableCollectionEqual((from i in jobAd.Description.Industries select i.Name).ToArray()));
            Assert.IsTrue(jobAdFeed.BulletPoints.NullableSequenceEqual(jobAd.Description.BulletPoints));
        }
    }
}