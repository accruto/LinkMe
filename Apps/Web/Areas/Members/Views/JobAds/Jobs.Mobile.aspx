<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.DeviceMyJobs) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="Body" runat="server">
    <div class="myjobs">
        <div class="title">My jobs<a href="<%= Html.RouteRefUrl(HomeRoutes.Home) %>"><div class="icon newsearch"></div>New job search</a></div>
        <%= Html.RouteRefLink("Saved jobs", JobAdsRoutes.MobileFolder, null, new { @class="button savedjobs" }) %>
        <%= Html.RouteRefLink("Jobs I've applied for", JobAdsRoutes.Applications, null, new { @class = "button jobsappliedfor" })%>
        <%= Html.RouteRefLink("Suggested jobs", JobAdsRoutes.Suggested, null, new { @class = "button suggestedjobs" })%>
    </div>
</asp:Content>