using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Queries;

namespace LinkMe.Apps.Services.External.JXT.Queries
{
    public class JxtQuery
        : IJxtQuery
    {
        private const string IntegratorUserName = "JXT";
        private readonly IIntegrationQuery _integrationQuery;
        private readonly IntegratorUser _integratorUser;

        public JxtQuery(IIntegrationQuery integrationQuery)
        {
            _integrationQuery = integrationQuery;
            _integratorUser = _integrationQuery.GetIntegratorUser(IntegratorUserName);
        }

        IntegratorUser IJxtQuery.GetIntegratorUser()
        {
            // Bit dangerous returning single instance but assuming no-one is updating this object once they have it.

            return _integratorUser;
        }
    }
}
