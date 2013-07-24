<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<OrganisationSearchModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Organisations"%>
<%@ Import Namespace="LinkMe.Web.Content" %>
<%@ Import Namespace="LinkMe.Web.Domain.Users.Administrators"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Recruiters"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.NoLeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Administrators.Page) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Search organisations</h1>
    </div>
    
    <div class="form">
    
<%  using (Html.RenderForm())
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
    
                <%= Html.TextBoxField(Model.Criteria, c => c.FullName)
                    .WithLabel("Organisation name")
                    .WithLargestWidth() %>
                <%= Html.AdministratorField(Model.Criteria, c => c.AccountManagerId, Model.AccountManagers)
                    .WithLabel("Account manager") %>
                <%= Html.CheckBoxesField(Model)
                    .Add("Verified", m => m.Criteria.VerifiedOrganisations)
                    .Add("Unverified", m => m.Criteria.UnverifiedOrganisations)
                    .WithLabel("Status") %>

                <%= Html.ButtonsField().Add(new SearchButton()) %>
    
            </div>    
            <div class="section-foot"></div>
        </div>
<%      }
        
        if (Model.Organisations != null)
        { %>
        <div class="section">
            <div class="section-body">
                <div id="search-results-header">
<%          if (Model.Organisations.Count == 0)
            { %>
                    <div class="search-results-count">
                        No results found.
                    </div>
<%          }
            else
            { %>
                    <div class="search-results-count">
                        <%= Model.Organisations.Count %> result<%= Model.Organisations.Count == 1 ? "" : "s" %> found.
                    </div>
<%          } %>
                </div>
                
<%          if (Model.Organisations.Count != 0)
            { %>
                <table id="search-results" class="list">
                    <thead>
                        <tr>
                            <th class="organisation-column-header">Organisation</th>
                            <th class="user-column-header">Account&nbsp;manager</th>
                            <th class="user-column-header">Verified&nbsp;by</th>
                        </tr>
                    </thead>
                    <tbody>
<%              for (var index = 0; index < Model.Organisations.Count(); ++index)
                {
                    var organisation = Model.Organisations[index];
                    var verifiedOrganisation = organisation as VerifiedOrganisation; %>
        		        <tr class="item item_<%= (index % 2) == 0 ? "odd" : "even" %> <%= verifiedOrganisation != null ? "verified-organisation" : "unverified-organisation" %> organisation">
                            <td>
                                <%= Html.RouteRefLink(Html.Encode(organisation.FullName), OrganisationsRoutes.Edit, new {id = organisation.Id}) %>
                            </td>
                            <td><%= Html.AccountManagerFullName(Model.AccountManagers, verifiedOrganisation != null ? verifiedOrganisation.AccountManagerId : (Guid?)null) %></td>
                            <td><%= Html.AccountManagerFullName(Model.AccountManagers, verifiedOrganisation != null ? verifiedOrganisation.VerifiedById : (Guid?)null) %></td>
                        </tr>
<%              } %>
                    </tbody>
                </table>
<%          } %>

            </div>
        </div>
<%      }
    } %>
        
    </div>
            
</asp:Content>
