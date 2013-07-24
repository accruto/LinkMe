<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/DeviceSite.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Members.Models.JobAds.JobAdModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Domain.Contacts" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.DeviceEmailJobAd) %>
    </mvc:RegisterStyleSheets>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.RenderScripts(ScriptBundles.DeviceEmailJobAd) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <div class="emailjobad">
        <div class="title">The <span><%= Model.JobAd.Title %></span> job was emailed successfully.</div>
        <div class="succ">
            <ul>
                <li><%= Html.RouteRefLink("Back", SearchRoutes.Results) %><span>&nbsp;to search results</span></li>
            </ul>
        </div>
    </div>
</asp:Content>