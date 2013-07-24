using System.Collections.Generic;
using LinkMe.Domain.Roles.Recruiters;

namespace LinkMe.Query.Search.Recruiters
{
    public interface IRecruitersRepository
    {
        IList<Organisation> Search(OrganisationSearchCriteria criteria);
        IList<string> GetOrganisationFullNames(string partialFullName, int count);
    }
}