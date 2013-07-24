using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Query.Search.Employers
{
    public interface IEmployersRepository
    {
        IList<Employer> Search(OrganisationEmployerSearchCriteria criteria);
        IList<Employer> Search(AdministrativeEmployerSearchCriteria criteria);
    }
}