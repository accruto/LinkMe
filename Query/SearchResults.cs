using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Query
{
    [Serializable]
    public abstract class SearchResults
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        public int TotalMatches { get; set; }
    }
}
