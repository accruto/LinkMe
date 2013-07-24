<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<FolderListModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.RenderStyles(StyleBundles.JobSearchResults)%>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom) %>
        <%= Html.RenderScripts(ScriptBundles.JobSearchResults)%>
    </mvc:RegisterJavaScripts>
    
    <% Html.RenderPartial("JSVariables", Model); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageSubHeader" runat="server">
    <ul class="breadcrumbs">
        <li>Candidate site</li>
        <li><a href="<%= Html.RouteRefUrl(SearchRoutes.Search) %>">Jobs</a></li>
        <li>Jobs in folder</li>
    </ul>    
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="leftside">
        <div class="area search">
            <div class="newsearch">
                <div class="icon"></div>
                <a href="<%= Html.RouteRefUrl(SearchRoutes.Search) %>">New search</a>
            </div>
            <div class="backtoresults">
                <div class="icon"></div>
                <a href="<%= Html.RouteRefUrl(SearchRoutes.Results) %>">Back to search results</a>
            </div>
        </div>
        <% Html.RenderPartial("MyFavouriteJobs", Model); %>
        <% Html.RenderPartial("HiddenJobs", Model); %>
    </div>
    <div class="rightside <%= Model.ListType %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.PartialFolder, new { folderId = Model.Folder.Id })) %>">
        <div class="resultheader">
            <div class="icon folder"></div>
            <div class="title"><%= Model.Folder.Name %></div>
            <div class="icon rename" folderid="<%= Model.Folder.Id %>"></div>
        </div>

        <% Html.RenderPartial("ResultsCount", Model); %>

        <% Html.RenderPartial("Sort", Model); %>

        <% Html.RenderPartial("TitleBar", Model); %>

        <div class="results">
            <% Html.RenderPartial("JobAdList", Model); %>
            <% Html.RenderPartial("EmptyList", Model); %>
        </div>

        <% Html.RenderPartial("Pagination", Model); %>
    </div>
    
    <% Html.RenderPartial("Overlays", Model); %>
</asp:Content>