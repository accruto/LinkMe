using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Users.Anonymous.Commands
{
    public class AnonymousUsersCommand
        : IAnonymousUsersCommand
    {
        private readonly IAnonymousRepository _repository;

        public AnonymousUsersCommand(IAnonymousRepository repository)
        {
            _repository = repository;
        }

        AnonymousContact IAnonymousUsersCommand.CreateContact(AnonymousUser user, ContactDetails contactDetails)
        {
            // Look for an existing contact.

            var contact = _repository.GetContact(user, contactDetails);
            if (contact != null)
                return contact;

            // Create a new contact.

            user.Prepare();
            user.Validate();

            contact = new AnonymousContact
            {
                EmailAddress = contactDetails.EmailAddress,
                FirstName = contactDetails.FirstName,
                LastName = contactDetails.LastName
            };
            contact.Prepare();
            contact.Validate();
            _repository.CreateContact(user, contact);

            return contact;
        }
    }
}