<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<SearchesModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Search"%>

<% for (var i = 0; i < Model.Searches.Count; i++ ) { %>
    <div class="row <%= i % 2 == 0 ? "odd" : "" %>" id="<%= Model.Searches[i].SearchId %>">
        <div class="hover">
            <div class="icon alert <%= Model.Searches[i].HasAlert ? "active" : "" %>" createurl="<%= Html.MungeUrl(Html.RouteRefUrl(SearchRoutes.ApiCreateAlertFromSearch)) %>" deleteurl="<%= Html.MungeUrl(Html.RouteRefUrl(SearchRoutes.ApiDeleteSearchAlert)) %>" alertid="<%= Model.Searches[i].SearchId %>"></div>
            <div class="divider">
                <div class="line one"></div>
                <div class="line two"></div>
            </div>
            <div class="title"><a href="<%= Html.RouteRefUrl(SearchRoutes.SavedSearch, new { savedSearchId = Model.Searches[i].SearchId }) %>"><span><%= !string.IsNullOrEmpty(Model.Searches[i].Name) ? Model.Searches[i].Name : Model.Searches[i].Criteria.GetDisplayHtml() %></span></a></div>
            <div class="divider rename">
                <div class="line one"></div>
                <div class="line two"></div>
                <div class="line three"></div>
            </div>
            <div class="icon action rename"></div>
            <div class="divider delete">
                <div class="line one"></div>
                <div class="line two"></div>
                <div class="line three"></div>
            </div>
            <div class="icon action delete" url="<%= Html.MungeUrl(Html.RouteRefUrl(SearchRoutes.ApiDeleteSearch)) %>"></div>
        </div>
    </div>
<% } %>
<% Html.RenderPartial("Pagination", Model); %>

<% if (Model.Searches.Count == 0) { %>
    <div class="text empty">You don't have any favourite searches.</div>
<% } %>