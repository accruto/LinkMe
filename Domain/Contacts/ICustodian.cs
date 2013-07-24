namespace LinkMe.Domain.Contacts
{
    public interface ICustodian
        : IRegisteredUser
    {
        EmailAddress EmailAddress { get; }
    }
}
