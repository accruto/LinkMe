<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="System.Web.Mvc.ViewPage<CustodiansModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Communities"%>
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
                <li><%= Html.RouteRefLink(Model.Community.Name, CommunitiesRoutes.Edit, new { id = Model.Community.Id }) %></li>
		    </ul>
	    </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Community custodians</h1>
    </div>
    
    <div class="form">

        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <ul class="actions">
                    <li><%=Html.RouteRefLink("Create a new community custodian", CommunitiesRoutes.NewCustodian, new { id = Model.Community.Id })%></li>
                </ul>
            </div>
            <div class="section-foot"></div>
        </div>

        <div class="section">
            <div class="section-body">
<%  if (Model.Custodians.Count == 0)
    { %>
                <div id="search-results-header">
                    <div class="search-results-count">
                        There are no custodians associated with this community.
                    </div>
                </div>
<%  }
    else
    { %>
                <table id="search-results" class="list">
                    <thead>
                        <tr>
                            <th class="user-column-header">Custodian</th>
                            <th class="status-column-header">Status</th>
                        </tr>
                    </thead>
                    <tbody>
    
<%      for (var index = 0; index < Model.Custodians.Count; ++index)
        {
            var custodian = Model.Custodians[index]; %>
           
                        <tr class="item item_<%= (index % 2) == 0 ? "odd" : "even" %> <%= custodian.IsEnabled ? "enabled-user" : "disabled-user" %> user">
                            <td><%= Html.RouteRefLink(custodian.FullName, CustodiansRoutes.Edit, new {id = custodian.Id})%></td>
                            <td><%= custodian.IsEnabled ? "Enabled" : "Disabled" %></td>
                        </tr>
<% } %>
                    </tbody>
                </table>
<% } %>
   
            </div>
        </div>

    </div>
        
</asp:Content>
