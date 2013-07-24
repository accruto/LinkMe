using System;
using System.Collections.Generic;

namespace LinkMe.Query.Resources
{
    public interface IResourceSearchEngineQuery
        : ISearchEngineQuery
    {
    }

    public class ResourceSearchEngineQuery
        : IResourceSearchEngineQuery
    {
        private readonly IResourceSearchEngineRepository _repository;

        public ResourceSearchEngineQuery(IResourceSearchEngineRepository repository)
        {
            _repository = repository;
        }

        IList<Guid> ISearchEngineQuery.GetModified(DateTime? modifiedSince)
        {
            return _repository.GetModified(modifiedSince);
        }
    }
}
