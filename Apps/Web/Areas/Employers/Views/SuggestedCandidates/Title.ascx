<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SuggestedCandidatesListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Users.Members.Search"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Search"%>

<span id="results-header-text" style="display:none;">
    <span class="suggested-candidate_search-criterion search-criterion">
        <span class="search-criterion-name">Suggested candidates for</span>
        <span class="search-criterion-data"><%= Model.JobAd.Title %></span>
    </span>
    <%= Model.Criteria.GetDisplayHtml(false) %>
</span>
