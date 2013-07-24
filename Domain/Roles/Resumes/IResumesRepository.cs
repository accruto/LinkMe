using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Resumes
{
    public interface IResumesRepository
    {
        void CreateResume(Resume resume);
        void UpdateResume(Resume resume);

        Resume GetResume(Guid id);
        IList<Resume> GetResumes(IEnumerable<Guid> ids);

        void CreateParsedResume(ParsedResume parsedResume);
        void DeleteParsedResume(Guid id);

        ParsedResume GetParsedResume(Guid id);
    }
}
