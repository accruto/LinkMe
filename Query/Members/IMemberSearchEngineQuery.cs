using System;
using System.Collections.Generic;

namespace LinkMe.Query.Members
{
    public interface IMemberSearchEngineQuery
        : ISearchEngineQuery
    {
    }

    public class MemberSearchEngineQuery
        : IMemberSearchEngineQuery
    {
        private readonly IMemberSearchEngineRepository _repository;

        public MemberSearchEngineQuery(IMemberSearchEngineRepository repository)
        {
            _repository = repository;
        }

        IList<Guid> ISearchEngineQuery.GetModified(DateTime? modifiedSince)
        {
            return _repository.GetModified(modifiedSince);
        }
    }
}
