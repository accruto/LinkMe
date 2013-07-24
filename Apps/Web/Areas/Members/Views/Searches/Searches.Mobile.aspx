<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.DeviceMySearches) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="Body" runat="server">
    <div class="mysearches">
        <div class="title">My searches</div>
        <%= Html.RouteRefLink("New job search", HomeRoutes.Home, null, new { @class="button newsearch" }) %>
        <%= Html.RouteRefLink("Recent searches", SearchRoutes.RecentSearches, null, new { @class = "button recentsearches" })%>
        <%= Html.RouteRefLink("My favourite searches", SearchRoutes.SavedSearches, null, new { @class = "button myfavouritesearches" })%>
    </div>
</asp:Content>