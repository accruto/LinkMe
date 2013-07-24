<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="System.Web.Mvc.ViewPage<IList<Administrator>>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.NoLeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Administrators.Page) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Administrators</h1>
    </div>
    
    <div class="form">

        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <ul class="actions">
                    <li><%=Html.RouteRefLink("Create a new administrator", AdministratorsRoutes.New, null)%></li>
                </ul>
            </div>
            <div class="section-foot"></div>
        </div>

        <div class="section">
            <div class="section-body">
<%  if (Model.Count == 0)
    { %>
                <div id="search-results-header">
                    <div class="search-results-count">
                        There are no administrators.
                    </div>
                </div>
<%  }
    else
    { %>
                <table id="search-results" class="list">
                    <thead>
                        <tr>
                            <th class="user-column-header">Administrator</th>
                            <th class="status-column-header">Status</th>
                        </tr>
                    </thead>
                    <tbody>
    
<%      for (var index = 0; index < Model.Count; ++index)
        {
            var administrator = Model[index]; %>
           
                        <tr class="item item_<%= (index % 2) == 0 ? "odd" : "even" %> <%= administrator.IsEnabled ? "enabled-user" : "disabled-user" %> user">
                            <td><%= Html.RouteRefLink(administrator.FullName, AdministratorsRoutes.Edit, new {id = administrator.Id}) %></td>
                            <td><%= administrator.IsEnabled ? "Enabled" : "Disabled" %></td>
                        </tr>
<%      } %>
                    </tbody>
                </table>
<%  } %>
   
            </div>
        </div>

    </div>
        
</asp:Content>
