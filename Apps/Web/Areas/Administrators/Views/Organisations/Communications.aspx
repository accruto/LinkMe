<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<CommunicationsModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.Communications.Settings"%>
<%@ Import Namespace="LinkMe.Domain.Roles.Communications.Settings"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Organisations"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>

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
                <li><%= Html.RouteRefLink("Reports", OrganisationsRoutes.Reports, new { id = Model.Organisation.Id })%></li>
		    </ul>
	    </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Organisation communications</h1>
    </div>

    <div class="form">

<%  using (Html.RenderForm())
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
            
                <p>
                    Disabling this setting will mean that no emails of the given type will be sent out to employers.
                </p>
                
<%          foreach (var category in Model.Categories)
            { %>
                <%= Html.CheckBoxField(category, category.Item1.Name, c => c.Item2 == null || c.Item2.Value != Frequency.Never)
                    .WithLabelOnRight(category.Item1.GetDisplayText()) %>
<%          } %>
        
                <%= Html.ButtonsField(new SaveButton()) %>
            </div>
            <div class="section-foot"></div>
        </div>
        
<%      }
    } %>
            
    </div>
        
</asp:Content>

