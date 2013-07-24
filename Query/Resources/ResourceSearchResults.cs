using System;
using System.Collections.Generic;
using LinkMe.Domain.Resources;

namespace LinkMe.Query.Resources
{
    [Serializable]
    public class ResourceSearchResults
        : SearchResults
    {
        private IList<Guid> _resourceIds;
        private IList<KeyValuePair<ResourceType, int>> _resourceTypeHits;

        public IList<Guid> ResourceIds
        {
            get { return _resourceIds ?? new List<Guid>(); }
            set { _resourceIds = value; }
        }

        public IList<KeyValuePair<ResourceType, int>> ResourceTypeHits
        {
            get { return _resourceTypeHits ?? new List<KeyValuePair<ResourceType, int>>(); }
            set { _resourceTypeHits = value; }
        }
    }
}
