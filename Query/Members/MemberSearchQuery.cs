using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility.Expressions;

namespace LinkMe.Query.Members
{
    public enum JobsToSearch
    {
        AllJobs = 0,
        LastJob = 1,
        RecentJobs = 3
    }

    public enum JobAdSearchType
    {
        SuggestedCandidates,
        ManageCandidates
    }

    [Serializable]
    public class MemberSearchQuery
    {
        public MemberSortOrder SortOrder { get; set; }
        public bool ReverseSortOrder { get; set; }

        public int Skip { get; set; }
        public int? Take { get; set; }

        public JobsToSearch JobTitlesToSearch { get; set; }
        public IExpression JobTitle { get; set; }
        public IExpression Keywords { get; set; }
        public IExpression CompanyKeywords { get; set; }
        public JobsToSearch CompaniesToSearch { get; set; }
        public IExpression EducationKeywords { get; set; }
        public IExpression DesiredJobTitle { get; set; }
        public IList<Guid> IndustryIds { get; set; }
        public Guid? CommunityId { get; set; }
        public LocationReference Location { get; set; }
        public int Distance { get; set; }
        public bool IncludeRelocating { get; set; }
        public bool IncludeInternational { get; set; }
        public JobTypes? DesiredJobTypes { get; set; }
        public IList<CandidateStatus> CandidateStatusList { get; set; }
        public EthnicStatus? EthnicStatus { get; set; }
        public IList<VisaStatus> VisaStatusList { get; set; }
        public TimeSpan? Recency { get; set; }
        public Salary Salary { get; set; }
        public int? SeniorityIndex { get; set; }         // internal to Query for now - not available through MemberSearchCriteria
        public bool ExcludeNoSalary { get; set; }
        public bool IncludeSynonyms { get; set; }
        public bool? HasResume { get; set; }
        public bool? IsActivated { get; set; }
        public bool? IsContactable { get; set; }
        public bool? InFolder { get; set; }
        public bool? IsFlagged { get; set; }
        public bool? HasNotes { get; set; }
        public bool? HasViewed { get; set; }
        public bool? IsUnlocked { get; set; }
        public string Name { get; set; }
        public bool IncludeSimilarNames { get; set; }
    }
}
