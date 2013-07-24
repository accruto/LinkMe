using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Resumes.Queries
{
    public interface IResumesQuery
    {
        Resume GetResume(Guid id);
        IList<Resume> GetResumes(IEnumerable<Guid> ids);
        ParsedResume GetParsedResume(Guid id);
    }
}