<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<CampaignCriteriaResultsModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Framework.Communications"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Campaigns"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1><%= Model.Campaign.Name %></h1>
    </div>
    
    <ul class="action-list">
        <li><%= Html.RouteRefLink("Back to all campaigns", CampaignsRoutes.Index)%></li>
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
            <h1>Search results</h1>
        </div>
    </div>
        
    <div class="section-content">
    
        <h3><%=Model.Users.Count %> users found</h3>

        <table class="list">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Email</th>
                </tr>
            </thead>
            <tbody>
        <% foreach (var user in Model.Users)
           { %>

                <tr class="item">
                    <td><%=user.FullName %></td>
                    <td><%= ((ICommunicationRecipient)user).EmailAddress %></td>
                </tr>
                
        <% } %>

            </tbody>
	    </table>

    </div>
        
</asp:Content>

