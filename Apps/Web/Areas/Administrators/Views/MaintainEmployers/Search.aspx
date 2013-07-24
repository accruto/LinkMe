<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="System.Web.Mvc.ViewPage<EmployerSearchModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Employers"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.NoLeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Administrators.Page) %>
        <%= Html.StyleSheet(StyleSheets.JQueryCustom) %>
        <%= Html.StyleSheet(StyleSheets.JQueryAutocomplete) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.Autocomplete) %>
        <%= Html.RenderScripts(ScriptBundles.Administrators) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Search employers</h1>
    </div>
    
    <div class="form">
<%  using (Html.RenderForm())
    {
        using (Html.RenderFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
       
                <%= Html.TextBoxField(Model.Criteria, c => c.OrganisationName)
                    .WithLabel("Organisation name")
                    .WithLargestWidth() %>
                <%= Html.CheckBoxField(Model.Criteria, c => c.MatchOrganisationNameExactly)
                    .WithLabelOnRight("Match organisation name exactly") %>
                <%= Html.TextBoxField(Model.Criteria, c => c.LoginId)
                    .WithLabel("Username")
                    .WithLargerWidth() %>
                <%= Html.TextBoxField(Model.Criteria, c => c.FirstName).WithLabel("First name") %>
                <%= Html.TextBoxField(Model.Criteria, c => c.LastName).WithLabel("Last name") %>
                <%= Html.TextBoxField(Model.Criteria, c => c.EmailAddress)
                    .WithLabel("Email address")
                    .WithLargerWidth() %>
                <%= Html.CheckBoxesField(Model.Criteria)
                    .Add("Enabled", c => c.IsEnabled)
                    .Add("Disabled", c => c.IsDisabled)
                    .WithLabel("Status") %>

                <%= Html.ButtonsField().Add(new SearchButton()) %>
    
            </div>    
            <div class="section-foot"></div>
        </div>
        
<%      }
        
        if (Model.Employers != null)
        { %>
        
        <div class="section">
            <div class="section-body">
                <div id="search-results-header">
<%          if (Model.Employers.Count == 0)
            { %>
                    <div class="search-results-count">
                        No results found.
                    </div>
<%          }
            else
            { %>
                    <div class="search-download-actions">
                        <%= Html.ButtonsField().Add(new DownloadButton())%>
                    </div>
                    <div class="search-results-count">
                        <%= Model.Employers.Count%> result<%= Model.Employers.Count == 1 ? "" : "s"%> found.
                    </div>
<%          } %>
                </div>
                    
<%          if (Model.Employers.Count != 0)
            { %>
                <table id="search-results" class="list">
                    <thead>
                        <tr>
                            <th class="user-column-header">Name</th>
                            <th class="username-column-header">Username</th>
                            <th class="organisation-column-header">Organisation</th>
                            <th class="status-column-header">Status</th>
                        </tr>
                    </thead>
                    <tbody>
                                
<%              for (var index = 0; index < Model.Employers.Count(); ++index)
                {
                    var employer = Model.Employers[index]; %>

        		        <tr class="item item_<%= (index % 2) == 0 ? "odd" : "even" %> <%= employer.Employer.IsEnabled ? "enabled-user" : "disabled-user" %> user">
                            <td><%= Html.RouteRefLink(employer.Employer.FullName, EmployersRoutes.Edit, new { id = employer.Employer.Id })%></td>
                            <td><%= Html.Encode(employer.LoginId) %></td>
                            <td><%= Html.Encode(employer.Employer.Organisation.FullName)%></td>
                            <td><%= employer.Employer.IsEnabled ? "Enabled" : "Disabled"%></td>
                        </tr>
<%              } %>
                    </tbody>
                </table>
                        
<%          } %>

            </div>
        </div>
<%      } %>

<%  } %>
            
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            linkme.administrators.ready({
                urls: {
                    apiOrganisationsPartialMatchesUrl: '<%= Html.RouteRefUrl(OrganisationsRoutes.ApiPartialMatches) %>?name='
                },
            });
        });
    </script>
            
</asp:Content>
