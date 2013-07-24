using System;
using System.Collections.Generic;

namespace LinkMe.Query
{
    public interface ISearchEngineQuery
    {
        IList<Guid> GetModified(DateTime? modifiedSince);
    }
}