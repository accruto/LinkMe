<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Employers.Models.Accounts.AccountModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.NoLeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.StyleSheet(StyleSheets.Block.Employers.Account) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.JobAds.JobAd) %>
        <%= Html.StyleSheet(StyleSheets.JQueryCustom) %>
        <%= Html.StyleSheet(StyleSheets.JQueryAutocomplete) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.Autocomplete) %>
        <%= Html.RenderScripts(ScriptBundles.Site) %>
        <%= Html.JavaScript(JavaScripts.Employers.Account) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Log in / Join</h1>
    </div>
    
    <div class="form">
<%  using (Html.RenderForm(Context.GetClientUrl()))
    { %>
    
        <% Html.RenderPartial("PartialAccount", Model); %>

        <div class="right-buttons-section section">
            <div class="section-body">
                <%= Html.ButtonsField(new CancelButton()) %>
            </div>
        </div>

<%  } %>
    </div>
    
    <script type="text/javascript">
        $(document).ready(function () {
            linkme.employers.account.ready({
                urls: {
                    partialMatchesUrl: '<%= Html.RouteRefUrl(LinkMe.Web.Areas.Api.Routes.LocationRoutes.PartialMatches) + "?countryId=1&location=" %>'
                }
            });
        });
    </script>
        
</asp:Content>
