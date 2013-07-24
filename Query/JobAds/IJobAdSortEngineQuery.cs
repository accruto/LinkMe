using System;
using System.Collections.Generic;

namespace LinkMe.Query.JobAds
{
    public interface IJobAdSortEngineQuery
        : ISearchEngineQuery
    {
    }

    public class JobAdSortEngineQuery
        : IJobAdSortEngineQuery
    {
        private readonly IJobAdSearchEngineRepository _repository;

        public JobAdSortEngineQuery(IJobAdSearchEngineRepository repository)
        {
            _repository = repository;
        }

        IList<Guid> ISearchEngineQuery.GetModified(DateTime? modifiedSince)
        {
            return _repository.GetSortModified(modifiedSince);
        }
    }
}
