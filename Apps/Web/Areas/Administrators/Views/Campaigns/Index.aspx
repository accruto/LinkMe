<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<PaginatedList<CampaignRecord>>" %>
<%@ Import Namespace="LinkMe.Web.Models"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Campaigns"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Helper"%>
<%@ Import Namespace="LinkMe.Web.Views.Shared"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
        <%= Html.StyleSheet(StyleSheets.Records) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Manage Campaigns</h1>
    </div>
    
    <ul class="action-list">
        <li><%= Html.RouteRefLink("New campaign", CampaignsRoutes.New, null)%></li>
    </ul>
    
    <div class="section">
        <div class="section-title">
            <h2>All Campaigns</h2>
        </div>
        <div class="section-content">
            <% Html.RenderPartial<Pager>(Model); %>

            <div>
            
<%  foreach (var campaign in Model.CurrentItems)
    {
        Html.RenderPartial("CampaignRecord", campaign); %>
                <hr />
<%  } %>
            </div>
            
            <% Html.RenderPartial<Pager>(Model); %>
        </div>
    </div>
            
</asp:Content>

