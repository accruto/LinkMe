<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<SimilarJobsListModel>" %>
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
        <li><a href="<%= Html.RouteRefUrl(SearchRoutes.Search) %>">New job search</a></li>
        <li>Similar jobs</li>
    </ul>    
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="leftside">
        <div class="area search">
            <div class="newsearch">
                <div class="icon"></div>
                <a href="<%= Html.RouteRefUrl(SearchRoutes.Search) %>">New search</a>
            </div>
        </div>
        <% Html.RenderPartial("MyFavouriteJobs", Model); %>
        <% Html.RenderPartial("HiddenJobs", Model); %>
    </div>
    
    <div class="rightside <%= CurrentMember == null ? "notloggedin" : "" %>  <%= Model.ListType %>" partialurl="<%= Html.MungeUrl(Html.RouteRefUrl(JobAdsRoutes.SimilarPartial, new { jobAdId = Model.JobAdId })) %>">

        <div class="resultheader">
            <div class="icon"></div>
            <div class="title">Similar jobs</div>
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