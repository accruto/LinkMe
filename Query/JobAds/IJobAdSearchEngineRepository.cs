using System;
using System.Collections.Generic;

namespace LinkMe.Query.JobAds
{
    public interface IJobAdSearchEngineRepository
    {
        IList<Guid> GetSearchModified(DateTime? modifiedSince);
        IList<Guid> GetSortModified(DateTime? modifiedSince);
        void SetModified(Guid id);
    }
}