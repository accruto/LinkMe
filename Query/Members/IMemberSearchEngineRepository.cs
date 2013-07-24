using System;
using System.Collections.Generic;

namespace LinkMe.Query.Members
{
    public interface IMemberSearchEngineRepository
    {
        IList<Guid> GetModified(DateTime? modifiedSince);
        void SetModified(Guid id);
    }
}
