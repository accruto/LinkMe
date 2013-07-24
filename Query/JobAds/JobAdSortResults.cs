using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Query.JobAds
{
    [Serializable]
    public class JobAdSortResult
    {
        public Guid JobAdId { get; set; }
    }

    [Serializable]
    public class JobAdSortResults
        : SearchResults, ICloneable
    {
        private List<JobAdSortResult> _results = new List<JobAdSortResult>();

        public void TrimExcessMatches(int maximumAvailable)
        {
            _results = _results.Take(maximumAvailable).ToList();
        }

        public IList<JobAdSortResult> Results
        {
            get { return _results; }
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}