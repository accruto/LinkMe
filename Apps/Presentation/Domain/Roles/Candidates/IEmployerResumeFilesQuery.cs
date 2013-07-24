using System.Collections.Generic;
using Ionic.Zip;
using LinkMe.Domain.Users.Employers.Views;

namespace LinkMe.Apps.Presentation.Domain.Roles.Candidates
{
    public interface IEmployerResumeFilesQuery
    {
        DocFile GetResumeFile(EmployerMemberView member);
        ZipFile GetResumeFile(IEnumerable<EmployerMemberView> members);
    }
}
