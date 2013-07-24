using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Integration.JobG8;
using LinkMe.Apps.Services.JobAds.Commands;
using LinkMe.Apps.Services.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Query.Reports.Roles.Integration.Commands;

namespace LinkMe.Apps.Integration.Test.JobG8.Services
{
    public abstract class AdvertPostServiceTests
        : JobG8Tests
    {
        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();
        private readonly IServiceAuthenticationManager _serviceAuthenticationManager = Resolve<IServiceAuthenticationManager>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        protected readonly IJobAdIntegrationQuery _jobAdIntegrationQuery = Resolve<IJobAdIntegrationQuery>();
        private readonly IExternalJobAdsCommand _externalJobAdsCommand = Resolve<IExternalJobAdsCommand>();
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly IJobAdIntegrationReportsCommand _jobAdIntegrationReportsCommand = Resolve<IJobAdIntegrationReportsCommand>();

        protected string PostAdvert(Employer employer, PostAdvertRequestMessage request)
        {
            IAdvertPostService service = new AdvertPostService(employer.GetLoginId(), _jobAdsCommand, _jobAdsQuery, _jobAdIntegrationQuery, _externalJobAdsCommand, _locationQuery, _industriesQuery, _employersQuery, _loginCredentialsQuery, _serviceAuthenticationManager, _jobAdIntegrationReportsCommand);
            return service.PostAdvert(request).PostAdvertResponse.Success;
        }

        protected string AmendAdvert(Employer employer, AmendAdvertRequestMessage request)
        {
            IAdvertPostService service = new AdvertPostService(employer.GetLoginId(), _jobAdsCommand, _jobAdsQuery, _jobAdIntegrationQuery, _externalJobAdsCommand, _locationQuery, _industriesQuery, _employersQuery, _loginCredentialsQuery, _serviceAuthenticationManager, _jobAdIntegrationReportsCommand);
            return service.AmendAdvert(request).AmendAdvertResponse.Success;
        }
    }
}