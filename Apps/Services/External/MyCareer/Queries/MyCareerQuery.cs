using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Queries;

namespace LinkMe.Apps.Services.External.MyCareer.Queries
{
    public class MyCareerQuery
        : IMyCareerQuery
    {
        private const string IntegratorUserName = "MyCareer-jobs";
        private readonly IIntegrationQuery _integrationQuery;
        private readonly IntegratorUser _integratorUser;

        public MyCareerQuery(IIntegrationQuery integrationQuery)
        {
            _integrationQuery = integrationQuery;
            _integratorUser = _integrationQuery.GetIntegratorUser(IntegratorUserName);
        }

        IntegratorUser IMyCareerQuery.GetIntegratorUser()
        {
            // Bit dangerous returning single instance but assuming no-one is updating this object once they have it.

            return _integratorUser;
        }
    }
}