using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Query.Search.Employers
{
    public interface IExecuteEmployerSearchCommand
    {
        IList<Employer> Search(EmployerSearchCriteria criteria);
    }
}