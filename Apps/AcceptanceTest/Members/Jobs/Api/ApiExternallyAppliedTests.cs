using System;
using System.Net;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Services.External.CareerOne.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Api
{
    [TestClass]
    public class ApiExternallyAppliedTests
        : WebTestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly ICareerOneQuery _careerOneQuery = Resolve<ICareerOneQuery>();

        [TestMethod]
        public void TestGetNotFound()
        {
            AssertJsonError(Deserialize<JsonResponseModel>(Get(HttpStatusCode.NotFound, GetExternallyAppliedUrl(Guid.NewGuid()))), null, "400", "The page cannot be found.");
        }

        [TestMethod]
        public void TestExternalJobAd()
        {
            var employer = CreateEmployer();
            var jobAd = employer.CreateTestJobAd();
            jobAd.Integration.IntegratorUserId = _careerOneQuery.GetIntegratorUser().Id;
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);

            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(GetExternallyAppliedUrl(jobAd.Id))));
        }

        [TestMethod]
        public void TestUnknownJobAd()
        {
            var jobAdId = Guid.NewGuid();
            AssertJsonError(Deserialize<JsonResponseModel>(Post(GetExternallyAppliedUrl(jobAdId))), "JobAdId", "300", "The job ad id with value '" + jobAdId + "' cannot be found.");
        }

        [TestMethod]
        public void TestNonExternalJobAd()
        {
            var employer = CreateEmployer();
            var jobAd = employer.CreateTestJobAd();
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);

            AssertJsonError(Deserialize<JsonResponseModel>(Post(GetExternallyAppliedUrl(jobAd.Id))), "IntegratorUserId", "300", "Integrator user id is required.");
        }

        private Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        private static ReadOnlyUrl GetExternallyAppliedUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl("~/members/jobs/api/" + jobAdId + "/externallyapplied");
        }
    }
}