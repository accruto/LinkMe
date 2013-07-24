using System;
using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.JobAds
{
    [Serializable]
    public class JobAdSearchResults
        : SearchResults
    {
        private IList<Guid> _jobAdIds;
        private IList<KeyValuePair<Guid, int>> _industryHits;
        private IList<KeyValuePair<JobTypes, int>> _jobTypeHits;

        public IList<Guid> JobAdIds
        {
            get { return _jobAdIds ?? new List<Guid>(); }
            set { _jobAdIds = value; }
        }

        public IList<KeyValuePair<Guid, int>> IndustryHits
        {
            get { return _industryHits ?? new List<KeyValuePair<Guid, int>>(); }
            set { _industryHits = value; }
        }

        public IList<KeyValuePair<JobTypes, int>> JobTypeHits
        {
            get { return _jobTypeHits ?? new List<KeyValuePair<JobTypes, int>>(); }
            set { _jobTypeHits = value; }
        }
    }
}