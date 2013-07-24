using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Queries;

namespace LinkMe.Apps.Services.External.JobG8.Queries
{
    public class JobG8Query
        : IJobG8Query
    {
        private const string IntegratorUserName = "JobG8";
        private readonly IIntegrationQuery _integrationQuery;
        private readonly IntegratorUser _integratorUser;

        public JobG8Query(IIntegrationQuery integrationQuery)
        {
            _integrationQuery = integrationQuery;
            _integratorUser = _integrationQuery.GetIntegratorUser(IntegratorUserName);
        }

        IntegratorUser IJobG8Query.GetIntegratorUser()
        {
            // Bit dangerous returning single instance but assuming no-one is updating this object once they have it.

            return _integratorUser;
        }
    }
}
