using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Resumes.Queries
{
    public class ResumesQuery
        : IResumesQuery
    {
        private readonly IResumesRepository _repository;

        public ResumesQuery(IResumesRepository repository)
        {
            _repository = repository;
        }

        Resume IResumesQuery.GetResume(Guid id)
        {
            return _repository.GetResume(id);
        }

        IList<Resume> IResumesQuery.GetResumes(IEnumerable<Guid> ids)
        {
            return _repository.GetResumes(ids);
        }

        ParsedResume IResumesQuery.GetParsedResume(Guid id)
        {
            return _repository.GetParsedResume(id);
        }
    }
}