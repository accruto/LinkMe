<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<NewOrderAccountModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Models.Products"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.StyleSheet(StyleSheets.Block.Employers.Account) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.Orders) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.Products.NewOrder) %>
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

<asp:Content ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Employer site</li>
        <li class="current-breadcrumb">New order</li>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <div class="section">
        <div class="section-body">
            <% Html.RenderPartial("WizardSteps", Model.Steps); %>
        </div>
    </div>
    <div class="section">
        <div class="section-body">
            <div id="order-compact-details">
                <% Html.RenderPartial("OrderCompactDetails", Model.OrderDetails); %>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Log in / Join</h1>
    </div>
    
    <div class="form">
<%  using (Html.RenderForm(Model.GetUrl(Context.GetClientUrl())))
    { %>
    
        <% Html.RenderPartial("PartialAccount", Model.Account); %>
    
        <div class="right-buttons-section section">
            <div class="section-body">
                <%= Html.ButtonsField(new BackButton(), new CancelButton()) %>
            </div>
        </div>

    <% } %>
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
