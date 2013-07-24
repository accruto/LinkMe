<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JobAdListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Query.Search"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models"%>

<div class="emptylist">
    <% if (Model.ListType == JobAdListType.SearchResult) { %>
        <p>Unfortunately, there aren't any jobs matching your criteria.</p>
        <p>Please change keywords or location, or reapply other filters.</p>
        <ul>
            <li class="changefilters">Change filter settings</li>
            <li class="resetfilters">Reset filter settings</li>
            <li><%= Html.RouteRefLink("Start a new job search", CurrentMember == null ? LinkMe.Web.Areas.Public.Routes.HomeRoutes.Home : LinkMe.Web.Areas.Members.Routes.HomeRoutes.Home, null)%></li>
        </ul>
    <% } %>
</div>
<% if (Model.Results.JobAds == null || Model.Results.JobAds.Count == 0) { %>
    <div class="row empty">
        <div class="titleline">
            <div class="icon new"></div>
            <div class="icon jobtype"></div>
            <div class="title"></div>
        </div>
        <div class="company"></div>
        <div class="icon saved"></div>
        <div class="location"></div>
        <div class="salary"></div>
        <div class="date"></div>
    </div>
<% } %>