<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CampaignActions>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Campaigns"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Communications.Campaigns"%>

<ul class="horizontal_action-list action-list">
    <li><%= Html.RouteRefLink("Edit campaign", CampaignsRoutes.Edit, new { id = Model.Campaign.Id })%></li>
    <li><%= Html.RouteRefLink("Edit criteria", CampaignsRoutes.EditCriteria, new { id = Model.Campaign.Id })%></li>
    <li><%= Html.RouteRefLink("Edit template", CampaignsRoutes.EditTemplate, new { id = Model.Campaign.Id })%></li>
    
    <% if (Model.Campaign.Status == CampaignStatus.Draft)
       { %>
    <li><%= Html.RouteRefLink("Delete", CampaignsRoutes.Delete, new { id = Model.Campaign.Id })%></li>
    <li><%= Html.RouteRefLink("Activate", CampaignsRoutes.Activate, new { id = Model.Campaign.Id })%></li>
    <% } %>
    <% else if (Model.Campaign.Status == CampaignStatus.Activated)
       { %>
    <li><%= Html.RouteRefLink("Stop", CampaignsRoutes.Stop, new { id = Model.Campaign.Id })%></li>
    <% } %>
    <% else if (Model.Campaign.Status == CampaignStatus.Stopped)
       { %>
    <li><%= Html.RouteRefLink("Activate", CampaignsRoutes.Activate, new { id = Model.Campaign.Id })%></li>
    <% } %>
    <% else if (Model.Campaign.Status == CampaignStatus.Running)
       { %>
    <li><%= Html.RouteRefLink("Stop", CampaignsRoutes.Stop, new { id = Model.Campaign.Id })%></li>
    <% } %>
</ul>

