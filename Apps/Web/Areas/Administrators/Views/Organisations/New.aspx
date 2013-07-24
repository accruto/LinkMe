<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<OrganisationModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Areas.Administrators.Models.Organisations"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.NoLeftSideBar) %>
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

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>New organisation</h1>
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
                <%= Html.ButtonsField(new CreateButton(), new CancelButton()) %>
                        
            </div>
            <div class="section-foot"></div>
        </div>
<%      }
    } %>
            
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
