namespace LinkMe.Domain.Contacts
{
    public interface IAdministrator
        : IRegisteredUser
    {
        EmailAddress EmailAddress { get; }
    }
}
