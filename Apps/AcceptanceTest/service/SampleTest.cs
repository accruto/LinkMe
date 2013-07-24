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

namespace LinkMe.AcceptanceTest.service
{
    /// <summary>
    /// This test case is here for testing weird behaviour of XmlSerializer,
    /// which accedentialy messes up order of XML elements when serializing objects (such as JobAd)
    /// To prevent that from happening, [XmlElement] attributes were introduced in JobAd class
    /// </summary>
    [TestClass]
    public class ASampleTest
        : WebTestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IEmployerCreditsQuery _employerCreditsQuery = Resolve<IEmployerCreditsQuery>();
        private readonly IMemberJobAdViewsQuery _memberJobAdViewsQuery = Resolve<IMemberJobAdViewsQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void Test()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer("employer", _organisationsCommand.CreateTestOrganisation(0));

            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var view = _memberJobAdViewsQuery.GetMemberJobAdView(null, jobAd.Id);

            var viewJobAdUrl = new ReadOnlyApplicationUrl("~/", new ReadOnlyQueryString("jobAdId", jobAd.Id.ToString()));
            var applyJobAdUrl = new ReadOnlyApplicationUrl("~/", new ReadOnlyQueryString("jobAdId", jobAd.Id.ToString()));
            var credits = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);

            CheckInstances(view.Map(viewJobAdUrl.AbsoluteUri, applyJobAdUrl.AbsoluteUri, credits.RemainingQuantity > 0), employer, jobAd, viewJobAdUrl.AbsoluteUri.ToLower(), applyJobAdUrl.AbsoluteUri.ToLower());
        }

        private static void CheckInstances(JobAdFeedElement jaF, IEmployer employer, JobAd jobAd, string viewJobAdUrl, string applyJobAdUrl)
        {
            Assert.AreEqual(jaF.ViewJobAdUrl, viewJobAdUrl);
            Assert.AreEqual(jaF.ApplyJobAdUrl, applyJobAdUrl);
            Assert.AreEqual(jaF.Id, jobAd.Id);
            Assert.AreEqual(jaF.RecruiterCompanyName, employer.Organisation.Name);
            Assert.AreEqual(jaF.EmployerCompanyName, jobAd.Description.CompanyName);
            Assert.AreEqual(jaF.ContactDetails.FirstName, jobAd.ContactDetails.FirstName);
            Assert.AreEqual(jaF.ContactDetails.LastName, jobAd.ContactDetails.LastName);
            Assert.AreEqual(jaF.ContactDetails.EmailAddress, jobAd.ContactDetails.EmailAddress);
            Assert.AreEqual(jaF.ContactDetails.SecondaryEmailAddresses, jobAd.ContactDetails.SecondaryEmailAddresses);
            Assert.AreEqual(jaF.ContactDetails.PhoneNumber, jobAd.ContactDetails.PhoneNumber);
            Assert.AreEqual(jaF.ContactDetails.FaxNumber, jobAd.ContactDetails.FaxNumber);
            Assert.AreEqual(jaF.ExternalReferenceId, jobAd.Integration.ExternalReferenceId);
            Assert.AreEqual(jaF.ExternalApplyUrl, jobAd.Integration.ExternalApplyUrl);
            Assert.AreEqual(jaF.Title, jobAd.Title);
            Assert.AreEqual(jaF.PositionTitle, jobAd.Description.PositionTitle);
            Assert.AreEqual(jaF.Summary, jobAd.Description.Summary);
            Assert.AreEqual(jaF.Content, jobAd.Description.Content);
            Assert.AreEqual(jaF.JobTypes, jobAd.Description.JobTypes);
            Assert.AreEqual(jaF.Location, jobAd.Description.Location.ToString());
            Assert.AreEqual(jaF.Postcode, jobAd.Description.Location.Postcode);
            Assert.AreEqual(jaF.Salary, jobAd.Description.Salary);
            Assert.AreEqual(jaF.PackageDetails, jobAd.Description.Package);
            Assert.AreEqual(jaF.ResidencyRequired, jobAd.Description.ResidencyRequired);
            Assert.IsTrue(jaF.Industries.ToArray().NullableCollectionEqual((from i in jobAd.Description.Industries select i.Name).ToArray()));
            Assert.IsTrue(jaF.BulletPoints.NullableSequenceEqual(jobAd.Description.BulletPoints));
        }
    }
}
