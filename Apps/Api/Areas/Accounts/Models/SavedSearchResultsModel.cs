using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Json.Models;

namespace LinkMe.Apps.Api.Areas.Accounts.Models
{
    public class SavedSearchResultsModel
    {
        public Guid SavedSearchId { get; set; }
        public IList<Guid> CandidateIds { get; set; }
    }

    public class SavedSearchResultsResponseModel
        : JsonResponseModel
    {
        public int TotalSearches { get; set; }
        public IList<SavedSearchResultsModel> Searches { get; set; }
    }
}
