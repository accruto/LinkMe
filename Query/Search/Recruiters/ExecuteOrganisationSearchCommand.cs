using System.Collections.Generic;
using LinkMe.Domain.Roles.Recruiters;

namespace LinkMe.Query.Search.Recruiters
{
    public class ExecuteOrganisationSearchCommand
        : IExecuteOrganisationSearchCommand
    {
        private readonly IRecruitersRepository _repository;

        public ExecuteOrganisationSearchCommand(IRecruitersRepository repository)
        {
            _repository = repository;
        }

        IList<Organisation> IExecuteOrganisationSearchCommand.Search(OrganisationSearchCriteria criteria)
        {
            return _repository.Search(criteria);
        }

        IList<string> IExecuteOrganisationSearchCommand.GetOrganisationFullNames(string partialFullName, int count)
        {
            return _repository.GetOrganisationFullNames(partialFullName, count);
        }
    }
}