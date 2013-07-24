using LinkMe.Domain.Roles.Integration;

namespace LinkMe.Apps.Services.External.MyCareer.Queries
{
    public interface IMyCareerQuery
    {
        IntegratorUser GetIntegratorUser();
    }
}