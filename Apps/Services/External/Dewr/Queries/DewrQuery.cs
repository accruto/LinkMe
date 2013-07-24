using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Queries;

namespace LinkMe.Apps.Services.External.Dewr.Queries
{
    public class DewrQuery
        : IDewrQuery
    {
        private const string IntegratorUserName = "Dewr-jobs";
        private readonly IIntegrationQuery _integrationQuery;
        private readonly IntegratorUser _integratorUser;

        public DewrQuery(IIntegrationQuery integrationQuery)
        {
            _integrationQuery = integrationQuery;
            _integratorUser = _integrationQuery.GetIntegratorUser(IntegratorUserName);
        }

        IntegratorUser IDewrQuery.GetIntegratorUser()
        {
            // Bit dangerous returning single instance but assuming no-one is updating this object once they have it.

            return _integratorUser;
        }
    }
}
