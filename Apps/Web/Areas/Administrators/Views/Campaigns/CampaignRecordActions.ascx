<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<CampaignRecord>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Campaigns"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Communications.Campaigns"%>
<%@ Import Namespace="LinkMe.Web.Models"%>

<ul class="corner_inline_action-list inline_action-list action-list">
<%  if (Model.Status == CampaignStatus.Draft)
    { %>
    <li><%= Html.RouteRefLink("Edit", CampaignsRoutes.Edit, new { id = Model.Id }, new { @class = "edit-action" })%></li>
    <li><%= Html.RouteRefLink("Delete", CampaignsRoutes.Delete, new { id = Model.Id, page = Model.Page }, new { @class = "delete-action" })%></li>
    <li><%= Html.RouteRefLink("Activate", CampaignsRoutes.Activate, new { id = Model.Id })%></li>
<%  }
    else if (Model.Status == CampaignStatus.Activated)
    { %>
    <li><%= Html.RouteRefLink("View", CampaignsRoutes.Edit, new { id = Model.Id }, new { @class = "edit-action" })%></li>
    <li><%= Html.RouteRefLink("Stop", CampaignsRoutes.Stop, new { id = Model.Id })%></li>
    <li><%= Html.RouteRefLink("Report", CampaignsRoutes.Report, new { id = Model.Id })%></li>
<%  }
    else if (Model.Status == CampaignStatus.Stopped)
    { %>
    <li><%= Html.RouteRefLink("Edit", CampaignsRoutes.Edit, new { id = Model.Id }, new { @class = "edit-action" })%></li>
    <li><%= Html.RouteRefLink("Activate", CampaignsRoutes.Activate, new { id = Model.Id })%></li>
    <li><%= Html.RouteRefLink("Report", CampaignsRoutes.Report, new { id = Model.Id })%></li>
<%  }
    else if (Model.Status == CampaignStatus.Running)
    { %>
    <li><%= Html.RouteRefLink("View", CampaignsRoutes.Edit, new { id = Model.Id }, new { @class = "edit-action" })%></li>
    <li><%= Html.RouteRefLink("Stop", CampaignsRoutes.Stop, new { id = Model.Id })%></li>
    <li><%= Html.RouteRefLink("Report", CampaignsRoutes.Report, new { id = Model.Id })%></li>
<%  } %>
    
    <li><%= Html.RouteRefLink("Preview", CampaignsRoutes.Preview, new { id = Model.Id })%></li>
</ul>

