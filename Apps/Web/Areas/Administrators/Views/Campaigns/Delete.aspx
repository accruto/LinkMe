<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<CampaignDeleteSummaryModel>" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Campaigns"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Campaign: <%= Model.CampaignSummary.Campaign.Name %></h1>
    </div>
    
    <div class="forms_v2">

        <% using (Html.RenderForm(new { page = Model.Pagination.Page }))
           {%>

            <div>Are you sure you want to delete the '<%= Model.CampaignSummary.Campaign.Name%>' campaign?</div>        
            <%= Html.SubmitButton("yes", "Delete", "yes_button button") %>
            <%= Html.BackButton("Back", "back_button button") %>

        <% } %>
        
    </div>
        
</asp:Content>

