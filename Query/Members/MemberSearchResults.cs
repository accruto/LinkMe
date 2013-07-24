using System;
using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Members
{
    [Serializable]
    public class MemberSearchResults
        : SearchResults
    {
        private IList<Guid> _memberIds;
        private IList<KeyValuePair<CandidateStatus, int>> _candidateStatusHits;
        private IList<KeyValuePair<Guid, int>> _industryHits;
        private IList<KeyValuePair<JobTypes, int>> _desiredJobTypeHits;

        public IList<Guid> MemberIds
        {
            get { return _memberIds ?? new List<Guid>(); }
            set { _memberIds = value; }
        }

        public IList<KeyValuePair<CandidateStatus, int>> CandidateStatusHits
        {
            get { return _candidateStatusHits ?? new List<KeyValuePair<CandidateStatus, int>>(); }
            set { _candidateStatusHits = value; }
        }

        public IList<KeyValuePair<Guid, int>> IndustryHits
        {
            get { return _industryHits ?? new List<KeyValuePair<Guid, int>>(); }
            set { _industryHits = value; }
        }

        public IList<KeyValuePair<JobTypes, int>> DesiredJobTypeHits
        {
            get { return _desiredJobTypeHits ?? new List<KeyValuePair<JobTypes, int>>(); }
            set { _desiredJobTypeHits = value; }
        }
    }
}
