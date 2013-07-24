<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdSearchListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search" %>
<%@ Import Namespace="LinkMe.Query.Search"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models"%>

<%  if (Model.ListType == JobAdListType.SearchResult || Model.ListType == JobAdListType.BrowseResult)
    { %>
    <div class="criteriahtml"><%= Model.Criteria == null ? null : Model.Criteria.GetCriteriaHtml() %></div>
<%  } %>
<div class="hits">
<%  if (Model.Results.IndustryHits != null)
    {
        foreach (var i in Model.Results.IndustryHits)
        { %>
            <div class="industryhits" id="<%= i.Key %>"><%= i.Value%></div>
<%      }
    }
    if (Model.Results.JobTypeHits != null)
    {
        foreach (var j in Model.Results.JobTypeHits)
        { %>
            <div class="jobtypehits" id="<%= j.Key %>"><%= j.Value %></div>
<%      }
    } %>
    <div class="total"><%= Model.Results.TotalJobAds %></div>
</div>
<%  foreach (var i in Model.Results.JobAdIds)
    {
        Html.RenderPartial("JobAdListView", Model.Results.JobAds[i]);
    } %>
