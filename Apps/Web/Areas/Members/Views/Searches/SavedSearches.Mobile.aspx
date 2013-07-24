<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<SearchesModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.Search"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.DeviceSavedSearches) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.RenderScripts(ScriptBundles.DeviceSavedSearches)%>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="Body" runat="server">
    <div class="savedsearches">
        <div class="title">My favourite searches</div>
<%  if (Model.Searches.Count > 0)
    {
        foreach (var search in Model.Searches)
        { %>
            <a class="row" href="<%= Html.RouteRefUrl(SearchRoutes.SavedSearch, new {savedSearchId = search.SearchId}) %>" id="<%= search.SearchId %>">
                <div class="column one">
                    <div class="icon alert <%= search.HasAlert ? "active" : "" %>" data-createurl="<%= Html.MungeUrl(Html.RouteRefUrl(SearchRoutes.ApiCreateAlertFromSearch)) %>" data-deleteurl="<%= Html.MungeUrl(Html.RouteRefUrl(SearchRoutes.ApiDeleteSearchAlert)) %>"></div>
                    <div class="icon delete" data-url="<%= Html.MungeUrl(Html.RouteRefUrl(SearchRoutes.ApiDeleteSearch)) %>"></div>                        
                </div>
                <div class="column two">
                    <span class="title"><%= search.Name %></span>
                    <span class="criteria"><%= search.Criteria.GetDisplayText() %></span>
                </div>
                <div class="column three">
                    <span class="icon rightarrow"></span>
                </div>
            </a>
<%      }
    }
    else
    { %>
            <div class="noresults">
                <div class="desc">You don't have any favourite searches.</div>
                <ul>
                    <li><%= Html.RouteRefLink("Search for jobs", HomeRoutes.Home) %>&nbsp;<span>now to create one</span></li>
                    <li><%= Html.RouteRefLink("Back", SearchRoutes.Searches) %>&nbsp;<span>to all my searches</span></li>
                </ul>
            </div>
<%  } %>
    </div>
</asp:Content>