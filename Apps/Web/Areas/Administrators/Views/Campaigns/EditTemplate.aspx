<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<CampaignSummaryModel>" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Campaigns"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Campaign: <%= Model.Campaign.Name %></h1>
    </div>
    
    <ul class="action-list">
        <li><%= Html.RouteRefLink("Back to all campaigns", CampaignsRoutes.Index, null)%></li>
    </ul>
    
    <div class="section">
        <div class="section-title">
            <h2>Actions</h2>
        </div>
        <div class="section-content">
            <% Html.RenderPartial("ActionList", new CampaignActions(Model.Campaign)); %>
        </div>
    </div>
    
    <div class="section">
        <div class="section-title">
            <h2>Template</h2>
        </div>
            
        <div class="section-content forms_v2">

            <% using (Html.RenderRouteForm(CampaignsRoutes.EditTemplate))
               {%>
            
                <% using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
                   {
                %>
                    <%= Html.TextBoxField(Model, m => m.Template.Subject).WithLargestWidth() %>
                    <%= Html.MessageTextBoxField(Model, m => m.Template.Body).WithSize(25, 50) %>
                    
                    <% if (!Model.IsReadOnly) {%>
                        <%=Html.ButtonsField().Add(new SaveButton()).Add(new PreviewRouteButton(CampaignsRoutes.Preview, new { id = Model.Campaign.Id })) %>
                     <%} else {%>
                        <%=Html.ButtonsField().Add(new PreviewRouteButton(CampaignsRoutes.Preview, new { id = Model.Campaign.Id })) %>
                     <%}%>
                                        
                <% } %>

            <% } %>
        </div>
        
    </div>
    
    <div class="section">
        <div class="section-title">
            <h2>Send test emails</h2>
        </div>
            
        <div class="section-content forms_v2">

            <% using (Html.RenderRouteForm(CampaignsRoutes.Send, new { id = Model.Campaign.Id }, "SendForm"))
               {%>

                <% using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
                   {%>

                    <%= Html.TextBoxField("emailAddresses", string.Empty).WithLabel("Email addresses").WithLargestWidth() %>
                    <%= Html.TextBoxField("loginIds", string.Empty).WithLabel("Login ids").WithLargestWidth()%>
                    <%= Html.ButtonsField().Add(new SendButton()) %>

                <% } %>

            <% } %>
            
        </div>
    </div>
    
</asp:Content>

