using System;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Integration.JobG8;
using LinkMe.Apps.Services.JobAds.Commands;
using LinkMe.Apps.Services.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Integration.Test.JobG8
{
    [TestClass]
    public abstract class AdvertPostServiceTests
        : TestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();
        private readonly IServiceAuthenticationManager _serviceAuthenticationManager = Resolve<IServiceAuthenticationManager>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        protected readonly IJobAdIntegrationQuery _jobAdIntegrationQuery = Resolve<IJobAdIntegrationQuery>();
        private readonly IExternalJobAdsCommand _externalJobAdsCommand = Resolve<IExternalJobAdsCommand>();
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        [TestInitialize]
        public void AdvertPostServiceTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected Employer CreateEmployer(int index)
        {
            return _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
        }

        protected string PostAdvert(Employer employer, PostAdvertRequestMessage request)
        {
            IAdvertPostService service = new Integration.JobG8.AdvertPostService(employer.GetLoginId(), _jobAdsCommand, _jobAdIntegrationQuery, _externalJobAdsCommand, _locationQuery, _industriesQuery, _employersQuery, _loginCredentialsQuery, _serviceAuthenticationManager);
            return service.PostAdvert(request).PostAdvertResponse.Success;
        }

        protected JobAd AssertJobAd(Guid jobPosterId)
        {
            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(jobPosterId, JobAdStatus.Open));
            Assert.AreEqual(1, jobAds.Count);
            Assert.AreEqual(jobAds[0].Integration.IntegratorReferenceId, "RefABCD/1235");
            Assert.AreEqual(jobAds[0].Integration.ExternalReferenceId, "RefABCD");
            return jobAds[0];
        }
    }
}
