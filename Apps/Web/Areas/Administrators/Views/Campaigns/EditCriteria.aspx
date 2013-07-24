<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<CampaignCriteriaModel>" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Campaigns"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Communications.Campaigns"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

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
            <h2>Criteria</h2>
        </div>
            
        <div class="section-content forms_v2">
        
            <% using (Html.RenderForm())
               {
                    if (Model.Campaign.Category == CampaignCategory.Employer)
                        Html.RenderPartial("EmployerCriteria", Model);
                    else
                        Html.RenderPartial("MemberCriteria", Model);

                    if (!Model.IsReadOnly)
                    { %>
                        <%=Html.ButtonsField().Add(new SaveButton())%>
                 <% } %>
                 
                    <%=Html.LinkButton(CampaignsRoutes.Search, new { id = Model.Campaign.Id }, "Search", "search_button button")%>
             <% } %>
            
        </div>
    </div>
        
</asp:Content>

