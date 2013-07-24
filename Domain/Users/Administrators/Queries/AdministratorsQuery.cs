using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Administrators.Queries
{
    public class AdministratorsQuery
        : IAdministratorsQuery
    {
        private readonly IAdministratorsRepository _repository;

        public AdministratorsQuery(IAdministratorsRepository repository)
        {
            _repository = repository;
        }

        Administrator IAdministratorsQuery.GetAdministrator(Guid id)
        {
            return _repository.GetAdministrator(id);
        }

        IList<Administrator> IAdministratorsQuery.GetAdministrators()
        {
            return _repository.GetAdministrators(false);
        }

        IList<Administrator> IAdministratorsQuery.GetAdministrators(bool enabledOnly)
        {
            return _repository.GetAdministrators(enabledOnly);
        }
    }
}