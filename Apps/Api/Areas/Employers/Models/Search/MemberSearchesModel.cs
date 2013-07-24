using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Query.Search.Members;

namespace LinkMe.Apps.Api.Areas.Employers.Models.Search
{
    public class MemberSearchModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsAlert { get; set; }
        public MemberSearchCriteria Criteria { get; set; }
    }

    public class MemberSearchesResponseModel
        : JsonResponseModel
    {
        public int TotalSearches { get; set; }
        public IList<MemberSearchModel> MemberSearches { get; set; }
    }
}