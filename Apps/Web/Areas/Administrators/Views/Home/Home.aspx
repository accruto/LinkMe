<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="LinkMe.Web.Areas.Accounts.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.NoLeftSideBar) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="shadowed-section section">
        <div class="section-head"></div>
        <div class="section-body">
    
            <div class="section-title">
                <h1>Members</h1>
            </div>
    
            <ul class="actions">
                <li><%= Html.RouteRefLink("Search members", MembersRoutes.Search) %></li>
            </ul>
            
        </div>
        <div class="section-foot"></div>
    </div>

    <div class="shadowed-section section">
        <div class="section-head"></div>
        <div class="section-body">
            
            <div class="section-title">
                <h1>Organisations</h1>
            </div>
    
            <ul class="actions">
                <li><%= Html.RouteRefLink("New organisation", OrganisationsRoutes.New) %></li>
                <li><%= Html.RouteRefLink("Search organisations", OrganisationsRoutes.Search) %></li>
            </ul>
            
        </div>
        <div class="section-foot"></div>
    </div>

    <div class="shadowed-section section">
        <div class="section-head"></div>
        <div class="section-body">
            
            <div class="section-title">
                <h1>Employers</h1>
            </div>
    
            <ul class="actions">
                <li><%= Html.RouteRefLink("Search employers", EmployersRoutes.Search) %></li>
            </ul>
            
        </div>
        <div class="section-foot"></div>
    </div>

    <div class="shadowed-section section">
        <div class="section-head"></div>
        <div class="section-body">
            
            <div class="section-title">
                <h1>Communities</h1>
            </div>
    
            <ul class="actions">
                <li><%= Html.RouteRefLink("Communities", CommunitiesRoutes.Index)%></li>
            </ul>
            
        </div>
        <div class="section-foot"></div>
    </div>

    <div class="shadowed-section section">
        <div class="section-head"></div>
        <div class="section-body">
            
            <div class="section-title">
                <h1>Account settings</h1>
            </div>
        
            <ul class="actions">
                <li><%= Html.RouteRefLink("Change password", AccountsRoutes.ChangePassword) %></li>
            </ul>
            
        </div>
        <div class="section-foot"></div>
    </div>
    
</asp:Content>
