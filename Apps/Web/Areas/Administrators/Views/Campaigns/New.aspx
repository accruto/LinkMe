<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<CampaignSummaryModel>" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
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
        <h1>New campaign</h1>
    </div>
    
    <div class="forms_v2">

        <% using (Html.RenderForm())
           {
               using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
               {
                   Html.RenderPartial("CampaignSummary", Model);%>
                   
                   <%= Html.ButtonsField().Add(new SaveButton()).Add(new CancelButton()) %>
            <% }
           } %>
    </div>
        
</asp:Content>

