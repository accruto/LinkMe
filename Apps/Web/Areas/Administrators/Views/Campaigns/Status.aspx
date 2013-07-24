<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<Campaign>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Communications.Campaigns"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1><%= Model.Name %></h1>
    </div>
    
    <div>
        <ul class="action-list">
            <li><%= Html.RouteRefLink("All campaigns", CampaignsRoutes.Index)%></li>
            <li><%= Html.RouteRefLink("Edit campaign", CampaignsRoutes.Edit, new { id = Model.Id })%></li>
        </ul>
    </div>
        
    <div class="forms_v2">

        <div>
            <p>The '<%=Model.Name %>' campaign is now <%= Model.Status.ToString().ToLower() %>.</p>
        </div>
        
    </div>
        
</asp:Content>

