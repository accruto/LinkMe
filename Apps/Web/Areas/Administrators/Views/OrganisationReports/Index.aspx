<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="System.Web.Mvc.ViewPage<ReportsModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Organisations"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Administrators.Page) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div class="section">
	    <div class="section-body">
		    <ul class="plain-actions actions">
                <li><%= Html.RouteRefLink(Model.Organisation.FullName, OrganisationsRoutes.Edit, new { id = Model.Organisation.Id }) %></li>
                <li><%= Html.RouteRefLink("Credits", OrganisationsRoutes.Credits, new { id = Model.Organisation.Id })%></li>
                <li><%= Html.RouteRefLink("Employers", OrganisationsRoutes.Employers, new { id = Model.Organisation.Id })%></li>
                <li><%= Html.RouteRefLink("Communications", OrganisationsRoutes.Communications, new { id = Model.Organisation.Id })%></li>
		    </ul>
	    </div>
    </div>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Organisation reports</h1>
    </div>
    
    <div class="form">

        <div class="section">
            <div class="section-body">
                
                <table id="search-results" class="list">
                    <thead>
                        <tr>
                            <th>Report name</th>
                            <th class="batch-status-column-header">Batch status</th>
                        </tr>
                    </thead>
                    <tbody>

<%  for (var index = 0; index < Model.Reports.Count; ++index)
    {
        var report = Model.Reports[index]; %>
                    
                        <tr class="item item_<%= (index % 2) == 0 ? "odd" : "even" %>">
                            <td><%= Html.RouteRefLink(report.Name, OrganisationsRoutes.Report, new { id = Model.Organisation.Id, type = report.GetType().Name })%></td>
                            <td><%= report.SendToAccountManager
                                        ? (report.SendToClient ? "To account manager and client" : "To account manager")
                                        : (report.SendToClient ? "To client" : "Disabled") %></td>
                        </tr>
                        
<%  } %>
                    
                    </tbody>
                </table>
            </div>
        </div>
                           
    </div>
        
</asp:Content>
