using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Anonymous.Queries
{
    public class AnonymousUsersQuery
        : IAnonymousUsersQuery
    {
        private readonly IAnonymousRepository _repository;

        public AnonymousUsersQuery(IAnonymousRepository repository)
        {
            _repository = repository;
        }

        AnonymousContact IAnonymousUsersQuery.GetContact(Guid contactId)
        {
            return _repository.GetContact(contactId);
        }

        AnonymousContact IAnonymousUsersQuery.GetContact(AnonymousUser user, ContactDetails contactDetails)
        {
            return _repository.GetContact(user, contactDetails);
        }
    }
}
