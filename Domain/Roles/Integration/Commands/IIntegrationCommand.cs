namespace LinkMe.Domain.Roles.Integration.Commands
{
    public interface IIntegrationCommand
    {
        void CreateIntegrationSystem(IntegrationSystem system);
        void CreateIntegratorUser(IntegratorUser integratorUser);
    }
}