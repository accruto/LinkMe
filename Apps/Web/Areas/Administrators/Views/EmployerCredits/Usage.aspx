<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Administrators.Models.Employers.EmployerExercisedCreditsModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.JQueryUiAll) %>
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Administrators.Page) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryUiCore) %>
        <%= Html.JavaScript(JavaScripts.JQueryUiDatePicker) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div class="section">
	    <div class="section-body">
		    <ul class="plain-actions actions">
		        <li><%= Html.RouteRefLink(Model.Employer.FullName, EmployersRoutes.Edit, new { id = Model.Employer.Id }) %></li>
                <li><%= Html.RouteRefLink("View credits", EmployersRoutes.Credits, new { id = Model.Employer.Id }) %></li>
		    </ul>
	    </div>
    </div>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Employer credit usage</h1>
    </div>
    
    <div class="form">

<%  using (Html.RenderForm())
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                
                <%= Html.DateField(Model, m => m.StartDate)
                    .WithLabel("Start date")
                    .WithButton(Images.Block.Calendar)
                    .WithFormat("dd/MM/yyyy") %>
                <%= Html.DateField(Model, m => m.EndDate)
                    .WithLabel("End date")
                    .WithButton(Images.Block.Calendar)
                    .WithFormat("dd/MM/yyyy") %>
                <%= Html.ButtonsField().Add(new SearchButton()) %>
                   
            </div>
            <div class="section-foot"></div>
        </div>

<%      }
    } %>

        <div class="section">
            <div class="section-body">

<%  if (Model.ExercisedCredits.Count == 0)
    { %>
                <div id="credits-header">
                    This employer has not used any credits.
                </div>
<%  }
    else
    { %>
                <table id="credits" class="list">
                    <thead>
                        <tr>
			                <th class="time-column-header">Time</th>
			                <th class="credit-column-header">Credit</th>
			                <th class="owner-column-header">Owner</th>
			                <th class="used-on-column-header">Used on</th>
			                <th class="credit-used-column-header">Credit used</th>
                        </tr>
                    </thead>
                    <tbody>
                            
<%      for (var index = 0; index < Model.ExercisedCredits.Count; ++index)
        {
            var exercisedCredit = Model.ExercisedCredits[index];
            var allocation = exercisedCredit.AllocationId == null ? null : Model.Allocations[exercisedCredit.AllocationId.Value]; %>
                                   
                        <tr class="item item_<%= (index % 2) == 0 ? "odd" : "even" %>">
                            <td class="time-column"><%= exercisedCredit.Time%></td>
                            <td class="credit-column"><%= Model.Credits[exercisedCredit.CreditId].ShortDescription%></td>

<%          if (allocation == null || allocation.OwnerId == Model.Employer.Id)
            { %>    
                            <td class="owner-column"><%= Html.RouteRefLink(Model.Employer.FullName, EmployersRoutes.Edit, new {id = Model.Employer.Id})%></td>
<%          }
            else
            { %>
                            <td class="owner-column"><%= Html.RouteRefLink(Model.Organisations[allocation.OwnerId].FullName, OrganisationsRoutes.Edit, new { id = allocation.OwnerId })%></td>
<%          } %>
                                        
                            <td class="used-on-column">
<%          if (exercisedCredit.ExercisedOnId != null)
            { %>
                                <%= Html.RouteRefLink(Model.Members[exercisedCredit.ExercisedOnId.Value].FullName, MembersRoutes.Edit, new { id = exercisedCredit.ExercisedOnId.Value })%>
<%          }
            else if (exercisedCredit.ReferenceId != null)
            {
                var jobAd = Model.JobAds[exercisedCredit.ReferenceId.Value]; %>
                                <a href="<%= jobAd.GenerateJobAdUrl() %>"><%= Html.TruncateForDisplay(jobAd.Title, 50) %></a>
<%          } %>

                            </td>
                            <td class="credit-used-column"><%= exercisedCredit.AdjustedAllocation ? "Yes" : "No" %></td>
                        </tr>
                                
<%      } %>
                    </tbody>
                </table>
                        
<%  } %>
            
            </div>
        </div>

    </div>
        
</asp:Content>

