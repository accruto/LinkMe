using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Custodians.Commands
{
    public interface ICustodiansCommand
    {
        void CreateCustodian(Custodian custodian);
        void UpdateCustodian(Custodian custodian);
    }
}
