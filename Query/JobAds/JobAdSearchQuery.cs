using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility.Expressions;

namespace LinkMe.Query.JobAds
{
    [Serializable]
    public class JobAdSearchQuery
    {
        public JobAdSortOrder SortOrder;
        public bool ReverseSortOrder;

        public int Skip;
        public int? Take;

        public bool IncludeSynonyms;

        public IExpression AdTitle;
        public LocationReference Location;
        public int Distance;
        public IExpression Keywords;

        public Guid? CommunityId;
        public bool? CommunityOnly;

        public TimeSpan? Recency;

        public IList<Guid> IndustryIds;
        public IExpression AdvertiserName;
        public Salary Salary;
        public bool ExcludeNoSalary;
        public JobTypes? JobTypes;
        public IList<LocationReference> Relocations;
        public bool? IsFlagged;
        public bool? HasNotes;
        public bool? HasViewed;
        public bool? HasApplied;
        public int? SeniorityIndex;         // internal to Query for now - not available through JobAdSearchCriteria
    }
}
