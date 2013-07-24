using System;
using System.Collections.Generic;

namespace LinkMe.Query.Resources
{
    public interface IResourceSearchEngineRepository
    {
        IList<Guid> GetModified(DateTime? modifiedSince);
        void SetModified(Guid id);
    }
}
