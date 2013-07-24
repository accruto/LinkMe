using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Anonymous.Commands
{
    public interface IAnonymousUsersCommand
    {
        AnonymousContact CreateContact(AnonymousUser user, ContactDetails contactDetails);
    }
}