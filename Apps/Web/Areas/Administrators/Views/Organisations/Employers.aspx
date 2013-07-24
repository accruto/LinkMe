<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<EmployersModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Organisations"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

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
                <li><%= Html.RouteRefLink("Reports", OrganisationsRoutes.Reports, new { id = Model.Organisation.Id })%></li>
                <li><%= Html.RouteRefLink("Communications", OrganisationsRoutes.Communications, new { id = Model.Organisation.Id })%></li>
		    </ul>
	    </div>
    </div>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Organisation employers</h1>
    </div>
    
    <div class="form">

<%  if (Model.Organisation.IsVerified)
    { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <ul class="actions">
                    <li><%=Html.RouteRefLink("Create a new employer account", OrganisationsRoutes.NewEmployer, new { id = Model.Organisation.Id })%></li>
                </ul>
            </div>
            <div class="section-foot"></div>
        </div>
<%  } %>

<%  using (Html.RenderForm())
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        {
            if (Model.Organisation.IsVerified)
            { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <%= Html.CheckBoxField(Model, m => m.IncludeChildOrganisations)
                    .WithLabelOnRight("Include child organisations")%>
            
                <%= Html.ButtonsField(new SearchButton())%>
            </div>
            <div class="section-foot"></div>
        </div>
<%          } %>

        <div class="section">
            <div class="section-body">
                <div id="search-results-header">
<%          if (Model.Employers.Count == 0)
            { %>
                    <div class="search-results-count">
                        There are no employers associated with this organisation.
                    </div>
<%          }
            else
            { %>
                    <div class="search-download-actions">
                        <%= Html.ButtonsField().Add(new DownloadButton())%>
                    </div>
<%          } %>
                </div>

                <table id="search-results" class="list">
                    <thead>
                        <tr>
                            <th class="user-column-header">Employer</th>
                            <th class="organisation-column-header">Organisation</th>
                            <th class="status-column-header">Status</th>
                        </tr>
                    </thead>
                    <tbody>
            
<%          for (var index = 0; index < Model.Employers.Count; ++index)
            {
                var employer = Model.Employers[index]; %>
                   
                    <tr class="item item_<%= (index % 2) == 0 ? "odd" : "even" %> <%= employer.IsEnabled ? "enabled-user" : "disabled-user" %> user">
                        <td><%= Html.RouteRefLink(employer.FullName, EmployersRoutes.Edit, new { id = employer.Id }) %></td>
                        <td><%= Html.Encode(employer.Organisation.FullName) %></td>
                        <td><%= employer.IsEnabled ? "Enabled" : "Disabled" %></td>
                    </tr>
                
<%          } %>
                    </tbody>
                </table>
   
            </div>
        </div>
<%      }
    } %>

    </div>
        
</asp:Content>
