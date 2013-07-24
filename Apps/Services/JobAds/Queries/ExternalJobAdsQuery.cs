using System;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Services.External.JXT.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds;

namespace LinkMe.Apps.Services.JobAds.Queries
{
    public class ExternalJobAdsQuery
        : ExternalJobAdsComponent, IExternalJobAdsQuery
    {
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly IEmployersQuery _employersQuery;
        private readonly IIntegrationQuery _integrationQuery;
        private readonly Guid _jxtIntegratorUserId;

        public ExternalJobAdsQuery(IJobAdsQuery jobAdsQuery, IJobAdIntegrationQuery jobAdIntegrationQuery, ILoginCredentialsQuery loginCredentialsQuery, IEmployersQuery employersQuery, IJxtQuery jxtQuery, IIntegrationQuery integrationQuery)
            : base(jobAdsQuery, jobAdIntegrationQuery)
        {
            _loginCredentialsQuery = loginCredentialsQuery;
            _employersQuery = employersQuery;
            _integrationQuery = integrationQuery;
            _jxtIntegratorUserId = jxtQuery.GetIntegratorUser().Id;
        }

        IEmployer IExternalJobAdsQuery.GetJobPoster(IntegratorUser integratorUser)
        {
            // Look for an employer account with the same loginId.

            return GetJobPoster(integratorUser.LoginId);
        }

        IEmployer IExternalJobAdsQuery.GetJobPoster(IntegratorUser integratorUser, string loginId)
        {
            return GetJobPoster(loginId);
        }

        string IExternalJobAdsQuery.GetRedirectName(MemberJobAdView jobAd)
        {
            //return the company name for JXT; return the integrator use name otherwise
            if (jobAd.Integration.IntegratorUserId == _jxtIntegratorUserId)
            {
                return jobAd.ContactDetails.CompanyName;
            }

            return _integrationQuery.GetIntegrationSystem<Ats>(
                    _integrationQuery.GetIntegratorUser(jobAd.Integration.IntegratorUserId.Value).IntegrationSystemId).
                    Name;
        }

        Guid? IExternalJobAdsQuery.GetJobAdId(string externalReferenceId)
        {
            var jobAds = GetOrderedJobAds<JobAdEntry>(_jobAdIntegrationQuery.GetJobAdIds(externalReferenceId));
            return jobAds.Count == 0 ? (Guid?) null : jobAds[0].Id;
        }

        private IEmployer GetJobPoster(string loginId)
        {
            var userId = _loginCredentialsQuery.GetUserId(loginId);
            return userId == null
                ? null
                : _employersQuery.GetEmployer(userId.Value);
        }
    }
}
