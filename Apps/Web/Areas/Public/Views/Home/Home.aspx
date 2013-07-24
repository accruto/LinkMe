<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<HomeModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Home"%>
<%@ Import Namespace="LinkMe.Web.Content"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.JQueryCustom) %>
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.RenderStyles(StyleBundles.Home) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom) %>
        <%= Html.RenderScripts(ScriptBundles.Home) %>
        <%= Html.JavaScript(JavaScripts.SwfObject) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="PageSubHeader" runat="server">
    <% Html.RenderPartial("Login", Model); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="homepage_aspx main-content">
        <div class="section">
            <div class="left-section">
                <% Html.RenderPartial("Jobs", Model.Reference); %>
            </div>
            <div class="right-section">
                <% Html.RenderPartial("ExposeYourself", Model.Reference); %>
            </div>
        </div>
        <div class="section">
            <% Html.RenderPartial("CandidateHelpInfo"); %>
        </div>
        <div class="section">
            <% Html.RenderPartial("JobsByEmployer", Model.Reference); %>
        </div>
        <div class="section">
            <% Html.RenderPartial("EmployerHelpInfo"); %>
        </div>
        <div class="section">
            <% Html.RenderPartial("GoogleAds"); %>
        </div>
    </div>
    
    <%if (Model.PreferredUserType == UserType.Anonymous) { %>
    <div class="firstlogin" url="<%= Html.MungeUrl(Html.RouteRefUrl(LinkMe.Web.Areas.Accounts.Routes.AccountsRoutes.PreferredUserType)) %>">
        <div class="bg">
            <div class="icon close"></div>
            <div class="button candidate"></div>
            <div class="button employer" url="<%= Html.RouteRefUrl(HomeRoutes.Home) %>"></div>
        </div>
    </div>
    <% } %>
</asp:Content>