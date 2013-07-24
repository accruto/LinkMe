using System;
using System.Collections.Generic;

namespace LinkMe.Query.JobAds
{
    public interface IJobAdSearchEngineQuery
        : ISearchEngineQuery
    {
    }

    public class JobAdSearchEngineQuery
        : IJobAdSearchEngineQuery
    {
        private readonly IJobAdSearchEngineRepository _repository;

        public JobAdSearchEngineQuery(IJobAdSearchEngineRepository repository)
        {
            _repository = repository;
        }

        IList<Guid> ISearchEngineQuery.GetModified(DateTime? modifiedSince)
        {
            return _repository.GetSearchModified(modifiedSince);
        }
    }
}
