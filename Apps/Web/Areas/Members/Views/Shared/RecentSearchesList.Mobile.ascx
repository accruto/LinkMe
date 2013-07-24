<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<IList<JobAdSearchModel>>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search" %>
<%@Import namespace="LinkMe.Web.Areas.Members.Models.Search" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>

<div class="recentsearches">
    <div class="title">Recent searches</div>
<%  if (Model.Count > 0)
    {
        foreach (var search in Model)
        { %>
            <a class="row" href="<%= Html.RouteRefUrl(SearchRoutes.RecentSearch, new {recentSearchId = search.ExecutionId}) %>">
                <span class="title"><%= search.Criteria.GetDisplayText() %></span>
                <span class="icon rightarrow"></span>
                <span class="count"><%= 575 %></span>
            </a>
<%      }
    }
    else
    { %>
        <div class="noresults">
            <div class="desc">You don't have any recent searches.</div>
            <ul>
                <li><%= Html.RouteRefLink("Search for jobs", HomeRoutes.Home) %>&nbsp;<span>now</span></li>
            </ul>
        </div>
<%  } %>
</div>