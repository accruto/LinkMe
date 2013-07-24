<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<OrganisationModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Organisations"%>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.Recruiters"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
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

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div class="section">
	    <div class="section-body">
		    <ul class="plain-actions actions">
                <li><%= Html.RouteRefLink("Credits", OrganisationsRoutes.Credits, new { id = Model.Organisation.Id })%></li>
                <li><%= Html.RouteRefLink("Employers", OrganisationsRoutes.Employers, new { id = Model.Organisation.Id })%></li>
                <li><%= Html.RouteRefLink("Reports", OrganisationsRoutes.Reports, new { id = Model.Organisation.Id })%></li>
                <li><%= Html.RouteRefLink("Communications", OrganisationsRoutes.Communications, new { id = Model.Organisation.Id })%></li>
		    </ul>
	    </div>
    </div>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Edit organisation</h1>
    </div>
    
    <div class="form">

<%  using (Html.RenderForm())
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                   
                <% Html.RenderPartial("Organisation", Model); %>
                <%= Model.Organisation.IsVerified ? Html.ButtonsField(new SaveButton()) : Html.ButtonsField(new SaveButton(), new VerifyButton()) %>
            
            </div>
            <div class="section-foot"></div>
        </div>
<%      }
    } %>
        
<%  if (Model.OrganisationHierarchy != null)
    { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h1>Organisation hierarchy</h1>
                </div>
            
                <%= Html.OrganisationHierarchy(Model.OrganisationHierarchy, Model.Organisation, o => Html.RouteRefLink(o.Name, OrganisationsRoutes.Edit, new { id = o.Id })) %>
            </div>
            <div class="section-foot"></div>
        </div>
<%  } %>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            linkme.administrators.ready({
                urls: {
                    apiOrganisationsPartialMatchesUrl: '<%= Html.RouteRefUrl(OrganisationsRoutes.ApiPartialMatches) %>?name=',
                    apiLocationPartialMatchesUrl: '<%= Html.RouteRefUrl(LinkMe.Web.Areas.Api.Routes.LocationRoutes.PartialMatches) %>?countryId=1&location='
                },
            });
        });
    </script>

</asp:Content>
