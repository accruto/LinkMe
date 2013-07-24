<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="System.Web.Mvc.ViewPage<MemberSearchModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Members"%>
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
        <h1>Search members</h1>
    </div>
    
    <div class="form">
    
<% using (Html.RenderForm())
   {
       using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
       { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
    
                <%= Html.TextBoxField(Model.Criteria, c => c.FirstName).WithLabel("First name") %>
                <%= Html.TextBoxField(Model.Criteria, c => c.LastName).WithLabel("Last name") %>
                <%= Html.TextBoxField(Model.Criteria, c => c.EmailAddress).WithLabel("Email address") %>
                <%= Html.DropDownListField(Model.Criteria, c => c.Count, new int?[]{10, 20, 50, 100, 200}) %>

                <%= Html.ButtonsField().Add(new SearchButton()) %>
    
            </div>    
            <div class="section-foot"></div>
        </div>
<%      }
        
        if (Model.Members != null)
        { %>
            <div class="section-body">
                <div id="search-results-header">
<%          if (Model.Members.Count == 0)
            { %>
                    <div class="search-results-count">
                        No results found.
                    </div>
<%          }
            else
            { %>
                    <div class="search-results-count">
                        <%= Model.Members.Count%> result<%= Model.Members.Count == 1 ? "" : "s"%> found.
                    </div>
<%          } %>
                </div>
        
<%          if (Model.Members.Count != 0)
            { %>
                <table id="search-results" class="list">
                    <thead>
                        <tr>
                            <th class="email-address-column-header">Email address</th>
                            <th class="name-column-header">Name</th>
                            <th class="status-column-header">Status</th>
                        </tr>
                    </thead>
                    <tbody>
<%              for (var index = 0; index < Model.Members.Count(); ++index)
                {
                    var member = Model.Members[index]; %>
        		        <tr class="item item_<%= (index % 2) == 0 ? "odd" : "even" %>">
                            <td><%= Html.RouteRefLink(member.EmailAddresses[0].Address, MembersRoutes.Edit, new {id = member.Id}) %></td>
                            <td><%= Html.Encode(member.FullName)%></td>
                            <td><%= member.IsEnabled ? (member.IsActivated ? "Activated" : "Deactivated") : "Disabled"%></td>
                        </tr>
<%              } %>
                    </tbody>
                </table>
<%          } %>

            </div>
<%      }
    } %>
            
    </div>
            
</asp:Content>
