using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Queries;

namespace LinkMe.Apps.Services.External.CareerOne.Queries
{
    public class CareerOneQuery
        : ICareerOneQuery
    {
        private const string IntegratorUserName = "CareerOne-jobs";
        private readonly IIntegrationQuery _integrationQuery;
        private readonly IntegratorUser _integratorUser;

        public CareerOneQuery(IIntegrationQuery integrationQuery)
        {
            _integrationQuery = integrationQuery;
            _integratorUser = _integrationQuery.GetIntegratorUser(IntegratorUserName);
        }

        IntegratorUser ICareerOneQuery.GetIntegratorUser()
        {
            // Bit dangerous returning single instance but assuming no-one is updating this object once they have it.

            return _integratorUser;
        }
    }
}
