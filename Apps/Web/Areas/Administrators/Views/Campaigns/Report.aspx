<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<Campaign>" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Communications.Campaigns"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Campaign: <%= Model.Name %></h1>
    </div>
    
    <div>
        <ul class="action-list">
            <li><%= Html.RouteRefLink("All campaigns", CampaignsRoutes.Index, null)%></li>
            <li><%= Html.RouteRefLink("Edit criteria", CampaignsRoutes.EditCriteria, new { id = Model.Id })%></li>
            <li><%= Html.RouteRefLink("Edit template", CampaignsRoutes.EditTemplate, new { id = Model.Id })%></li>
            
            <% if (Model.Status == CampaignStatus.Activated)
               { %>
            <li><%= Html.RouteRefLink("Stop", CampaignsRoutes.Stop, new { id = Model.Id })%></li>
            <% } %>
            <% else if (Model.Status == CampaignStatus.Stopped)
               { %>
            <li><%= Html.RouteRefLink("Activate", CampaignsRoutes.Activate, new { id = Model.Id })%></li>
            <% } %>
            <% else if (Model.Status == CampaignStatus.Running)
               { %>
            <li><%= Html.RouteRefLink("Stop", CampaignsRoutes.Stop, new { id = Model.Id })%></li>
            <% } %>

        </ul>
    </div>
        
    <div class="forms_v2">

        <% using (Html.RenderForm())
           {
               using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
               {
                   Html.RenderPartial("CampaignSummary", Model);
//                   Html.DateTimeField("ActivatedTime", "Activated", Model.ActivatedTime, null, FieldOptions.ReadOnly);
  //                 Html.DateTimeField("StartedTime", "Started", Model.StartedTime, null, FieldOptions.ReadOnly);
    //               Html.DateTimeField("StoppedTime", "Stopped", Model.StoppedTime, null, FieldOptions.ReadOnly);
                   //Html.IntegerField("Sent", 43, FieldOptions.ReadOnly);
                   //Html.IntegerField("Opened", 12, FieldOptions.ReadOnly);
                   //Html.IntegerField("Links clicked", "LinksClicked", 34, FieldOptions.ReadOnly);
               }
           } %>
        
    </div>
        
</asp:Content>

