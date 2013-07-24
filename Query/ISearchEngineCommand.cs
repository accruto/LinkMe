using System;

namespace LinkMe.Query
{
    public interface ISearchEngineCommand
    {
        void SetModified(Guid id);
    }
}