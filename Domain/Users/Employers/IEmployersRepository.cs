using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers
{
    public interface IEmployersRepository
    {
        void CreateEmployer(Employer employer);
        void UpdateEmployer(Employer employer);

        Employer GetEmployer(Guid id);
        IList<Employer> GetEmployers(IEnumerable<Guid> ids);
        IList<Employer> GetEmployers(string emailAddress);

        IList<Guid> GetEmployerIds();
    }
}