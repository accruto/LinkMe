<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="System.Web.Mvc.ViewPage<IList<Community>>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Affiliations.Communities"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.NoLeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Administrators.Page) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Communities</h1>
    </div>
    
    <div class="form">

        <div class="section">
            <div class="section-body">
                <div id="search-results-header">
<%  if (Model.Count == 0)
    { %>
                    <div class="search-results-count">
                        No results found.
                    </div>
<%  }
    else
    { %>
                    <div class="search-results-count">
                        <%= Model.Count%> communit<%= Model.Count == 1 ? "y" : "ies"%> found.
                    </div>
<%  } %>
                </div>
                
                <table id="search-results" class="list">
                    <thead>
                        <tr>
                            <th>Name</th>
                        </tr>
                    </thead>
                    <tbody>
                                
<%  for (var index = 0; index < Model.Count(); ++index)
    {
        var community = Model[index]; %>
                        <tr class="item item_<%= (index % 2) == 0 ? "odd" : "even" %>">
                            <td><%= Html.RouteRefLink(community.Name, CommunitiesRoutes.Edit, new { id = community.Id })%></td>
                        </tr>
<%  } %>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
            
</asp:Content>
