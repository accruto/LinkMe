using System;
using System.Collections.Generic;

namespace LinkMe.Query.Search.Members.Commands
{
    public class AdministrativeMemberSearchCommand
        : IAdministrativeMemberSearchCommand
    {
        private readonly ISearchMembersRepository _repository;

        public AdministrativeMemberSearchCommand(ISearchMembersRepository repository)
        {
            _repository = repository;
        }

        IList<Guid> IAdministrativeMemberSearchCommand.Search(AdministrativeMemberSearchCriteria criteria)
        {
            return _repository.Search(criteria);
        }
    }
}