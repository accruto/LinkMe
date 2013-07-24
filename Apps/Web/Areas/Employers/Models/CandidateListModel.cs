using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;

namespace LinkMe.Web.Areas.Employers.Models
{
    public class CandidateListResultsModel
    {
        public int TotalCandidates { get; set; }
        public IList<Guid> CandidateIds { get; set; }
        public IList<Guid> RejectedCandidateIds { get; set; }
        public EmployerMemberViews Views { get; set; }
        public IDictionary<Guid, DateTime> LastUpdatedTimes { get; set; }
        public IDictionary<CandidateStatus, int> CandidateStatusHits { get; set; }
        public IList<KeyValuePair<Guid, int>> IndustryHits { get; set; }
        public IDictionary<JobTypes, int> DesiredJobTypeHits { get; set; }
    }

    public class CandidateListRecoveryModel
    {
        public IList<SpellingSuggestion> SpellingSuggestions { get; set; }
        public IList<MemberSearchSuggestion> MoreResultsSuggestions { get; set; }
    }

    public abstract class CandidateListModel
    {
        public MemberSearchCriteria Criteria { get; set; }
        public CandidatesPresentationModel Presentation { get; set; }
        public CandidateListResultsModel Results { get; set; }
        public CandidateListRecoveryModel Recovery { get; set; }

        public IList<MemberSortOrder> SortOrders { get; set; }
        public IDictionary<Guid, Community> Communities { get; set; }
        public IDictionary<Guid, Vertical> Verticals { get; set; }
    }
}
